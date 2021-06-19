using Eshopper.Controllers;
using Eshopper.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Eshopper.Areas.Admin.Controllers
{
    public class ThongKeController : Controller
    {
        // GET: Admin/ThongKe
        private string connectString = ConfigurationManager.ConnectionStrings["DBModels"].ToString();
        private string _path = @"F:\";
        [HttpGet]
        [Authorize(Roles = "employee,admin")]
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
        [Authorize(Roles = "employee,admin")]
        public ActionResult ThongKeForTime()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> XuatExcelForTime(InputTime time)
        {
            var model = new ThongKeViewModel()
            {
                TKNhaps = TKNhapForTime(time),
                TKXuats = TKXuatForTime(time),
                TKTopSanPhams = TKTopSanPhamForTime(time),
                TKXuatTheoUsers = TKXuatTheoUserForTime(time)
            };
            string headerPN = "Thống kê phiếu nhập từ ngày " + time.StartDate.ToString("dd/MM/yyyy") + " đến ngày " + time.EndDate.ToString("dd/MM/yyyy");
            string h1 = "Mã phiếu nhập, Tên nhà cung cấp, tổng số sản phẩm, tổng tiền";
            List<string> strs = new List<string>() { headerPN, h1 };
            foreach (var item in model.TKNhaps)
            {
                string str = item.MaPN + "," + item.TenNCC + "," + item.TotalQuantities.ToString() + "," + item.TotalPrices.ToString("#,##").Replace(',', '.');
                strs.Add(str);
            }
            string headerPx = "Thống kê phiếu xuất từ ngày " + time.StartDate.ToString("dd/MM/yyyy") + " đến ngày " + time.EndDate.ToString("dd/MM/yyyy");
            string h2 = "Mã phiếu xuất, Tên người mua, ngày đặt, ngày ship, tổng sản phẩm, tổng tiền";
            string emty = "    ";
            strs.Add(emty);
            strs.Add(headerPx);
            strs.Add(h2);
            foreach (var item in model.TKXuats)
            {
                if (item.NgayShip != null)
                {
                    string str = item.MaPX + "," + item.TenND + "," + item.NgayDat.Value.ToString("dd/MM/yyyy") + "," +
                    item.NgayShip.Value.ToString("dd/MM/yyyy") + "," + item.TotalQuantities.ToString() + "," + item.TotalPrices.ToString("#,##").Replace(',', '.');
                    strs.Add(str);
                }
                else
                {
                    string str = item.MaPX + "," + item.TenND + "," + item.NgayDat.Value.ToString("dd/MM/yyyy") + "," +
                    item.TotalQuantities.ToString() + "," + item.TotalPrices.ToString("#,##").Replace(',', '.');
                    strs.Add(str);
                }

            }

            string headerSP = "Thống kê top " + model.TKTopSanPhams.Count().ToString() + " sản phẩm bán chạy nhất từ ngày " + time.StartDate.ToString("dd/MM/yyyy") + " đến ngày " + time.EndDate.ToString("dd/MM/yyyy");
            string h3 = "Mã sản phẩm, Tên sản phẩm, tổng sản phẩm, tổng tiền";
            string emty1 = "    ";
            strs.Add(emty1);
            strs.Add(headerSP);
            strs.Add(h3);
            foreach (var item in model.TKTopSanPhams)
            {
                string str = item.MaSP + "," + item.TenSP + "," + item.TotalQuantities.ToString() + "," + item.TotalPrices.ToString("#,##").Replace(',', '.');
                strs.Add(str);
            }

            string headerUser = "Thống kê tổng số lượng mua hàng của khách hàng từ ngày " + time.StartDate.ToString("dd/MM/yyyy") + " đến ngày " + time.EndDate.ToString("dd/MM/yyyy");
            string h4 = "Tên người dùng, số điện thoại, địa chỉ, tổng sản phẩm, tổng tiền";
            string emty2 = "    ";
            strs.Add(emty2);
            strs.Add(headerUser);
            strs.Add(h4);
            foreach (var item in model.TKXuatTheoUsers)
            {
                string str = item.TenND + "," + item.SDT + "," + item.DiaChi + "," + item.TotalQuantities.ToString() + "," + item.TotalPrices.ToString("#,##").Replace(',', '.');
                strs.Add(str);
            }
            await WritingCsv(strs, _path + "ThongKeforTime.csv");
            return Json(true);
        }

        [HttpPost]
        public async Task<ActionResult> XuatExcel()
        {
            var model = new ThongKeViewModel()
            {
                TKNhaps = TKNhap(),
                TKXuats = TKXuat(),
                TKTopSanPhams = TKTopSanPham(),
                TKXuatTheoUsers = TKXuatTheoUser()
            };
            string headerPN = "Thống kê phiếu nhập";
            string h1 = "Mã phiếu nhập, Tên nhà cung cấp, tổng số sản phẩm, tổng tiền";
            List<string> strs = new List<string>() { headerPN, h1 };
            foreach (var item in model.TKNhaps)
            {
                string str = item.MaPN + "," + item.TenNCC + "," + item.TotalQuantities.ToString() + "," + item.TotalPrices.ToString("#,##").Replace(',', '.');
                strs.Add(str);
            }
            string headerPx = "Thống kê phiếu xuất";
            string h2 = "Mã phiếu xuất, Tên người mua, ngày đặt, ngày ship, tổng sản phẩm, tổng tiền";
            string emty = "    ";
            strs.Add(emty);
            strs.Add(headerPx);
            strs.Add(h2);
            foreach (var item in model.TKXuats)
            {
                if (item.NgayShip != null)
                {
                    string str = item.MaPX + "," + item.TenND + "," + item.NgayDat.Value.ToString("dd/MM/yyyy") + "," +
                    item.NgayShip.Value.ToString("dd/MM/yyyy") + "," + item.TotalQuantities.ToString() + "," + item.TotalPrices.ToString("#,##").Replace(',', '.');
                    strs.Add(str);
                }
                else
                {
                    string str = item.MaPX + "," + item.TenND + "," + item.NgayDat.Value.ToString("dd/MM/yyyy") + "," +
                    item.TotalQuantities.ToString() + "," + item.TotalPrices.ToString("#,##").Replace(',', '.');
                    strs.Add(str);
                }

            }

            string headerSP = "Thống kê top " + model.TKTopSanPhams.Count().ToString() + " sản phẩm bán chạy nhất";
            string h3 = "Mã sản phẩm, Tên sản phẩm, tổng sản phẩm, tổng tiền";
            string emty1 = "    ";
            strs.Add(emty1);
            strs.Add(headerSP);
            strs.Add(h3);
            foreach (var item in model.TKTopSanPhams)
            {
                string str = item.MaSP + "," + item.TenSP + "," + item.TotalQuantities.ToString() + "," + item.TotalPrices.ToString("#,##").Replace(',', '.');
                strs.Add(str);
            }

            string headerUser = "Thống kê tổng số lượng mua hàng của khách hàng";
            string h4 = "Tên người dùng, số điện thoại, địa chỉ, tổng sản phẩm, tổng tiền";
            string emty2 = "    ";
            strs.Add(emty2);
            strs.Add(headerUser);
            strs.Add(h4);
            foreach (var item in model.TKXuatTheoUsers)
            {
                string str = item.TenND + "," + item.SDT + "," + item.DiaChi + "," + item.TotalQuantities.ToString() + "," + item.TotalPrices.ToString("#,##").Replace(',', '.');
                strs.Add(str);
            }
            await WritingCsv(strs, _path + "ThongKe.csv");
            return Json(true);
        }

        async Task WritingCsv(List<string> csv, string path)
        {

            StringBuilder text = new StringBuilder();
            foreach (var item in csv)
            {
                text.AppendLine(item);
            }
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            using (var file = new StreamWriter(path, false, Encoding.UTF8))
            {
                await file.WriteAsync(text.ToString());
            }
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