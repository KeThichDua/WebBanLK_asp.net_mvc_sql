using Eshopper.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshopper.Models.Dao
{
    public class NhaCCDAO
    {
        DBModels db = new DBModels();

        public List<NhaCC> GetListNhaCC()
        {

            return db.NhaCCs.ToList();
        }

        public NhaCCDAO()
        {
            db = new DBModels();
        }

        public bool Insert(NhaCC entity)
        {
            if (!string.IsNullOrEmpty(entity.MaNCC))
            {
                db.NhaCCs.Add(entity);
                db.SaveChanges();
                return true;
            }
            else
                return false;

        }

        public NhaCC ViewDetail(string id)
        {
            return db.NhaCCs.Find(id);
        }
        public bool Update(NhaCC entity)
        {
            var NhaCC = db.NhaCCs.Find(entity.MaNCC);
            NhaCC.Ten = entity.Ten;
            NhaCC.DiaChi = entity.DiaChi;
            NhaCC.SDT = entity.SDT;
            NhaCC.Email = entity.Email;

            db.SaveChanges();
            return true;
        }

        public bool Delete(string id)
        {
            try
            {
                var NhaCC = db.NhaCCs.Find(id);
                db.NhaCCs.Remove(NhaCC);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}