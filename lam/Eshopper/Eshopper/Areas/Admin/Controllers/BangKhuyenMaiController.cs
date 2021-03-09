using Eshopper.Models.Dao;
using Eshopper.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eshopper.Areas.Admin.Controllers
{
    public class BangKhuyenMaiController : Controller
    {
        // GET: Admin/BangKhuyenMai
        public ActionResult Index()
        {
            BangKhuyenMaiDAO db = new BangKhuyenMaiDAO();
            List<BangKhuyenMai> listsp = db.GetListBangKhuyenMai();
            return View(listsp);
        }

        // POST: Admin/BangKhuyenMai/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Admin/BangKhuyenMai/Create
        [HttpPost]
        public ActionResult Create(BangKhuyenMai sp)
        {
            if (ModelState.IsValid)
            {
                var dao = new BangKhuyenMaiDAO();

                bool id = dao.Insert(sp);
                if (id)
                {
                    return RedirectToAction("Index", "BangKhuyenMai");
                }
                else
                {
                    ModelState.AddModelError("", "Tạo khuyến mại thất bại!");
                }
            }
            return View("Index");
        }

        // GET: Admin/BangKhuyenMai/Edit/5
        public ActionResult Edit(string id)
        {
            var sp = new BangKhuyenMaiDAO().ViewDetail(id);
            return View(sp);
        }

        // POST: Admin/BangKhuyenMai/Edit/5
        [HttpPost]
        public ActionResult Edit(BangKhuyenMai sp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new BangKhuyenMaiDAO();
                    var result = dao.Update(sp);
                    if (result)
                    {
                        return RedirectToAction("Index", "BangKhuyenMai");
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

        // delete: Admin/BangKhuyenMai/Delete/5
        public ActionResult Delete(string id)
        {
            while (id.Length < 10)
            {
                id += " ";
            }
            new BangKhuyenMaiDAO().Delete(id);
            return RedirectToAction("Index");
        }

    }
}