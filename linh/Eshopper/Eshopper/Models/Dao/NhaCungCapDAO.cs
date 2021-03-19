using Eshopper.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshopper.Models.Dao
{
    public class NhaCungCapDAO
    {
        DBModels db = new DBModels();

        public List<NhaCC> GetListNhaCungCap()
        {
            return db.NhaCCs.ToList();
        }
    }
}