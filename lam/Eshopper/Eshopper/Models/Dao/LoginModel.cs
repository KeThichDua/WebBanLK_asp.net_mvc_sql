using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eshopper.Models.Dao
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Mời nhập email")]
        public string Email { set; get; }

        [Required(ErrorMessage = "Mời nhập mật khẩu")]
        public string Pass { set; get; }

        public bool RememberMe { set; get; }
    }
}