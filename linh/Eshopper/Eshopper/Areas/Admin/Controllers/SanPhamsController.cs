using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eshopper.Models.EF;
using PagedList;

namespace Eshopper.Areas.Admin.Controllers
{
    public class SanPhamsController : Controller
    {
        private DBModels db = new DBModels();

        // GET: Admin/SanPhams
        public ActionResult Index(string searchString, int? page)
        {
            //var loaisps = from l in db.LoaiSPs select l;
            //ViewBag.MaLoaiSP = new SelectList(loaisps, "MaLoaiSP", "TenLoai");

            //var sps = from s in db.SanPhams
            //          join l in db.LoaiSPs on s.MaLoaiSP equals l.MaLoaiSP
            //          select new { s.MaSP, s.TenLoai, s.SoLuong, s.DonGia, s.MoTa, s.GiaKM, s.URLAnh, s.MaLoaiSP, s.MaKM, l.TenLoai };
            //1. Tạo danh sách danh mục để hiển thị ở giao diện View thông qua DropDownList
            //var lsps = from l in db.LoaiSPs select l;
            //ViewBag.MaLoaiSP = new SelectList(lsps, "MaLoaiSP", "TenLoai"); // danh sách Category

            //2. Tạo câu truy vấn kết 2 bảng Link, Category bằng hàm Include do 2 bảng có ràng buộc ở các thuộc tính virtual
            //var sps = db.SanPhams.Include(s => s.LoaiSP);

            //3. Tìm kiếm chuỗi truy vấn
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    sps = sps.Where(s => s.TenSP.Contains(searchString));
            //}

            //4. Tìm kiếm theo CategoryID
            //if (MaLoaiSP.equal != null)
            //{
            //    lsps = lsps.Where(x => x.MaLoaiSP == MaLoaiSP);
            //}

            //return View(sps.ToList());

            //if (page == null) page = 1;

            //var sps = (from s in db.SanPhams select s).OrderBy(x => x.MaSP);
            //var sps = (from s in db.SanPhams select s).OrderBy(s=>s.MaSP);

            //var properties = typeof(SanPham).GetProperties();
            //List<Tuple<string, bool, int>> list = new List<Tuple<string, bool, int>>();
            //foreach (var item in properties)
            //{
            //    int order = 999;
            //    var isVirtual = item.GetAccessors()[0].IsVirtual;

            //    if (item.Name == "LinkName") order = 2;
            //    if (item.Name == "LinkID") order = 1;
            //    if (item.Name == "LinkDescription") order = 3;
            //    if (item.Name == "LinkURL") order = 4;
            //    if (item.Name == "CategoryID") continue; // Không hiển thị cột này
            //    Tuple<string, bool, int> t = new Tuple<string, bool, int>(item.Name, isVirtual, order);
            //    list.Add(t);
            //}
            ////list = list.OrderBy(x => x.Item3).ToList();
            //if (page == null) page = 1;

            var sps = from s in db.SanPhams select s;

            //sps.OrderBy(x => x.MaSP);

         

            //int pageSize = 3;

            

            //int pageNumber = (page ?? 1);

            
            if (!String.IsNullOrEmpty(searchString))
            {
                sps = sps.Where(s => s.TenSP.Contains(searchString));
            }

            return View(sps);
            //sps.OrderBy(x => x.MaSP);
            //return View(sps.ToPagedList(pageNumber, pageSize));
            //var sanPhams = db.SanPhams.Include(s => s.BangKhuyenMai).Include(s => s.LoaiSP);
            //return View(sanPhams.ToList());
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
        public ActionResult Create([Bind(Include = "MaSP,TenSP,SoLuong,DonGia,MoTa,GiaKM,URLAnh,MaLoaiSP,MaKM")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
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
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,SoLuong,DonGia,MoTa,GiaKM,URLAnh,MaLoaiSP,MaKM")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
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
