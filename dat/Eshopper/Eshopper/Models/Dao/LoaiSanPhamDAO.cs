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
        

        public List<LoaiSP> GetListLoaiSanPham()
        {
            //string query = "select *from SanPham";
            //return db.Database.SqlQuery<SanPham>(query).FirstOrDefault();
           
            return db.LoaiSPs.ToList();
        }

        
        public void ThemLoaiSP(LoaiSP lsp)
        {
            db.LoaiSPs.Add(lsp);
            db.SaveChanges();
            //var loaisanpham = new LoaiSP()
            //{
            //    MaLoaiSP = lsp.MaLoaiSP,
            //    TenLoai = lsp.TenLoai
            //    //MoTa = lsp.MoTa,
            //    //URLAnh = lsp.URLAnh

            //};
            //db.LoaiSPs.Add(loaisanpham);
            //db.SaveChanges();
            //return loaisanpham;
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}