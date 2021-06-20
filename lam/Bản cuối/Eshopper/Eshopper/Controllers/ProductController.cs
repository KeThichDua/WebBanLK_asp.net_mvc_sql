using Eshopper.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eshopper.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int? page, string searchString = "") 
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.
 
            DBModels db = new DBModels();
            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            var items = db.SanPhams.ToList();
            if (searchString != string.Empty)
            {
                items = items.Where(x => x.TenSP.Contains(searchString)).ToList();
            }
            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo ID mới có thể phân trang.
            var links = (from l in /*db.SanPhams*/ items select l).OrderBy(x => x.MaSP);

            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 12;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            ViewBag.lsp = db.LoaiSPs;
            ViewBag.ncc = db.NhaCCs;
           
            // 5. Trả về các Link được phân trang theo kích thước và số trang.            
            return View(links.ToPagedList(pageNumber, pageSize));
        }

        // lọc sp theo loại sp, hãng sản xuất, bán chạy hoặc hạ giá
        public ActionResult Loc(string id, int? loai, int? page, string searchString = "")
        {


            DBModels db = new DBModels();
            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            List<SanPham> list = new List<SanPham>();
 

            //var items = db.SanPhams.ToList();
            if (searchString != string.Empty)
            {
                list = db.SanPhams.ToList();
                list = list.Where(x => x.TenSP.Contains(searchString)).ToList();
            }
            else
            {
                if (id != null)
                {
                    while (id.Length < 10)
                    {
                        id += " ";
                    }
                }

                foreach (var item in db.SanPhams)
                {
                    if(loai > 4)
                    {
                        list = db.SanPhams.ToList();
                        break;
                    }
                           
                    switch (loai)
                    {
                        case 1:
                            if (item.MaLoaiSP == id)
                            {
                                list.Add(item);
                            }
                            break;
                        case 2:
                            if(item.MaNCC == id)
                            {
                                list.Add(item);
                            }
                            break;
                        case 3:
                            if(item.SoLuong <= 14)
                            {
                                list.Add(item);
                            }
                            break;
                        case 4:
                            if (item.MaKM != null)
                            {
                                list.Add(item);
                            }
                            break;
                    }
                }
            }
            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo LinkID mới có thể phân trang.
            var links = (from l in list select l).OrderBy(x => x.MaSP);

            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 12;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            ViewBag.lsp = db.LoaiSPs;
            ViewBag.ncc = db.NhaCCs;

            return View(links.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ProductDetail(string id, string ncc)
        {
            while (id.Length < 10)
            {
                id += " ";
            }
            
            DBModels db = new DBModels();
            

            ViewBag.id = id;
            ViewBag.ncc = ncc;
            return View(db);
        }
    }
}