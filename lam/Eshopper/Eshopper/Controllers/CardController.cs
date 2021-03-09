using Eshopper.Commom;
using Eshopper.Models.Dao;
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
            var cart = Session[CommomConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);

        }

        public ActionResult AddItem(string id, int quantity)
        {
            var SanPham = new SanPhamDAO().ViewDetail(id);
            var cart = Session[CommomConstants.CartSession];
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

                foreach (var item in list.ToList())
                {
                    if (item.SanPham.MaSP == id)
                    {
                        list.Remove(item);
                    }
                }

            Session[CommomConstants.CartSession] = list;
            return RedirectToAction("Index");
        }
        public ActionResult Cong(string id)
        {
            while (id.Length < 10)
            {
                id += " ";
            }
            var cart = Session[CommomConstants.CartSession];
            var list = (List<CartItem>)cart;

            foreach (var item in list.ToList())
            {
                if (item.SanPham.MaSP == id)
                {
                        item.Quantity += 1;
                    
                }
            }

            Session[CommomConstants.CartSession] = list;
            return RedirectToAction("Index");
        }
        public ActionResult Tru(string id)
        {
            while (id.Length < 10)
            {
                id += " ";
            }
            var cart = Session[CommomConstants.CartSession];
            var list = (List<CartItem>)cart;

            foreach (var item in list.ToList())
            {
                if (item.SanPham.MaSP == id)
                {
                    if (item.Quantity > 1)
                    {

                        item.Quantity -= 1;
                    }
                    else
                    {
                        list.Remove(item);
                    }
                }
            }

            Session[CommomConstants.CartSession] = list;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult PayMent()
        {
            var cart = Session[CommomConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
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