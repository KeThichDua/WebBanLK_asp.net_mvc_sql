namespace Eshopper.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserRole")]
    public partial class UserRole
    {
        [Key]
        [Column(Order = 0)]
        public Guid RoleId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string UserId { get; set; }

        [StringLength(250)]
        public string Note { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }

        public virtual Role Role { get; set; }
    }
}
