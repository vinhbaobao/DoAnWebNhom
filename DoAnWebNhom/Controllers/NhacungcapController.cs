using DoAnWebNhom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWebNhom.Controllers
{
    public class NhacungcapController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        // GET: Nhacungcap
        public ActionResult QLNcc()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var nhacc = from ncc in db.NHACUNGCAPs select ncc;
                return View(nhacc);
            }
        }
        public ActionResult Details(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var nhacc = from ncc in db.NHACUNGCAPs where ncc.MaNCC == id select ncc;
                return View(nhacc.Single());
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Create(NHACUNGCAP nhacc)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                db.NHACUNGCAPs.InsertOnSubmit(nhacc);
                db.SubmitChanges();
                return RedirectToAction("QLNcc", "Nhacungcap");
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var nhaxuatban = from ncc in db.NHACUNGCAPs where ncc.MaNCC == id select ncc;
                return View(nhaxuatban.Single());
            }
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult Capnhat(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                NHACUNGCAP ncc = db.NHACUNGCAPs.SingleOrDefault(n => n.MaNCC == id);
                UpdateModel(ncc);
                db.SubmitChanges();
                return RedirectToAction("QLNcc", "Nhacungcap");
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var nhacc = from ncc in db.NHACUNGCAPs where ncc.MaNCC == id select ncc;
                return View(nhacc.Single());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xoa(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                NHACUNGCAP nhacc = db.NHACUNGCAPs.SingleOrDefault(n => n.MaNCC == id);
                db.NHACUNGCAPs.DeleteOnSubmit(nhacc);
                db.SubmitChanges();
                return RedirectToAction("QLNcc", "Nhacungcap");
            }
        }
    }
}