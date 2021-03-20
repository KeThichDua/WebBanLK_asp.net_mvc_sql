using Eshopper.Models.EF;
using Eshopper.Models.ViewModels;
using Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Eshopper.Areas.Admin.Controllers
{
    public class SecurityController : Controller
    {
        DBModels db = new DBModels();
        // GET: Admin/Identity
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var res = db.NguoiDungs.SingleOrDefault(x => x.UserName == model.UserName);
            if(res == null)
            {
                ModelState.AddModelError("", @"Sai tài khoản");
                return View(model);
            }
            if (!HtmlHelpers.VerifyHash(model.PassWord, "SHA256", res.MatKhau))
            {
                ModelState.AddModelError("", @"Mật khẩu không chính xác.");
                return View(model);
            }
            string roles = "";
            foreach(var item in res.UserRoles)
            {
                var x = db.Roles.Find(item.RoleId);
                roles += x.Code + ",";
            }
            if(roles.Length > 1)
            {
                roles = roles.Substring(0, roles.Length - 1);
            }
            SetRoles(model.UserName, roles);
            return RedirectToAction("ListUser");
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(NguoiDung model, string confimPass)
        {
            if (model.UserName == "Admin" || model.UserName == "admin")
            {
                ModelState.AddModelError("", @"Không được lấy tên là admin hoặc Admin");
                return View(model);
            }
            var check = db.NguoiDungs.SingleOrDefault(x => x.UserName == model.UserName);
            if(check != null)
            {
                ModelState.AddModelError("", @"Tài khoản đã được sử dụng");
                return View(model);
            }
            if (model.MatKhau != confimPass)
            {
                ModelState.AddModelError("", @"Mật khẩu xác nhận không khớp nhau");
                return View(model);
            }
            model.MatKhau = HtmlHelpers.ComputeHash(model.MatKhau, "SHA256", null);
            model.MaND = getMaND();
            Role role = db.Roles.SingleOrDefault(x => x.Code == "member");
            if (role != null)
            {
                var userRole = new UserRole()
                {
                    RoleId = role.Id,
                    UserId = model.MaND
                };
                db.UserRoles.Add(userRole);
            }
            db.NguoiDungs.Add(model);
            db.SaveChanges();
            return RedirectToAction(nameof(Login));
        }

        string getMaND()
        {
            string maND = "ND";
            for (int i = 1; i < 100000000; i++)
            {
                if (i < 10)
                {
                    maND = "ND0000000" + i.ToString();
                    var item = db.PhieuXuats.Find(maND);
                    if (item == null)
                    {
                        return maND;
                    }
                }
                else
                {
                    if (i < 100)
                    {
                        maND = "ND000000" + i.ToString();
                        var item = db.PhieuXuats.Find(maND);
                        if (item == null)
                        {
                            return maND;
                        }
                    }
                    else
                    {
                        if (i < 1000)
                        {
                            maND = "ND00000" + i.ToString();
                            var item = db.PhieuXuats.Find(maND);
                            if (item == null)
                            {
                                return maND;
                            }
                        }
                        else
                        {
                            if (i < 10000)
                            {
                                maND = "ND0000" + i.ToString();
                                var item = db.PhieuXuats.Find(maND);
                                if (item == null)
                                {
                                    return maND;
                                }
                            }
                            else
                            {
                                if (i < 100000)
                                {
                                    maND = "ND000" + i.ToString();
                                    var item = db.PhieuXuats.Find(maND);
                                    if (item == null)
                                    {
                                        return maND;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return maND;
        }


        public void SetRoles(string userName, string roles)
        {
            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1, userName,
                                                        DateTime.Now, //time begin
                                                        DateTime.Now.AddDays(3), //timeout
                                                        false, roles,
                                                        FormsAuthentication.FormsCookiePath);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            if (ticket.IsPersistent)
                cookie.Expires = ticket.Expiration;

            //FormsAuthenticationTicket item = 

            Response.Cookies.Add(cookie);
        }
        string GetUserNameFromCookie()
        {
            string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
            string UserName = ticket.Name; //You have the UserName!
            return UserName;
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("ListUser");
        }

        [HttpGet]
        public ActionResult ListUser()
        {
            var items = db.NguoiDungs.ToList();
            return View(items);
        }

        [HttpGet]
        public ActionResult GetRoleForUser(string maND)
        {
            var item = db.NguoiDungs.FirstOrDefault(x => x.MaND == maND);
            if(item != null)
            {
                ViewBag.Roles = db.Roles.ToList();

                return View(item);
            }
            return HttpNotFound("Khong tim thay");
        }

        [HttpPost]
        public ActionResult GetRoleForUser(NguoiDung model, List<Guid> RoleIds)
        {
            //var userRoles = db.UserRoles.Where(x => x.UserId == model.MaND).ToList();
            foreach(var id in RoleIds)
            {
                var userRole = new UserRole()
                {
                    RoleId = id,
                    UserId = model.MaND
                };
                db.UserRoles.AddOrUpdate(userRole);
            }
            db.SaveChanges();
            return RedirectToAction("ListUser");
        }

    }
}