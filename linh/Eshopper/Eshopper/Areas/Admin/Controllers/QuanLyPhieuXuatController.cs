using Eshopper.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eshopper.Areas.Admin.Controllers
{
    public class QuanLyPhieuXuatController : Controller
    {
        private DBModels _db = new DBModels();
        // GET: Admin/QuanLyPhieuNhap
        public ActionResult Index()
        {
            var item = _db.PhieuXuats.ToList();
            return View(item);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.SanPhams = _db.SanPhams.ToList();
            ViewBag.NguoiDung = _db.NguoiDungs.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(PhieuXuat model, IEnumerable<CTPhieuXuat> cTPhieuXuats)
        {
            try
            {
                var maPX = getMaPhieuXuat();
                model.MaPX = maPX;
                model.NgayDat = DateTime.Now;
                _db.PhieuXuats.Add(model);

                foreach (var item in cTPhieuXuats)
                {
                    item.MaPX = maPX;
                    var product = _db.SanPhams.Find(item.MaSP);
                    if (product != null)
                    {
                        product.SoLuong -= item.SoLuong;
                        if(product.SoLuong < 0)
                        {
                            ModelState.AddModelError("", "Số lượng không đủ");
                            return View(model);
                        }
                    }

                }
                _db.CTPhieuXuats.AddRange(cTPhieuXuats);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.SanPhams = _db.SanPhams.ToList();
                ViewBag.NguoiDung = _db.NguoiDungs.ToList();

                return View(model);
            }
        }
        string getMaPhieuXuat()
        {
            string maPX = "PX";
            for (int i = 1; i < 100000000; i++)
            {
                if (i < 10)
                {
                    maPX = "PX0000000" + i.ToString();
                    var item = _db.PhieuXuats.Find(maPX);
                    if (item == null)
                    {
                        return maPX;
                    }
                }
                else
                {
                    if (i < 100)
                    {
                        maPX = "PX000000" + i.ToString();
                        var item = _db.PhieuXuats.Find(maPX);
                        if (item == null)
                        {
                            return maPX;
                        }
                    }
                    else
                    {
                        if (i < 1000)
                        {
                            maPX = "PX00000" + i.ToString();
                            var item = _db.PhieuXuats.Find(maPX);
                            if (item == null)
                            {
                                return maPX;
                            }
                        }
                        else
                        {
                            if (i < 10000)
                            {
                                maPX = "PX0000" + i.ToString();
                                var item = _db.PhieuXuats.Find(maPX);
                                if (item == null)
                                {
                                    return maPX;
                                }
                            }
                            else
                            {
                                if (i < 100000)
                                {
                                    maPX = "PX000" + i.ToString();
                                    var item = _db.PhieuXuats.Find(maPX);
                                    if (item == null)
                                    {
                                        return maPX;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return maPX;
        }

        [HttpGet]
        public ActionResult Details(string maPX)
        {
            var model = _db.PhieuXuats.Find(maPX);
            return View(model);
        }


        [HttpGet]
        public ActionResult Update(string maPX)
        {
            var model = _db.PhieuXuats.Find(maPX);
            ViewBag.SanPhams = _db.SanPhams.ToList();
            ViewBag.NguoiDung = _db.NguoiDungs.ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(PhieuXuat model, IEnumerable<CTPhieuXuat> cTPhieuXuats)
        {
            try
            {
                _db.PhieuXuats.AddOrUpdate(model);

                foreach (var item in cTPhieuXuats)
                {
                    item.MaPX = model.MaPX;
                    var product = _db.SanPhams.Find(item.MaSP);
                    if (product != null)
                    {
                        product.SoLuong -= item.SoLuong;
                        if (product.SoLuong < 0)
                        {
                            ModelState.AddModelError("", "Số lượng không đủ");
                            return View(model);
                        }
                    }
                    _db.CTPhieuXuats.AddOrUpdate(item);
                }
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.SanPhams = _db.SanPhams.ToList();
                ViewBag.NguoiDung = _db.NguoiDungs.ToList();

                return View(model);
            }
        }


        [HttpPost]
        public ActionResult DeletePhieuXuat(string maPX)
        {
            var model = _db.PhieuXuats.Find(maPX);
            if (model == null) return Json(false);
            if (model.CTPhieuXuats.Count() > 0)
            {
                foreach (var item in model.CTPhieuXuats)
                {
                    var id = item.MaSP.Trim();
                    var sp = _db.SanPhams.Find(id);
                    sp.SoLuong -= item.SoLuong;
                }
                _db.CTPhieuXuats.RemoveRange(model.CTPhieuXuats);
            }
            _db.PhieuXuats.Remove(model);
            _db.SaveChanges();
            return Json(true);
        }
    }
}