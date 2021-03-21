using Eshopper.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshopper.Commom
{
    [Serializable]
    public class CartItem
    {
        public SanPham SanPham { set; get; }
        public int Quantity { set; get; }
    }
}