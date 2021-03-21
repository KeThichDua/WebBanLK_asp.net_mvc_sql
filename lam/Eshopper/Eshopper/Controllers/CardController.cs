using Eshopper.Commom;
using Eshopper.Models.Dao;
using Eshopper.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eshopper.Controllers
{
    public class CardController : Controller
    {

        // GET: Cart
        public ActionResult Index()
        {
            //lấy về giỏ hàng hiện tại
            var cart = Session[CommomConstants.CartSession];
            var list = new List<CartItem>();
            DBModels db = new DBModels();

            // lấy về người đăng nhập hiện tại
            var sessnd = Session[CommomConstants.NguoiDungSession];
            var nd = new NguoiDungLogin();
            nd = (NguoiDungLogin)sessnd;

            // nếu chưa đăng nhập
            if (sessnd == null)
            {
                if (cart != null)
                {
                    list = (List<CartItem>)cart;
                }
            }
            else // đã đăng nhập
            {
                // kiểm tra giỏ hàng trong db
                var gh = new GioHang();
                gh = db.GioHangs.SingleOrDefault(x => x.MaND == nd.ID); // lấy giỏ hàng của người dùng hiện tại

                    //đưa giỏ hàng trong db vào sess cart
                    foreach (var item in db.CTGioHangs)
                    {
                        if (item.MaGH == gh.MaGH)
                        {
                            var t = new CartItem();
                            //var SanPham = new SanPhamDAO().ViewDetail(item.MaSP);
                            t.SanPham = item.SanPham;
                            t.Quantity = (int)item.SoLuong;
                            list.Add(t);
                        }
                    }
                
                
            }
            Session[CommomConstants.CartSession] = list;
            return View(list);
        }

        public ActionResult AddItem(string id, int quantity)
        {
            var SanPham = new SanPhamDAO().ViewDetail(id);
            var cart = Session[CommomConstants.CartSession];

            // lấy về người đăng nhập hiện tại
            var sessnd = Session[CommomConstants.NguoiDungSession];
            var nd = new NguoiDungLogin();
            nd = (NguoiDungLogin)sessnd;

            // nếu chưa đăng nhập
            if (sessnd == null)
            {
                if (cart != null)
                {
                    var list = (List<CartItem>)cart;
                    if (list.Exists(x => x.SanPham.MaSP == id))
                    {
                        foreach (var item in list)
                        {
                            if (item.SanPham.MaSP == id)
                            {
                                item.Quantity += quantity;
                            }
                        }
                    }
                    else
                    {
                        //tạo mới đối tượng cart item
                        var item = new CartItem();
                        item.SanPham = SanPham;
                        item.Quantity = quantity;
                        list.Add(item);
                    }
                    //Gán vào session
                    Session[CommomConstants.CartSession] = list;
                }
                else
                {
                    //tạo mới đối tượng cart item
                    var item = new CartItem();
                    item.SanPham = SanPham;
                    item.Quantity = quantity;
                    var list = new List<CartItem>();
                    list.Add(item);
                    //Gán vào Session
                    Session[CommomConstants.CartSession] = list;
                }
            }
            else //đã đăng nhập
            {
                DBModels db = new DBModels();
                // kiểm tra giỏ hàng trong db
                var gh = new GioHang();
                gh = db.GioHangs.SingleOrDefault(x => x.MaND == nd.ID); // lấy giỏ hàng của người dùng hiện tại

                var item = db.CTGioHangs.Find(gh.MaGH, SanPham.MaSP);
                if (item != null)
                {
                    item.SoLuong += quantity;
                    db.SaveChanges();
                }
                else
                {
                    var tem = new CTGioHang();
                    tem.MaGH = gh.MaGH;
                    tem.MaSP = SanPham.MaSP;
                    tem.SoLuong = quantity;
                    db.CTGioHangs.Add(tem);
                    db.SaveChanges();
                }
            }
                    

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {   
            while(id.Length < 10)
            {
                id += " ";
            }
            var cart = Session[CommomConstants.CartSession];
            var list = (List<CartItem>)cart;
            // lấy về người đăng nhập hiện tại
            var sessnd = Session[CommomConstants.NguoiDungSession];
            var nd = new NguoiDungLogin();
            nd = (NguoiDungLogin)sessnd;

            // nếu chưa đăng nhập
            if (sessnd == null)
            {
                foreach (var item in list.ToList())
                {
                    if (item.SanPham.MaSP == id)
                    {
                        list.Remove(item);
                    }
                }

                Session[CommomConstants.CartSession] = list;
            }
            else
            {
                DBModels db = new DBModels();
                // kiểm tra giỏ hàng trong db
                var gh = new GioHang();
                gh = db.GioHangs.SingleOrDefault(x => x.MaND == nd.ID); // lấy giỏ hàng của người dùng hiện tại

                var item = db.CTGioHangs.Find(gh.MaGH, id);
                db.CTGioHangs.Remove(item);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public ActionResult PayMent()
        //{
        //    var cart = Session[CommomConstants.CartSession];
        //    var list = new List<CartItem>();
        //    if (cart != null)
        //    {
        //        list = (List<CartItem>)cart;
        //    }
        //    return View(list);
        //}

        //public ActionResult Payment1()
        //{
        //    //OrderDao orderdao = ViewBag.OrderDao;
        //    var order = new Order();

        //    order.CreatedDate = DateTime.Now;
        //    order.ShipAddress = ViewBag.ShipAddress;
        //    order.ShipMobile = ViewBag.ShipMobile;
        //    order.ShipName = ViewBag.ShipName;
        //    order.ShipEmail = ViewBag.ShipEmail;
        //    try
        //    {
        //        var id = new OrderDao().Insert(order);
        //        var cart = (List<CartItem>)Session[CommomConstants.CartSession];
        //        var detailDao = new OrderDetailDao();
        //        foreach (var item in cart)
        //        {
        //            var ortherDetail = new OrderDetail();
        //            ortherDetail.id = item.SanPham.ID;
        //            ortherDetail.OrderID = id;
        //            ortherDetail.Price = item.SanPham.Price;
        //            ortherDetail.Quantity = item.Quantity;
        //            detailDao.Insert(ortherDetail);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        //ghi log
        //        return RedirectToAction("Fail");
        //    }
        //    return RedirectToAction("Success");
        //}
        //public ActionResult Success()
        //{
        //    return View();
        //}
        //public ActionResult Fail()
        //{
        //    return View();
        //}
    }
}