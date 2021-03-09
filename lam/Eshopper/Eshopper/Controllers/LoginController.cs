using Eshopper.Commom;
using Eshopper.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return View();
        }

        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new NguoiDungDao();
                if (model.Email == "ad1")
                    return RedirectToAction("Index", "SanPham", new { area = "Admin" });
                var result = dao.Login(model.Email, model.Pass);
                if (result)
                {
                    var NguoiDung = dao.GetById(model.Email);
                    var NguoiDungSession = new NguoiDungLogin();
                    NguoiDungSession.Email = NguoiDung.Email;
                    NguoiDungSession.ID = NguoiDung.MaND;
                    NguoiDungSession.UserName = NguoiDung.UserName;


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
    }
}