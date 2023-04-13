using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using DoAnWebNhom.Models;
using PagedList;
using PagedList.Mvc;
namespace DoAnWebNhom.Controllers
{
    public class DoAnWebNhomController : Controller
    {
        // GET: BookStore
        //Tao doi tuong data chưa dữ liệu từ model dbBanXe đã tạo. 
        DataClasses1DataContext data = new DataClasses1DataContext();

        // Ham lay n quyen sach moi
        private List<XE> Layxemoi(int count)
        {
            //Sắp xếp sách theo ngày cập nhật, sau đó lấy top @count 
            return data.XEs.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }
        public ActionResult Index(int? page)
        {
            //Số mẫu tin tối đa cho 1 trang
            int pageSize = 5;
       
            //Nếu biến page là null thì pagenum=1, ngược pagenum = page.
            int pageNum = (page ?? 1);
            //Lấy top 5 Album bán chạy nhất
            var xemoi = Layxemoi(30);
            return View(xemoi.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Loaixe()
        {
            var loaixe = from lx in data.LOAIXEs select lx;
            return PartialView(loaixe);
        }
        public ActionResult Nhacungcap()
        {
            var nhacungcap = from nxb in data.NHACUNGCAPs select nxb;
            return PartialView(nhacungcap);
        }
        public ActionResult Xetheoloai(int id)
        {
            var xe = from s in data.XEs where s.MaLoai == id select s;
            return View(xe);
        }
        public ActionResult XetheoNCC(int id)
        {
            var xe = from s in data.XEs where s.MaNCC == id select s;
            return View(xe);
        }
        public ActionResult Chitietxe(int id)
        {
            var xe = from s in data.XEs
                       where s.MaXe == id
                       select s;
            return View(xe.Single());
        }
        public ActionResult Search(string searchString)
        {

            var links = from l in data.XEs
                        select l;

            if (!String.IsNullOrEmpty(searchString))
            {
                links = links.Where(s => s.TenXe.Contains(searchString));
            }

            return View(links);
        }
        public ActionResult Lienhe()
        {
            return View();
        }
    }
}   