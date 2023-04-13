using DoAnWebNhom.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWebNhom.Controllers
{
    public class QLXeController : Controller
    {
        // GET: QLXe
        DataClasses1DataContext data = new DataClasses1DataContext();
        public ActionResult Xe()
        {
            //int pagesize = 7;
            //int pagenum = (page ?? 1); //if page on null then return 1 else return page
            if (Session["Taikhoanadmin"] == null)
            {
                ViewBag.ThongBao = "Bạn cần đăng nhập trước khi sử dụng chức năng chỉnh sửa!";
                return RedirectToAction("Login", "Admin");
            }
            else
                return View(data.XEs.ToList().OrderByDescending(n => n.MaXe));
        }

        //2. Xem CHI TIẾT Xe 
        public ActionResult Detail(int id)
        {
            //kiem tra dang nhap
            if (Session["Taikhoanadmin"] == null)
            {
                ViewBag.ThongBao = "Bạn cần đăng nhập trước khi sử dụng chức năng chỉnh sửa!";
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var xe = from x in data.XEs where x.MaXe == id select x;
                return View(xe.SingleOrDefault());
            }
        }
        //3. xóa 1 Chiec Xe
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                ViewBag.ThongBao = "Bạn cần đăng nhập trước khi sử dụng chức năng chỉnh sửa!";
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                XE xe = data.XEs.SingleOrDefault(n => n.MaXe == id);
                return View(xe);
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                ViewBag.ThongBao = "Bạn cần đăng nhập trước khi sử dụng chức năng chỉnh sửa!";
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                XE xe = data.XEs.SingleOrDefault(n => n.MaXe == id);
                data.XEs.DeleteOnSubmit(xe);
                data.SubmitChanges();
                return RedirectToAction("Xe", "QLXe");
            }
        }
        //4. thêm mới Xe
        [HttpGet]

        public ActionResult Create()
        {
            if (Session["Taikhoanadmin"] == null)
            {
                ViewBag.ThongBao = "Bạn cần đăng nhập trước khi sử dụng chức năng chỉnh sửa!";
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                // Đưa dữ liệu vào dropdownlist
                // lấy ds từ tabke chủ đề , sắp xếp tăng dần theo tên Nhà Cung cấp chọn lấy giá trị Ma Nha CC hien thị tên nhà CC
                ViewBag.MaLoai = new SelectList(data.LOAIXEs.ToList().OrderBy(n => n.TenLoaiXe).ToList(), "MaLoai", "TenLoaiXe");
                ViewBag.MaNCC = new SelectList(data.NHACUNGCAPs.ToList().OrderBy(n => n.TenNCC).ToList(), "MaNCC", "TenNCC");
                ViewBag.MaMau = new SelectList(data.MAUXEs.OrderBy(n => n.TenMauXe).ToList(), "MaMau", "TenMauXe");
                return View();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(XE xe, HttpPostedFileBase fileupload)
        {
           if (Session["Taikhoanadmin"] == null)
            {
                ViewBag.ThongBao = "Bạn cần đăng nhập trước khi sử dụng chức năng chỉnh sửa!";
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                //Kiem tra duong dan file
                if (fileupload == null)
                {
                    ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                    return View();
                }
                //Them vao CSDL
                else
                {
                    if (ModelState.IsValid)
                    {
                        //Luu ten fie, luu y bo sung thu vien using System.IO;
                        var fileName = Path.GetFileName(fileupload.FileName);
                        //Luu duong dan cua file
                        var path = Path.Combine(Server.MapPath("~/images"), fileName);
                        //Kiem tra hình anh ton tai chua?
                        if (System.IO.File.Exists(path))
                            ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                        else
                        {
                            //Luu hinh anh vao duong dan
                            fileupload.SaveAs(path);
                        }
                        xe.AnhBia = fileName;
                        //Luu vao CSDL
                        data.XEs.InsertOnSubmit(xe);
                        data.SubmitChanges();
                    }
                    return RedirectToAction("Xe");
                }
            }
        }
        //5 ĐIỀU CHỈNH THÔNG TIN 1 Xe
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                ViewBag.ThongBao = "Bạn cần đăng nhập trước khi sử dụng chức năng chỉnh sửa!";
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                XE xe = data.XEs.SingleOrDefault(n => n.MaXe == id);
                ViewBag.MaLoai = new SelectList(data.LOAIXEs.OrderBy(n => n.TenLoaiXe).ToList(), "MaLoai", "TenLoaiXe", xe.MaLoai);
                ViewBag.MaNCC = new SelectList(data.NHACUNGCAPs.OrderBy(n => n.TenNCC).ToList(), "MaNCC", "TenNCC", xe.MaNCC);
                ViewBag.MaMau = new SelectList(data.MAUXEs.OrderBy(n => n.TenMauXe).ToList(), "MaMau", "TenMauXe", xe.MaMau);
                return View(xe);
            }
        }

        [HttpPost, ActionName("Edit")]

        public ActionResult ConfirmEdit(int id)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                ViewBag.ThongBao = "Bạn cần đăng nhập trước khi sử dụng chức năng chỉnh sửa!";
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                XE xe = data.XEs.SingleOrDefault(n => n.MaXe == id);
                UpdateModel(xe);
                data.SubmitChanges();
                return RedirectToAction("Xe","QLXe");
            }
        }

    }
}