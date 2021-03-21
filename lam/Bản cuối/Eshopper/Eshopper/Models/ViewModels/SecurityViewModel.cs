using Eshopper.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshopper.Models.ViewModels
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
    public class UserRoleViewModel
    {
        public string MaND { get; set; }
        public string TenND { get; set; }
        public string TaiKhoan { get; set; }
        public List<Role> Roles { get; set; }
    }

}