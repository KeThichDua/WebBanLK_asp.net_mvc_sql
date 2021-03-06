using Eshopper.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshopper.Models.Dao
{
    public class SanPhamDAO
    {
        DBModels db = new DBModels();

        public List<SanPham> GetListSanPham()
        {
            //string query = "select *from SanPham";
            //return db.Database.SqlQuery<SanPham>(query).FirstOrDefault();

            return db.SanPhams.ToList();
        }
    }
}