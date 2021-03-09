using Eshopper.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshopper.Models.Dao
{
    public class LoaiSanPhamDAO
    {
        DBModels db = new DBModels();

        public List<LoaiSP> GetListLoaiSP()
        {
            //string query = "select *from SanPham";
            //return db.Database.SqlQuery<SanPham>(query).FirstOrDefault();

            return db.LoaiSPs.ToList();
        }

        public LoaiSanPhamDAO()
        {
            db = new DBModels();
        }

        public bool Insert(LoaiSP entity)
        {
            if (!string.IsNullOrEmpty(entity.MaLoaiSP))
            {
                db.LoaiSPs.Add(entity);
                db.SaveChanges();
                return true;
            }
            else
                return false;

        }

        public LoaiSP ViewDetail(string id)
        {
            return db.LoaiSPs.Find(id);
        }
        public bool Update(LoaiSP entity)
        {
            var LoaiSanPham = db.LoaiSPs.Find(entity.MaLoaiSP);
            LoaiSanPham.TenLoai = entity.TenLoai;
            LoaiSanPham.MoTa = entity.MoTa;

            db.SaveChanges();
            return true;
        }

        public bool Delete(string id)
        {
            try
            {
                var LoaiSanPham = db.LoaiSPs.Find(id);
                db.LoaiSPs.Remove(LoaiSanPham);
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