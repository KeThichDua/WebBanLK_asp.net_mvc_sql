using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eshopper.Controllers;
using Eshopper.Models;
using Eshopper.Models.Dao;
using Eshopper.Models.EF;

namespace Eshopper.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: Admin/SanPham
        public ActionResult Index()
        {
            SanPhamDAO db = new SanPhamDAO();
            List <SanPham> listsp = db.GetListSanPham();
            return View(listsp);
        }

        // POST: Admin/SanPham/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Admin/SanPham/Create
        [HttpPost]
        public ActionResult Create(SanPham sp)
        {
            if (ModelState.IsValid)
            {
                var dao = new SanPhamDAO();

                bool id = dao.Insert(sp);
                if (id)
                {
                    return RedirectToAction("Index", "SanPham");
                }
                else
                {
                    ModelState.AddModelError("", "Create user failed!");
                }
            }
            return View("Index");
        }

        // GET: Admin/SanPham/Edit/5
        public ActionResult Edit(string id)
        {
            var sp = new SanPhamDAO().ViewDetail(id);
            return View(sp);
        }

        // POST: Admin/SanPham/Edit/5
        [HttpPost]
        public ActionResult Edit(SanPham sp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new SanPhamDAO();
                    var result = dao.Update(sp);
                    if (result)
                    {
                        return RedirectToAction("Index", "SanPham");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cập nhật thất bại!");
                    }
                }
                return View("Index");
            }
            catch
            {
                return View();
            }
        }

        // delete: Admin/SanPham/Delete/5
        public ActionResult Delete(string id)
        {
            while (id.Length < 10)
            {
                id += " ";
            }
            new SanPhamDAO().Delete(id);
            return RedirectToAction("Index");
        }

        //public ActionResult GetData()
        //{
        //    using (DbModel db = new DbModel())
        //    {

        //    }
        //}
        //// GET: Admin/SanPham/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}





        //// POST: Admin/SanPham/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
