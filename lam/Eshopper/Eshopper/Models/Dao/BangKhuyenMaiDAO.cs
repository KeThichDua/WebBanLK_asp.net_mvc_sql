using Eshopper.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshopper.Models.Dao
{
    public class BangKhuyenMaiDAO
    {
        DBModels db = new DBModels();

        public List<BangKhuyenMai> GetListBangKhuyenMai()
        {

            return db.BangKhuyenMais.ToList();
        }

        public BangKhuyenMaiDAO()
        {
            db = new DBModels();
        }

        public bool Insert(BangKhuyenMai entity)
        {
            if (!string.IsNullOrEmpty(entity.MaKM))
            {
                db.BangKhuyenMais.Add(entity);
                db.SaveChanges();
                return true;
            }
            else
                return false;

        }

        public BangKhuyenMai ViewDetail(string id)
        {
            return db.BangKhuyenMais.Find(id);
        }
        public bool Update(BangKhuyenMai entity)
        {
            var BangKhuyenMai = db.BangKhuyenMais.Find(entity.MaKM);
            BangKhuyenMai.TienKM = entity.TienKM;
            BangKhuyenMai.TiLeKM = entity.TiLeKM;

            db.SaveChanges();
            return true;
        }

        public bool Delete(string id)
        {
            try
            {
                var BangKhuyenMai = db.BangKhuyenMais.Find(id);
                db.BangKhuyenMais.Remove(BangKhuyenMai);
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