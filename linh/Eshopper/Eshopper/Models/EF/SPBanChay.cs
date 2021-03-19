using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Eshopper.Models.EF
{
    [Table("SPBanChay")]
    public class SPBanChay
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string MaPX { get; set; }

        [Display(Name = "Tên sản phẩm")]
        [Required]
        public string TenSP { get; set; }

        public int? SoLuong { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayDat { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayShip { get; set; }

        public virtual ICollection<CTPhieuXuat> CTPhieuXuats { get; set; }

        public virtual PhieuXuat PhieuXuat { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}