using Eshopper.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshopper.Models.Dao
{
    public class LoaiNDDAO
    {
        DBModels db = new DBModels();

        public List<LoaiND> GetListLoaiND()
        {

            return db.LoaiNDs.ToList();
        }

        public LoaiNDDAO()
        {
            db = new DBModels();
        }

        public bool Insert(LoaiND entity)
        {
            if (!string.IsNullOrEmpty(entity.MaLoaiND))
            {
                db.LoaiNDs.Add(entity);
                db.SaveChanges();
                return true;
            }
            else
                return false;

        }

        public LoaiND ViewDetail(string id)
        {
            return db.LoaiNDs.Find(id);
        }
        public bool Update(LoaiND entity)
        {
            var LoaiND = db.LoaiNDs.Find(entity.MaLoaiND);
            LoaiND.TenLoai = entity.TenLoai;
            LoaiND.SoDinhDanh = entity.SoDinhDanh;

            db.SaveChanges();
            return true;
        }

        public bool Delete(string id)
        {
            try
            {
                var LoaiND = db.LoaiNDs.Find(id);
                db.LoaiNDs.Remove(LoaiND);
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