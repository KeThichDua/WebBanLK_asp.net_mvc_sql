using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshopper.Commom
{
    [Serializable]
    public class NguoiDungLogin
    {
        public string ID { set; get; }
        public string UserName { set; get; }
        public string Email { set; get; }

    }
}