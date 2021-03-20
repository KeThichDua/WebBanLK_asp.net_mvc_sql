namespace Eshopper.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NguoiDung")]
    public partial class NguoiDung
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NguoiDung()
        {
            BinhLuans = new HashSet<BinhLuan>();
            PhieuXuats = new HashSet<PhieuXuat>();
            UserRoles = new HashSet<UserRole>();
        }

        [Key]
        [StringLength(10)]
        public string MaND { get; set; }

        [StringLength(50)]
        public string SDT { get; set; }

        [StringLength(10)]
        public string GioiTinh { get; set; }

        public int? Tuoi { get; set; }

        public string DiaChi { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Pass { get; set; }

        [StringLength(10)]
        public string MaLoaiND { get; set; }

        public string TenND { get; set; }

        [StringLength(250)]
        public string MatKhau { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }

        public virtual LoaiND LoaiND { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuXuat> PhieuXuats { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
