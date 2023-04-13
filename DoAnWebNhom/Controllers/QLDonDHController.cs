using DoAnWebNhom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace DoAnWebNhom.Controllers
{
    public class QLDonDHController : Controller
    {
        DataClasses1DataContext data = new DataClasses1DataContext();
        // GET: QLDonDH
        public ActionResult QLDonDH()
        {
            if (Session["Taikhoanadmin"] == null)
            {
                ViewBag.ThongBao = "Bạn cần đăng nhập trước khi sử dụng chức năng chỉnh sửa!";
                return RedirectToAction("Login", "Admin");
            }
            return View(data.DONDATHANGs.ToList().OrderByDescending(n => n.MaDonHang));
        }

        public ActionResult Edit(int id)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                ViewBag.ThongBao = "Bạn cần đăng nhập trước khi sử dụng chức năng chỉnh sửa!";
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                DONDATHANG ddh = data.DONDATHANGs.SingleOrDefault(p => p.MaDonHang == id);
                if (ddh == null)
                {
                    HttpNotFound();
                }

                return View(ddh);
            }
        }
        //HTTP POST
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(FormCollection collection)
        {
            var id = collection["MaDonHang"];

            DONDATHANG ddh = data.DONDATHANGs.SingleOrDefault(P => P.MaDonHang == int.Parse(id));
            if (ddh != null)
            {
                //add ddh vào trong DB
                UpdateModel(ddh);
                data.SubmitChanges();
                return RedirectToAction("QLDonDH", "QLDonDH");
            }
            else
            {
                return HttpNotFound();
            }

        }
        //chi tiet don hang
        //get
        public ActionResult Details(int id)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                ViewBag.ThongBao = "Bạn cần đăng nhập trước khi sử dụng chức năng chỉnh sửa!";
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                List<CHITIETDONHANG> dsdh = (from ctdh in data.CHITIETDONHANGs where ctdh.MaDonHang == id select ctdh).ToList();
                return View(dsdh);
            }
        }


        //xóa
        //get
        public ActionResult Delete(int id)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                ViewBag.ThongBao = "Bạn cần đăng nhập trước khi sử dụng chức năng chỉnh sửa!";
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                DONDATHANG ddh = data.DONDATHANGs.SingleOrDefault(p => p.MaDonHang == id);
                return View(ddh);
            }
        }
        //post
        [HttpPost, ActionName("Delete")]
        public ActionResult ComfirmDelete(int id)
        {
            DONDATHANG ddh = data.DONDATHANGs.SingleOrDefault(p => p.MaDonHang == id);
            if (ddh != null)
            {
                data.DONDATHANGs.DeleteOnSubmit(ddh);
                data.SubmitChanges();
                return RedirectToAction("QLDonDH", "QLDonDH");
            }
            return HttpNotFound();
        }
    }
}