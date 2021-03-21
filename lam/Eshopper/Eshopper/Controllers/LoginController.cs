using Eshopper.Commom;
using Eshopper.Models.Dao;
using Eshopper.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Eshopper.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            Session[CommomConstants.NguoiDungSession] = null;
            Session[CommomConstants.CartSession] = null;
            Session[CommomConstants.AdminSession] = null;
            return View();
        }

        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                DBModels db = new DBModels();
                var dao = new NguoiDungDao();
                var admin = db.Admins.FirstOrDefault(x => x.UserAd == model.Email && x.Pass == model.Pass);
                if (admin != null)
                    return RedirectToAction("Index", "SanPham", new { area = "Admin" });

                var result = dao.Login(model.Email, model.Pass);
                if (result)
                {
                    //DBModels db = new DBModels();
                    //var admin = db.Admins.FirstOrDefault(x => x.Email == model.Email && x.Pass == model.Pass);
                    //if (admin != null)
                    //{
                        
                    //    var AdminSess = new NguoiDungLogin();
                    //    AdminSess.Email = admin.Email;
                    //    AdminSess.ID = admin.MaAdmin;
                    //    AdminSess.UserName = admin.UserAd;
                    //    Session.Add(CommomConstants.AdminSession, AdminSess);
                    //    return RedirectToAction("Index", "SanPham", new { area = "Admin" });
                    //}

                    var NguoiDung = dao.GetById(model.Email);
                    var NguoiDungSession = new NguoiDungLogin();
                    NguoiDungSession.Email = NguoiDung.Email;
                    NguoiDungSession.ID = NguoiDung.MaND;
                    NguoiDungSession.UserName = NguoiDung.UserName;

                    // kiểm tra giỏ hàng trong db
                    GioHang gh = db.GioHangs.SingleOrDefault(x => x.MaND == NguoiDung.MaND); // lấy giỏ hàng của người dùng hiện tại

                    if (gh == null)
                    {
                        gh = new GioHang();
                        gh.MaGH = NguoiDungSession.ID;
                        while (gh.MaGH.Length > 10)
                        {
                            gh.MaGH = gh.MaGH.Substring(1, gh.MaGH.Length - 2);
                        }
                        gh.MaND = NguoiDung.MaND;
                        db.GioHangs.Add(gh);
                        db.SaveChanges();
                    }

                    Session.Add(CommomConstants.NguoiDungSession, NguoiDungSession);
                   
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Mật khẩu hoặc email không đúng.");
                }
            }
            return View("Index");
        }

        // POST: /Login/Create
        [HttpPost]
        public ActionResult Create(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new NguoiDungDao();

                NguoiDung nd = new NguoiDung();
                nd.TenND = model.TenND;
                nd.Email = model.Email;
                nd.Pass = model.Pass;
                nd.UserName = model.UserName;
                nd.MaND = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString();
                while(nd.MaND.Length > 10)
                {
                    nd.MaND = nd.MaND.Substring(1, nd.MaND.Length - 2);
                }
                nd.MaLoaiND = "lnd2";
                NguoiDung kt = dao.ViewDetail(nd.MaND);
                if(kt != null)
                {
                    ModelState.AddModelError("", "Người dùng đã tồn tại!");
                }
                else
                {
                    NguoiDung kte = dao.GetById(nd.Email);
                    if(kte != null)
                    {
                        ModelState.AddModelError("", "Email đã đăng ký!");
                    }
                    else
                    {
                        bool id = dao.Insert(nd);
                        if (id)
                        {
                            var NguoiDungSession = new NguoiDungLogin();
                            NguoiDungSession.Email = nd.Email;
                            NguoiDungSession.ID = nd.MaND;
                            NguoiDungSession.UserName = nd.UserName;


                            Session.Add(CommomConstants.NguoiDungSession, NguoiDungSession);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Tạo mới người dùng thất bại!");
                        }
                    }

                }

            }
            return View("Index");
        }
    }
}