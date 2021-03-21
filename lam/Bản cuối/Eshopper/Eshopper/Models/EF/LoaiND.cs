namespace Eshopper.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiND")]
    public partial class LoaiND
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiND()
        {
            NguoiDungs = new HashSet<NguoiDung>();
        }

        [Key]
        [StringLength(10)]
        public string MaLoaiND { get; set; }

        public string TenLoai { get; set; }

        public int? SoDinhDanh { get; set; }

        [StringLength(10)]
        public string MaKM { get; set; }

        public virtual BangKhuyenMai BangKhuyenMai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguoiDung> NguoiDungs { get; set; }
    }
}
