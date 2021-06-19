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
    public class NhaCungCapController : Controller
    {
        private DBModels db = new DBModels();

        // GET: Admin/NhaCungCap
        [Authorize(Roles = "employee,admin")]
        public ActionResult Index()
        {
            return View(db.NhaCCs.ToList());
        }

        // GET: Admin/NhaCungCap/Details/5
        [Authorize(Roles = "employee,admin")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCC nhaCC = db.NhaCCs.Find(id);
            if (nhaCC == null)
            {
                return HttpNotFound();
            }
            return View(nhaCC);
        }

        // GET: Admin/NhaCungCap/Create
        [Authorize(Roles = "employee,admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NhaCungCap/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNCC,Ten,DiaChi,SDT,Email")] NhaCC nhaCC)
        {
            if (ModelState.IsValid)
            {
                nhaCC.MaNCC = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString();
                while (nhaCC.MaNCC.Contains("."))
                {
                    nhaCC.MaNCC = nhaCC.MaNCC.Substring(1, nhaCC.MaNCC.Length - 2);
                }
                db.NhaCCs.Add(nhaCC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nhaCC);
        }

        // GET: Admin/NhaCungCap/Edit/5
        [Authorize(Roles = "employee,admin")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCC nhaCC = db.NhaCCs.Find(id);
            if (nhaCC == null)
            {
                return HttpNotFound();
            }
            return View(nhaCC);
        }

        // POST: Admin/NhaCungCap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNCC,Ten,DiaChi,SDT,Email")] NhaCC nhaCC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhaCC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhaCC);
        }

        // GET: Admin/NhaCungCap/Delete/5
        [Authorize(Roles = "employee,admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCC nhaCC = db.NhaCCs.Find(id);
            if (nhaCC == null)
            {
                return HttpNotFound();
            }
            return View(nhaCC);
        }

        // POST: Admin/NhaCungCap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NhaCC nhaCC = db.NhaCCs.Find(id);
            db.NhaCCs.Remove(nhaCC);
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
