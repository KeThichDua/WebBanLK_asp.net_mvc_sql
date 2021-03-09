using Eshopper.Models.Dao;
using Eshopper.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eshopper.Areas.Admin.Controllers
{
    public class NhaCungCapController : Controller
    {
        // GET: Admin/NhaCC
        public ActionResult Index()
        {
            NhaCCDAO db = new NhaCCDAO();
            List<NhaCC> listsp = db.GetListNhaCC();
            return View(listsp);
        }

        // POST: Admin/NhaCC/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Admin/NhaCC/Create
        [HttpPost]
        public ActionResult Create(NhaCC sp)
        {
            if (ModelState.IsValid)
            {
                var dao = new NhaCCDAO();

                bool id = dao.Insert(sp);
                if (id)
                {
                    return RedirectToAction("Index", "NhaCungCap");
                }
                else
                {
                    ModelState.AddModelError("", "Tạo nhà cc thất bại!");
                }
            }
            return View("Index");
        }

        // GET: Admin/NhaCC/Edit/5
        public ActionResult Edit(string id)
        {
            var sp = new NhaCCDAO().ViewDetail(id);
            return View(sp);
        }

        // POST: Admin/NhaCC/Edit/5
        [HttpPost]
        public ActionResult Edit(NhaCC sp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new NhaCCDAO();
                    var result = dao.Update(sp);
                    if (result)
                    {
                        return RedirectToAction("Index", "NhaCungCap");
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

        // delete: Admin/NhaCC/Delete/5
        public ActionResult Delete(string id)
        {
            while (id.Length < 10)
            {
                id += " ";
            }
            new NhaCCDAO().Delete(id);
            return RedirectToAction("Index");
        }
    }
}
