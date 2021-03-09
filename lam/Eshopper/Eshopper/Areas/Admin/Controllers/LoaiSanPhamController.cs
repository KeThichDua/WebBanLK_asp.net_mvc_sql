using Eshopper.Models.Dao;
using Eshopper.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eshopper.Areas.Admin.Controllers
{
    public class LoaiSanPhamController : Controller
    {
        // GET: Admin/LoaiSanPham
        public ActionResult Index()
        {
            LoaiSanPhamDAO db = new LoaiSanPhamDAO();
            List<LoaiSP> listsp = db.GetListLoaiSP();
            return View(listsp);
        }

        // POST: Admin/LoaiSanPham/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Admin/LoaiSanPham/Create
        [HttpPost]
        public ActionResult Create(LoaiSP sp)
        {
            if (ModelState.IsValid)
            {
                var dao = new LoaiSanPhamDAO();

                bool id = dao.Insert(sp);
                if (id)
                {
                    return RedirectToAction("Index", "LoaiSanPham");
                }
                else
                {
                    ModelState.AddModelError("", "Tạo mới Loại sp thất bại!");
                }
            }
            return View("Index");
        }

        // GET: Admin/LoaiSanPham/Edit/5
        public ActionResult Edit(string id)
        {
            var sp = new LoaiSanPhamDAO().ViewDetail(id);
            return View(sp);
        }

        // POST: Admin/LoaiSanPham/Edit/5
        [HttpPost]
        public ActionResult Edit(LoaiSP sp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new LoaiSanPhamDAO();
                    var result = dao.Update(sp);
                    if (result)
                    {
                        return RedirectToAction("Index", "LoaiSanPham");
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

        // delete: Admin/LoaiSanPham/Delete/5
        public ActionResult Delete(string id)
        {
            while (id.Length < 10)
            {
                id += " ";
            }
            new LoaiSanPhamDAO().Delete(id);
            return RedirectToAction("Index");
        }
    }
}
