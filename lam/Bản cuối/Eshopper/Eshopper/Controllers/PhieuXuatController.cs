using Eshopper.Commom;
using Eshopper.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eshopper.Controllers
{
    public class PhieuXuatController : BaseController
    {
        // GET: PhieuXuat
        public ActionResult Index(int? page)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.

            // lấy về người đăng nhập hiện tại
            var sessnd = Session[CommomConstants.NguoiDungSession];
            var nd = new NguoiDungLogin();
            nd = (NguoiDungLogin)sessnd;

            DBModels db = new DBModels();
            List<PhieuXuat> list = new List<PhieuXuat>();
            foreach(var item in db.PhieuXuats)
            {
                if (item.MaND == nd.ID)
                    list.Add(item);
            }
            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo LinkID mới có thể phân trang.
            var links = (from l in list select l).OrderBy(x => x.MaPX);

            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 5;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            ViewBag.lsp = db.LoaiSPs;
            ViewBag.ncc = db.NhaCCs;

            // 5. Trả về các Link được phân trang theo kích thước và số trang.            
            return View(links.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(string id)
        {
            while (id.Length < 10)
            {
                id += " ";
            }
            DBModels db = new DBModels();
            var listsp = new List<CartItem>();
            var px = db.PhieuXuats.Find(id);
            ViewBag.ngaydat = px.NgayDat;
            ViewBag.ngayship = px.NgayShip;
            foreach(var item in db.CTPhieuXuats)
            {
                if(item.PhieuXuat.MaPX == px.MaPX)
                {
                    var ci = new CartItem();
                    ci.SanPham = db.SanPhams.Find(item.MaSP);
                    ci.Quantity = item.SoLuong;
                    listsp.Add(ci);
                }
            }
            return View(listsp);
        }

        public ActionResult Cancel (string id)
        {
            while (id.Length < 10)
            {
                id += " ";
            }
            DBModels db = new DBModels();
            var px = db.PhieuXuats.Find(id);
            px.NgayShip = null;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //public ActionResult Delete(string id)
        //{
        //    while (id.Length < 10)
        //    {
        //        id += " ";
        //    }
        //    var cart = Session[CommomConstants.CartSession];
        //    var list = (List<CartItem>)cart;
        //    // lấy về người đăng nhập hiện tại
        //    var sessnd = Session[CommomConstants.NguoiDungSession];
        //    var nd = new NguoiDungLogin();
        //    nd = (NguoiDungLogin)sessnd;

        //    // nếu chưa đăng nhập
        //    if (sessnd == null)
        //    {
        //        foreach (var item in list.ToList())
        //        {
        //            if (item.SanPham.MaSP == id)
        //            {
        //                list.Remove(item);
        //            }
        //        }

        //        Session[CommomConstants.CartSession] = list;
        //    }
        //    return RedirectToAction("Index");
        //}
    }
}