using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eshopper.Models.EF;
using Helpers;
using PagedList;

namespace Eshopper.Areas.Admin.Controllers
{
    public class SanPhamsController : Controller
    {
        private DBModels db = new DBModels();

        // GET: Admin/SanPhams
        [Authorize(Roles ="member")]
        public ActionResult Index(int? page, string searchString ="" )
        {
            var items = db.SanPhams.ToList();
            if(searchString != string.Empty)
            {
                items = items.Where(x => x.TenSP.Contains(searchString)).ToList();
            }
            var pageNumber = page == null ? 1: page.Value;
            var data = items.ToPagedList(pageNumber, 15);
            return View(data);
        }

        

        // GET: Admin/SanPhams/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaKM = new SelectList(db.BangKhuyenMais, "MaKM", "MaKM");
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoai");
            return View();
        }

        // POST: Admin/SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,SoLuong,DonGia,MoTa,GiaKM,URLAnh,MaLoaiSP,MaKM")] SanPham sanPham, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                if (!UploadImage(picture, sanPham))
                {
                    ModelState.AddModelError("", @"Hãy chọn ảnh");
                    ViewBag.MaKM = new SelectList(db.BangKhuyenMais, "MaKM", "MaKM", sanPham.MaKM);
                    ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoai", sanPham.MaLoaiSP);
                    return View(sanPham);
                }
                db.SanPhams.Add(sanPham);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKM = new SelectList(db.BangKhuyenMais, "MaKM", "MaKM", sanPham.MaKM);
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoai", sanPham.MaLoaiSP);
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKM = new SelectList(db.BangKhuyenMais, "MaKM", "MaKM", sanPham.MaKM);
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoai", sanPham.MaLoaiSP);
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,SoLuong,DonGia,MoTa,GiaKM,URLAnh,MaLoaiSP,MaKM")] SanPham sanPham, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                if(!UploadImage(picture, sanPham))
                {
                    ModelState.AddModelError("", @"Hãy chọn ảnh");
                    ViewBag.MaKM = new SelectList(db.BangKhuyenMais, "MaKM", "MaKM", sanPham.MaKM);
                    ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoai", sanPham.MaLoaiSP);
                    return View(sanPham);
                }
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKM = new SelectList(db.BangKhuyenMais, "MaKM", "MaKM", sanPham.MaKM);
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoai", sanPham.MaLoaiSP);
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        private bool UploadImage(HttpPostedFileBase picture, SanPham sanPham)
        {
            if (picture == null) return false;
            if (picture != null)
            {
                var imgPath = "/images/sanphams/" + DateTime.Now.ToString("yyyy/MM/dd");
                HtmlHelpers.CreateFolder(Server.MapPath(imgPath));

                sanPham.URLAnh = AddImageToFolder(picture, imgPath);
            }
            return true;
        }
        private string AddImageToFolder(HttpPostedFileBase picture, string imgPath)
        {
            var imgFileName = DateTime.Now.ToFileTimeUtc() + "." + HtmlHelpers.GetExt(picture.FileName);

            if (System.IO.File.Exists(Server.MapPath(imgPath + "/" + imgFileName)))
            {
                System.IO.File.Delete(Server.MapPath(imgPath + "/" + imgFileName));
            }
            picture.SaveAs(Server.MapPath(Path.Combine(imgPath, imgFileName)));
            string item = imgPath + "/" + imgFileName;
            return item;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
