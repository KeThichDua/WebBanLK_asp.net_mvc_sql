using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eshopper.Models.EF;

namespace Eshopper.Areas.Admin.Controllers
{
    public class BangKhuyenMaisController : Controller
    {
        private DBModels db = new DBModels();

        // GET: Admin/BangKhuyenMais
        public ActionResult Index()
        {
            return View(db.BangKhuyenMais.ToList());
        }

        // GET: Admin/BangKhuyenMais/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BangKhuyenMai bangKhuyenMai = db.BangKhuyenMais.Find(id);
            if (bangKhuyenMai == null)
            {
                return HttpNotFound();
            }
            return View(bangKhuyenMai);
        }

        // GET: Admin/BangKhuyenMais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/BangKhuyenMais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKM,TienKM,TiLeKM")] BangKhuyenMai bangKhuyenMai)
        {
            if (ModelState.IsValid)
            {
                db.BangKhuyenMais.Add(bangKhuyenMai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bangKhuyenMai);
        }

        // GET: Admin/BangKhuyenMais/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BangKhuyenMai bangKhuyenMai = db.BangKhuyenMais.Find(id);
            if (bangKhuyenMai == null)
            {
                return HttpNotFound();
            }
            return View(bangKhuyenMai);
        }

        // POST: Admin/BangKhuyenMais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKM,TienKM,TiLeKM")] BangKhuyenMai bangKhuyenMai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bangKhuyenMai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bangKhuyenMai);
        }

        // GET: Admin/BangKhuyenMais/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BangKhuyenMai bangKhuyenMai = db.BangKhuyenMais.Find(id);
            if (bangKhuyenMai == null)
            {
                return HttpNotFound();
            }
            return View(bangKhuyenMai);
        }

        // POST: Admin/BangKhuyenMais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BangKhuyenMai bangKhuyenMai = db.BangKhuyenMais.Find(id);
            db.BangKhuyenMais.Remove(bangKhuyenMai);
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
