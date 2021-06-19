using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eshopper.Controllers;
using Eshopper.Models.EF;

namespace Eshopper.Areas.Admin.Controllers
{
    public class NguoiDungsController : Controller
    {
        private DBModels db = new DBModels();

        // GET: Admin/NguoiDungs
        [Authorize(Roles = "employee,admin")]
        public ActionResult Index()
        {
            var nguoiDungs = db.NguoiDungs.Include(n => n.LoaiND);
            return View(nguoiDungs.ToList());
        }

        // GET: Admin/NguoiDungs/Details/5
        [Authorize(Roles = "employee,admin")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(nguoiDung);
        }

        // GET: Admin/NguoiDungs/Create
        [Authorize(Roles = "employee,admin")]
        public ActionResult Create()
        {
            ViewBag.MaLoaiND = new SelectList(db.LoaiNDs, "MaLoaiND", "TenLoai");
            return View();
        }

        // POST: Admin/NguoiDungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaND,SDT,GioiTinh,Tuoi,DiaChi,UserName,Email,Pass,MaLoaiND,TenND")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                db.NguoiDungs.Add(nguoiDung);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoaiND = new SelectList(db.LoaiNDs, "MaLoaiND", "TenLoai", nguoiDung.MaLoaiND);
            return View(nguoiDung);
        }

        // GET: Admin/NguoiDungs/Edit/5
        [Authorize(Roles = "employee,admin")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiND = new SelectList(db.LoaiNDs, "MaLoaiND", "TenLoai", nguoiDung.MaLoaiND);
            return View(nguoiDung);
        }

        // POST: Admin/NguoiDungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaND,SDT,GioiTinh,Tuoi,DiaChi,UserName,Email,Pass,MaLoaiND,TenND")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nguoiDung).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiND = new SelectList(db.LoaiNDs, "MaLoaiND", "TenLoai", nguoiDung.MaLoaiND);
            return View(nguoiDung);
        }

        // GET: Admin/NguoiDungs/Delete/5
        [Authorize(Roles = "employee,admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(nguoiDung);
        }

        // POST: Admin/NguoiDungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);

            var checkPX = db.PhieuXuats.Any(x => x.MaND == id);
            if (checkPX)
            {
                ModelState.AddModelError("", @"Vui lòng xóa hoặc sửa phiếu xuất của người dùng này trước.");
                return View(nguoiDung);
            }
            db.NguoiDungs.Remove(nguoiDung);
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
