using Eshopper.Models.Dao;
using Eshopper.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eshopper.Areas.Admin.Controllers
{
    public class LoaiNDController : Controller
    {
        // GET: Admin/LoaiND
        public ActionResult Index()
        {
            LoaiNDDAO db = new LoaiNDDAO();
            List<LoaiND> listsp = db.GetListLoaiND();
            return View(listsp);
        }

        // POST: Admin/LoaiND/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Admin/LoaiND/Create
        [HttpPost]
        public ActionResult Create(LoaiND sp)
        {
            if (ModelState.IsValid)
            {
                var dao = new LoaiNDDAO();

                bool id = dao.Insert(sp);
                if (id)
                {
                    return RedirectToAction("Index", "LoaiND");
                }
                else
                {
                    ModelState.AddModelError("", "Tạo loại nd thất bại!");
                }
            }
            return View("Index");
        }

        // GET: Admin/LoaiND/Edit/5
        public ActionResult Edit(string id)
        {
            var sp = new LoaiNDDAO().ViewDetail(id);
            return View(sp);
        }

        // POST: Admin/LoaiND/Edit/5
        [HttpPost]
        public ActionResult Edit(LoaiND sp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new LoaiNDDAO();
                    var result = dao.Update(sp);
                    if (result)
                    {
                        return RedirectToAction("Index", "LoaiND");
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

        // delete: Admin/LoaiND/Delete/5
        public ActionResult Delete(string id)
        {
            while (id.Length < 10)
            {
                id += " ";
            }
            new LoaiNDDAO().Delete(id);
            return RedirectToAction("Index");
        }

    }
}