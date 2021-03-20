using Eshopper.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eshopper.Areas.Admin.Controllers
{
    public class ThongKeController : Controller
    {
        // GET: Admin/ThongKe
        private string connectString = ConfigurationManager.ConnectionStrings["DBModels"].ToString();

        [HttpGet]
        public ActionResult ThongKe()
        {
            var model = new ThongKeViewModel()
            {
                TKNhaps = TKNhap(),
                TKXuats = TKXuat(),
                TKTopSanPhams = TKTopSanPham(),
                TKXuatTheoUsers = TKXuatTheoUser()
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult ThongKeForTime()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThongKeForTimePartialView(InputTime time)
        {
            if (time.StartDate > time.EndDate)
            {
                return Json(false);
            }
            var model = new ThongKeViewModel()
            {
                TKNhaps = TKNhapForTime(time),
                TKXuats = TKXuatForTime(time),
                TKTopSanPhams = TKTopSanPhamForTime(time),
                TKXuatTheoUsers = TKXuatTheoUserForTime(time)
            };
            return View(model);
        }

        List<TKNhapViewModel> TKNhap()
        {
            var data = SelectRows("EXEC ThongKe_Nhap");
            List<TKNhapViewModel> items = new List<TKNhapViewModel>();
            foreach (DataRow dr in data.Rows)
            {

                items.Add(

                    new TKNhapViewModel
                    {

                        MaPN = dr["MaPN"].ToString(),
                        MaNCC = dr["MaNCC"].ToString(),
                        TenNCC = dr["TenNCC"].ToString(),
                        TotalQuantities = int.Parse(dr["TotalQuantities"].ToString()),
                        TotalPrices = decimal.Parse(dr["TotalPrices"].ToString())

                    }
                    );
            }
            return items;
        }
        List<TKXuatTheoUserViewModel> TKXuatTheoUser()
        {
            var data = SelectRows("EXEC ThongKe_Xuat_Theo_Nguoi_Dung"); 
            List<TKXuatTheoUserViewModel> items = new List<TKXuatTheoUserViewModel>();
            foreach (DataRow dr in data.Rows)
            {
                items.Add(
                    new TKXuatTheoUserViewModel
                    {
                        TenND = dr["TenND"].ToString(),
                        SDT = dr["SDT"].ToString(),
                        DiaChi = dr["DiaChi"].ToString(),
                        TotalQuantities = int.Parse(dr["TotalQuantities"].ToString()),
                        TotalPrices = decimal.Parse(dr["TotalPrices"].ToString())
                    }
                    );
            }
            return items;
        }
        List<TKXuatViewModel> TKXuat()
        {
            var data = SelectRows("exec ThongKe_Xuat");
            List<TKXuatViewModel> items = new List<TKXuatViewModel>();
            foreach (DataRow dr in data.Rows)
            {
                items.Add(
                    new TKXuatViewModel
                    {

                        MaPX = dr["MaPX"].ToString(),
                        TenND = dr["TenND"].ToString(),
                        NgayDat = DateTime.Parse(dr["NgayDat"].ToString()),
                        NgayShip = DateTime.Parse(dr["NgayShip"].ToString()),
                        TotalQuantities = int.Parse(dr["TotalQuantities"].ToString()),
                        TotalPrices = decimal.Parse(dr["TotalPrices"].ToString())
                    }
                    );
            }
            return items;
        }
        List<TKTopSanPhamViewModel> TKTopSanPham()
        {

            var data = SelectRows("exec ThongKe_Top_10SP_BanChay");
            List<TKTopSanPhamViewModel> items = new List<TKTopSanPhamViewModel>();
            foreach (DataRow dr in data.Rows)
            {
                items.Add(
                    new TKTopSanPhamViewModel
                    {

                        MaSP = dr["MaSP"].ToString(),
                        TenSP = dr["TenSP"].ToString(),
                        TotalQuantities = int.Parse(dr["TotalQuantities"].ToString()),
                        TotalPrices = decimal.Parse(dr["TotalPrices"].ToString())
                    }
                    );
            }
            return items;
        }

        List<TKNhapViewModel> TKNhapForTime(InputTime time)
        {
            var data = SelectRows("exec ThongKe_Nhap_Khoang_Thoi_gian '" + time.StartDate +"','" + time.EndDate + "'");
            List<TKNhapViewModel> items = new List<TKNhapViewModel>();
            foreach (DataRow dr in data.Rows)
            {

                items.Add(

                    new TKNhapViewModel
                    {

                        MaPN = dr["MaPN"].ToString(),
                        MaNCC = dr["MaNCC"].ToString(),
                        TenNCC = dr["TenNCC"].ToString(),
                        TotalQuantities = int.Parse(dr["TotalQuantities"].ToString()),
                        TotalPrices = decimal.Parse(dr["TotalPrices"].ToString())

                    }
                    );
            }
            return items;
        }
        List<TKXuatViewModel> TKXuatForTime(InputTime time)
        {
            var data = SelectRows("exec ThongKe_Xuat_Khoang_Thoi_gian '" + time.StartDate + "','" + time.EndDate + "'");

            List<TKXuatViewModel> items = new List<TKXuatViewModel>();
            foreach (DataRow dr in data.Rows)
            {
                items.Add(
                    new TKXuatViewModel
                    {

                        MaPX = dr["MaPX"].ToString(),
                        TenND = dr["TenND"].ToString(),
                        NgayDat = DateTime.Parse(dr["NgayDat"].ToString()),
                        NgayShip = DateTime.Parse(dr["NgayShip"].ToString()),
                        TotalQuantities = int.Parse(dr["TotalQuantities"].ToString()),
                        TotalPrices = decimal.Parse(dr["TotalPrices"].ToString())
                    }
                    );
            }
            return items;
        }
        List<TKTopSanPhamViewModel> TKTopSanPhamForTime(InputTime time)
        {
            var data = SelectRows("exec ThongKe_Top_10SP_BanChay_Trong_Khoang_Thoi_gian '" + time.StartDate + "','" + time.EndDate + "'");

            List<TKTopSanPhamViewModel> items = new List<TKTopSanPhamViewModel>();
            foreach (DataRow dr in data.Rows)
            {
                items.Add(
                    new TKTopSanPhamViewModel
                    {

                        MaSP = dr["MaSP"].ToString(),
                        TenSP = dr["TenSP"].ToString(),
                        TotalQuantities = int.Parse(dr["TotalQuantities"].ToString()),
                        TotalPrices = decimal.Parse(dr["TotalPrices"].ToString())
                    }
                    );
            }
            return items;
        }
        List<TKXuatTheoUserViewModel> TKXuatTheoUserForTime(InputTime time)
        {
            var data = SelectRows("exec ThongKe_Xuat_Theo_Nguoi_Dung_Khoang_Thoi_gian '" + time.StartDate + "','" + time.EndDate + "'");

            List<TKXuatTheoUserViewModel> items = new List<TKXuatTheoUserViewModel>();
            foreach (DataRow dr in data.Rows)
            {
                items.Add(
                    new TKXuatTheoUserViewModel
                    {
                        TenND = dr["TenND"].ToString(),
                        SDT = dr["SDT"].ToString(),
                        DiaChi = dr["DiaChi"].ToString(),
                        TotalQuantities = int.Parse(dr["TotalQuantities"].ToString()),
                        TotalPrices = decimal.Parse(dr["TotalPrices"].ToString())
                    }
                    );
            }
            return items;
        }

        private DataTable SelectRows(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(this.connectString))
            {
                DataTable dataset = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(queryString, connection); 
                adapter.Fill(dataset);
                return dataset;
            } 
        }
    }
}