using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshopper.Models.ViewModels
{
    public class ThongKeViewModel
    {
        public List<TKNhapViewModel> TKNhaps { get; set; }
        public List<TKXuatViewModel> TKXuats { get; set; }
        public List<TKTopSanPhamViewModel> TKTopSanPhams { get; set; }
        public List<TKXuatTheoUserViewModel> TKXuatTheoUsers { get; set; }
    }

    public class InputTime
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class TKNhapViewModel
    {
        public string MaPN { get; set; }
        public string MaNCC { get; set; }
        public string TenNCC { get; set; }
        public int TotalQuantities { get; set; }
        public decimal TotalPrices { get; set; }
        
    }
    public class TKXuatViewModel
    {
        public string MaPX { get; set; }
        public string TenND { get; set; }
        public DateTime? NgayDat { get; set; }
        public DateTime? NgayShip { get; set; }
        public int TotalQuantities { get; set; }
        public decimal TotalPrices { get; set; }

    }
    public class TKTopSanPhamViewModel
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public int TotalQuantities { get; set; }
        public decimal TotalPrices { get; set; }
    }
    public class TKXuatTheoUserViewModel
    {
        public string TenND { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public int TotalQuantities { get; set; }
        public decimal TotalPrices { get; set; }
    }
}