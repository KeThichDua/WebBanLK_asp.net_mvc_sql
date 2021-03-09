using Eshopper.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshopper.Models.Dao
{
    public class NguoiDungDao
    {
        DBModels db = null;
        public NguoiDungDao()
        {
            db = new DBModels();
        }
        public bool Insert(NguoiDung entity)
        {
            if (!string.IsNullOrEmpty(entity.Pass))
            {
                db.NguoiDungs.Add(entity);
                db.SaveChanges();
                return true;
            }
            else return false;

        }
        //public IEnumerable<NguoiDung> ListAllPaging(string searchString, int page, int pageSize)
        //{
        //    IQueryable<NguoiDung> model = db.NguoiDungs;
        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));
        //    }
        //    return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        //}
        public NguoiDung GetById(string Email)
        {

            return db.NguoiDungs.SingleOrDefault(x => x.Email == Email);
        }
        public NguoiDung ViewDetail(long id)
        {
            return db.NguoiDungs.Find(id);
        }
        //public bool Update(NguoiDung entity)
        //{
        //    try
        //    {
        //        var NguoiDung = db.NguoiDungs.Find(entity.MaND);
        //        NguoiDung.TenND = entity.TenND;
        //        NguoiDung.DiaChi = entity.DiaChi;
        //        NguoiDung.Email = entity.Email;
        //        NguoiDung.SDT = entity.SDT;

        //        db.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        //loging
        //        return false;
        //    }
        //}
        public bool Login(string Email, string pass)
        {
            var result = db.NguoiDungs.SingleOrDefault(x => x.Email == Email && x.Pass == pass);
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //public bool Delete(long id)
        //{
        //    try
        //    {
        //        var NguoiDung = db.NguoiDungs.Find(id);
        //        db.NguoiDungs.Remove(NguoiDung);
        //        db.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
    }
}