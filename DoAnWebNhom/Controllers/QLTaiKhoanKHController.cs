using DoAnWebNhom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWebNhom.Controllers
{
    public class QLTaiKhoanKHController : Controller
    {
        // GET: QLTaiKhoanKH
        DataClasses1DataContext data = new DataClasses1DataContext();
        
        public ActionResult QLTK()
        {
            if (Session["Taikhoanadmin"] == null)
            {
                ViewBag.ThongBao = "Bạn cần đăng nhập trước khi sử dụng chức năng chỉnh sửa!";
                return RedirectToAction("Login", "Admin");
            }
            else
                return View(data.KHACHHANGs.ToList().OrderByDescending(n => n.MaKH));
        }

        public ActionResult Create()
        {
            if (Session["Taikhoanadmin"] == null)
            {
                ViewBag.ThongBao = "Bạn cần đăng nhập trước khi sử dụng chức năng chỉnh sửa!";
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }
        //post
        [HttpPost, ActionName("Create")]
        public ActionResult comfirmCreate(KHACHHANG tk)
        {
            data.KHACHHANGs.InsertOnSubmit(tk);
            data.SubmitChanges();
            return RedirectToAction("QLTK", "QLTaikhoanKH");
        }

        public ActionResult Delete(int id)
        {
            //kiem tra dang nhap
            if (Session["Taikhoanadmin"] == null)
            {
                ViewBag.ThongBao = "Bạn cần đăng nhập trước khi sử dụng chức năng chỉnh sửa!";
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                KHACHHANG tk = data.KHACHHANGs.SingleOrDefault(p => p.MaKH == id);
                return View(tk);
            }

        }
        //post
        [HttpPost, ActionName("Delete")]
        public ActionResult comfirmDelete(int id)
        {
            KHACHHANG tk = data.KHACHHANGs.SingleOrDefault(p => p.MaKH == id);
            if (tk != null)
            {
                var dem = data.DONDATHANGs.Count(p => p.MaKH == id);
                for (int i = 0; i < dem; i++)
                {
                    DONDATHANG ddh = data.DONDATHANGs.FirstOrDefault(p => p.MaKH == id);
                    data.DONDATHANGs.DeleteOnSubmit(ddh);
                    data.SubmitChanges();
                }
              
                data.SubmitChanges();
                return RedirectToAction("QLTK", "QLTaikhoanKH");
            }
            return HttpNotFound();

        }

        public ActionResult Edit(int id)
        {
            //kiem tra dang nhap
            if (Session["Taikhoanadmin"] == null)
            {
                ViewBag.ThongBao = "Bạn cần đăng nhập trước khi sử dụng chức năng chỉnh sửa!";
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                KHACHHANG tk = data.KHACHHANGs.SingleOrDefault(p => p.MaKH == id);

                return View(tk);
            }
        }
        //post
        [HttpPost, ActionName("Edit")]
        public ActionResult comfirmEdit(int id)
        {
            KHACHHANG tk = data.KHACHHANGs.SingleOrDefault(p => p.MaKH == id);
            if (tk != null)
            {
                UpdateModel(tk);
                data.SubmitChanges();
                return RedirectToAction("QLTK", "QLTaikhoanKH");
            }
            return HttpNotFound();
        }

        public ActionResult Details(int id)
        {
            //kiem tra dang nhap
            if (Session["Taikhoanadmin"] == null)
            {
                ViewBag.ThongBao = "Bạn cần đăng nhập trước khi sử dụng chức năng chỉnh sửa!";
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                KHACHHANG tk = data.KHACHHANGs.SingleOrDefault(p => p.MaKH == id);

                return View(tk);
            }

        }

    }
}