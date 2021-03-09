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

            public SanPhamDAO()
            {
                db = new DBModels();
            }

            public bool Insert(SanPham entity)
            {
                if (!string.IsNullOrEmpty(entity.MaSP))
                {
                    db.SanPhams.Add(entity);
                    db.SaveChanges();
                    return true;
                }
                else
                return false;

        }
        //public IEnumerable<SanPham> ListAllPaging(string searchString, int page, int pageSize)
        //{
        //    IQueryable<SanPham> model = db.SanPhams;
        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        model = model.Where(x => x.SanPhamName.Contains(searchString) || x.Name.Contains(searchString));
        //    }
        //    return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        //}
            //public SanPham GetById(string SanPham)
            //{

            //    return db.SanPhams.SingleOrDefault(x => x.SanPham == SanPham);
            //}
             public SanPham ViewDetail(string id)
            {
                return db.SanPhams.Find(id);
            }
            public bool Update(SanPham entity)
            {
                    var SanPham = db.SanPhams.Find(entity.MaSP);
                    SanPham.TenSP = entity.TenSP;
                    SanPham.SoLuong = entity.SoLuong;
                    SanPham.DonGia = entity.DonGia;
                    SanPham.MoTa = entity.MoTa;
                    SanPham.GiaKM = entity.GiaKM;
                    SanPham.URLAnh = entity.URLAnh;

                    db.SaveChanges();
                    return true;
            }

        public bool Delete(string id)
            {
                try
                {
                    var SanPham = db.SanPhams.Find(id);
                    db.SanPhams.Remove(SanPham);
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