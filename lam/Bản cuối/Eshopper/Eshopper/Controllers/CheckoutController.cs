using Eshopper.Commom;
using Eshopper.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eshopper.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Check()
        {
            //lấy về giỏ hàng hiện tại
            var cart = Session[CommomConstants.CartSession];
            var list = new List<CartItem>();
            DBModels db = new DBModels();

            // lấy về người đăng nhập hiện tại
            var sessnd = Session[CommomConstants.NguoiDungSession];
            var nd = new NguoiDungLogin();
            nd = (NguoiDungLogin)sessnd;

            if(nd == null)
            {
                var px = new PhieuXuat();
                px.MaND = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString();
                while (px.MaND.Length > 10)
                {
                    px.MaND = px.MaND.Substring(1, px.MaND.Length - 2);
                }
                px.MaPX = px.MaND;
                px.NgayDat = DateTime.Now.Date;
                px.NgayShip = DateTime.Now.Date;
                db.PhieuXuats.Add(px);

                var kvl = new NguoiDung();
                kvl.MaND = px.MaND;
                db.NguoiDungs.Add(kvl);

                db.SaveChanges();
                //Gán vào session
                Session[CommomConstants.CartSession] = list;
                return RedirectToAction("Index", "Checkout");
            }
            else
            {
                var px = new PhieuXuat();
                px.MaPX = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString();
                while (px.MaPX.Length > 10)
                {
                    px.MaPX = px.MaPX.Substring(1, px.MaPX.Length - 2);
                }
                px.MaND = nd.ID;
                px.NgayDat = DateTime.Now.Date;
                px.NgayShip = DateTime.Now.Date;
                db.PhieuXuats.Add(px);

                db.SaveChanges();
                //Gán vào session
                Session[CommomConstants.CartSession] = list;
                return RedirectToAction("Index", "Checkout");
            }
            //return RedirectToAction("Index", "Card");
        }
    }
}