using DoAnWebNhom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWebNhom.Controllers
{
    public class QLNhaCCController : Controller
    {
        // GET: QLNhaCC
        DataClasses1DataContext db = new DataClasses1DataContext();
        // GET: QLNhaCC
        public ActionResult Index()
        {
            var dsncc = db.NHACUNGCAPs.ToList().OrderByDescending(n => n.MaNCC);
            return View(dsncc);
        }
        //them
        public ActionResult Create()
        {
            if (Session["username_Admin"] == null)
            {
                ViewBag.ThongBao = "Bạn cần đăng nhập trước khi sử dụng chức năng chỉnh sửa!";
                return RedirectToAction("DangNhapAdmin", "Admin");
            }
            return View();
        }
        //post
        [HttpPost, ActionName("Create")]
        public ActionResult comfirmCreate(NHACUNGCAP ncc)
        {
            db.NHACUNGCAPs.InsertOnSubmit(ncc);
            db.SubmitChanges();
            return RedirectToAction("Index", "QLNhaCC");
        }
        //Xóa
        public ActionResult Delete(int id)
        {
            //kiem tra dang nhap
            if (Session["username_Admin"] == null)
            {
                ViewBag.ThongBao = "Bạn cần đăng nhập trước khi sử dụng chức năng chỉnh sửa!";
                return RedirectToAction("DangNhapAdmin", "Admin");
            }
            else
            {
                NHACUNGCAP nxb = db.NHACUNGCAPs.SingleOrDefault(p => p.MaNCC == id);

                return View(nxb);
            }

        }
        //post
        [HttpPost, ActionName("Delete")]
        public ActionResult comfirmDelete(int id)
        {
            NHACUNGCAP nxb = db.NHACUNGCAPs.SingleOrDefault(p => p.MaNCC == id);
            if (nxb != null)
            {
                db.NHACUNGCAPs.DeleteOnSubmit(nxb);
                db.SubmitChanges();
                return RedirectToAction("Index", "QLNhaCC");
            }
            return HttpNotFound();

        }
        //cap nhat
        public ActionResult Edit(int id)
        {
            //kiem tra dang nhap
            if (Session["username_Admin"] == null)
            {
                ViewBag.ThongBao = "Bạn cần đăng nhập trước khi sử dụng chức năng chỉnh sửa!";
                return RedirectToAction("DangNhapAdmin", "Admin");
            }
            else
            {
                NHACUNGCAP nxb = db.NHACUNGCAPs.SingleOrDefault(p => p.MaNCC == id);

                return View(nxb);
            }
        }
        //post
        [HttpPost, ActionName("Edit")]
        public ActionResult comfirmEdit(int id)
        {
            NHACUNGCAP ncc = db.NHACUNGCAPs.SingleOrDefault(p => p.MaNCC == id);
            if ( ncc != null)
            {
                UpdateModel(ncc);
                db.SubmitChanges();
                return RedirectToAction("Index", "QLNhaCC");
            }
            return HttpNotFound();
        }
    }
}
