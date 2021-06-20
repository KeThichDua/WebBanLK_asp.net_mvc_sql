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
                ViewBag.idgh = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString(); // tạo id tạm dựa trên thời gian
                while (ViewBag.idgh.Length > 10)
                {
                    ViewBag.idgh = ViewBag.idgh.Substring(1, ViewBag.idgh.Length - 2);
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
                ViewBag.idgh = gh.MaGH;
                
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
                    var list = (List<CartItem>)cart; //ép kiểu về list cartitem lấy từ session(dữ liệu trong session là kiểu list
                    if (list.Exists(x => x.SanPham.MaSP == id)) //nếu list chứa productid truyền vào
                    {
                        foreach (var item in list) 
                        {
                            if (item.SanPham.MaSP == id) // nếu đã tồn tại id mà thêm một id => tăng thêm số lượng
                            {
                                item.Quantity += quantity;
                            }
                        }
                    }
                    else // add mới một sản phẩm
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
        public ActionResult CheckOutIndex()
        {
            var cart = Session[CommomConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            Session[CommomConstants.CartSession] = list;
            return View(list);
        }

        
        public ActionResult Check()
        {
            //lấy về giỏ hàng hiện tại
            var cart = Session[CommomConstants.CartSession];
            var list = new List<CartItem>();
            list = (List<CartItem>)cart;
            DBModels db = new DBModels();

            // lấy về người đăng nhập hiện tại
            var sessnd = Session[CommomConstants.NguoiDungSession];
            var nd = new NguoiDungLogin();
            nd = (NguoiDungLogin)sessnd;

            if (nd == null)
            {
                var px = new PhieuXuat();
                px.MaND = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString();
                while (px.MaND.Length > 10)
                {
                    px.MaND = px.MaND.Substring(1, px.MaND.Length - 2);
                }
                px.MaPX = px.MaND;
                px.NgayDat = DateTime.Now;
                px.NgayShip = DateTime.Now;
                db.PhieuXuats.Add(px);

                var kvl = new NguoiDung();
                kvl.MaND = px.MaND;
                db.NguoiDungs.Add(kvl);

                

                db.SaveChanges();
                //Gán vào session
                Session[CommomConstants.CartSession] = null;
                return RedirectToAction("Success", "Card");
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

                foreach (var i in list) //list cac san pham trong gio hang
                {
                    db.CTPhieuXuats.Add(new CTPhieuXuat //them tuong ung vao phieu xuat
                    {
                        MaPX = px.MaPX,
                        MaSP = i.SanPham.MaSP,
                        SoLuong = i.Quantity
                    });
                }
                //list.Clear();
                db.SaveChanges();
                //Gán vào session
                Session[CommomConstants.CartSession] = list;
                return RedirectToAction("Success", "Card");
            }
            //return RedirectToAction("Index", "Card");
        }
        public ActionResult Success()
        {
            return View();
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
                    if (item.SanPham.MaSP == id) //kiểm tra id của sản phẩm trong giỏ hàng trùng với id chuyền vào => xóa sản phẩm 
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