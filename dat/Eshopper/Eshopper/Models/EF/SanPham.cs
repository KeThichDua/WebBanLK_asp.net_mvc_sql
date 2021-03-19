namespace Eshopper.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            BinhLuans = new HashSet<BinhLuan>();
            CTGioHangs = new HashSet<CTGioHang>();
            CTPhieuNhaps = new HashSet<CTPhieuNhap>();
            CTPhieuXuats = new HashSet<CTPhieuXuat>();
        }

        [Key]
        [StringLength(10)]
        [Display(Name = "Mã sản phẩm")]
        public string MaSP { get; set; }

        [Display(Name = "Tên sản phẩm")]
        public string TenSP { get; set; }
        [Display(Name = "Số lượng")]
        public int? SoLuong { get; set; }
        [Display(Name = "Đơn giá")]
        public int? DonGia { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }
        [Display(Name = "Giá khuyến mãi")]
        public int? GiaKM { get; set; }
        [Display(Name = "URL ảnh")]
        public string URLAnh { get; set; }
        [Display(Name = "Mã loại sản phẩm")]
        [StringLength(10)]
        public string MaLoaiSP { get; set; }
        [Display(Name = "Mã khuyến mãi")]
        [StringLength(10)]
        public string MaKM { get; set; }

        public virtual BangKhuyenMai BangKhuyenMai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTGioHang> CTGioHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTPhieuNhap> CTPhieuNhaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTPhieuXuat> CTPhieuXuats { get; set; }

        public virtual LoaiSP LoaiSP { get; set; }
    }
}
