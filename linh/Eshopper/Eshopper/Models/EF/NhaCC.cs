namespace Eshopper.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhaCC")]
    public partial class NhaCC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhaCC()
        {
            PhieuNhaps = new HashSet<PhieuNhap>();
        }

        [Key]
        [StringLength(10)]
        [Display(Name = "Mã nhà cung cấp")]
        public string MaNCC { get; set; }

        [Display(Name = "Tên")]
        public string Ten { get; set; }
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [StringLength(50)]
        [Display(Name = "Số điện thoại")]
        public string SDT { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; }
    }
}
