using DoAnWebNhom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWebNhom.Controllers
{
    public class GioHangController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }

        //Lay gio hang
        public List<Giohang> Laygiohang()
        {
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang == null)
            {
                //Neu gio hang chua ton tai thi khoi tao listGiohang
                lstGiohang = new List<Giohang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }
        //Them 1 mặt hang vao gio, nều mặt h2ng đã có thì tăng số lượng =1
        public ActionResult ThemGiohang(int id, string strURL)
        {
            //Lay ra Session gio hang
            List<Giohang> lstGiohang = Laygiohang();
            //Kiem tra sách này tồn tại trong Session["Giohang"] chưa?
            Giohang xe = lstGiohang.Find(n => n.iMaXe == id);
            if (xe == null)
            {
                xe = new Giohang(id);
                lstGiohang.Add(xe);
                return Redirect(strURL);
            }
            else
            {
                xe.iSoLuong++;
                return Redirect(strURL);
            }
        }
        //Tong so luong
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        //Tinh tong tien
        private double TongTien()
        {
            double iTongTien = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongTien = lstGiohang.Sum(n => n.dThanhtien);
            }
            return iTongTien;
        }
        //Hien thi Gio hang
        public ActionResult GioHang()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            List<Giohang> lstGiohang = Laygiohang();
            //Neu gio hang khong co mat hang thi ve trang chủ
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "DoAnWebNhom");
            }
            return View(lstGiohang);
        }
        // tạo partial view de hiển thị thông tin giỏ hàng
        public ActionResult GiohangPartal()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            return PartialView();
        }
        //Xoa Giohang
        public ActionResult XoaGiohang(int id)
        {
            //Lay gio hang tu Session
            List<Giohang> lstGiohang = Laygiohang();
            //Kiem tra sach da co trong Session["Giohang"]
            Giohang xe = lstGiohang.SingleOrDefault(n => n.iMaXe == id);
            //Neu ton tai thi cho sua Soluong
            if (xe != null)
            {
                lstGiohang.RemoveAll(n => n.iMaXe == id);
                return RedirectToAction("Giohang");

            }
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "DoAnWebNhom");
            }
            return RedirectToAction("Giohang");
        }
        //Xoa tat ca thong tin trong Gio hang
        public ActionResult XoaTatcaGiohang()
        {
            //Lay gio hang tu Session
            List<Giohang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("Index", "DoAnWebNhom");
        }
        //Cap nhat Giỏ hàng
        public ActionResult CapnhatGiohang(int id, FormCollection f)
        {

            //Lay gio hang tu Session
            List<Giohang> lstGiohang = Laygiohang();
            //Kiem tra sach da co trong Session["Giohang"]
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iMaXe == id);
            //Neu ton tai thi cho sua Soluong
            if (sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("Giohang");
        }
        [HttpGet]
        public ActionResult Dathang()
        {
            //Kiem tra dang nhap
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "Nguoidung");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "DoAnWebNhom");
            }
            else
            {
                // lấy giỏ hàng từ session
                List<Giohang> lstGiohang = Laygiohang();
                ViewBag.Tongsoluong = TongSoLuong();
                ViewBag.Tongtien = TongTien();
                return View(lstGiohang);
            }
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            //Them Don hang
            DONDATHANG ddh = new DONDATHANG();
            KHACHHANG kh = (KHACHHANG)Session["Taikhoan"];
            List<Giohang> gh = Laygiohang();
            ddh.MaKH = kh.MaKH;
            ddh.NgayDat = DateTime.Now;
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["NgayGiao"]);
            ddh.NgayGiao = DateTime.Parse(ngaygiao);
            ddh.TinhTrangGiao = false;  
            ddh.Dathanhtoan = false;
            db.DONDATHANGs.InsertOnSubmit(ddh);
            db.SubmitChanges();
            //Them chi tiet don hang            
            foreach (var item in gh)
            {
                CHITIETDONHANG ctdh = new CHITIETDONHANG();
                ctdh.MaDonHang = ddh.MaDonHang;
                ctdh.MaXe = item.iMaXe;
                ctdh.SoLuong = item.iSoLuong;
                ctdh.Dongia = (decimal)item.dDonGia;
                db.CHITIETDONHANGs.InsertOnSubmit(ctdh);
            }
            db.SubmitChanges();
            Session["Giohang"] = null;
            return RedirectToAction("Xacnhandonhang", "Giohang");
        }
        public ActionResult Xacnhandonhang()
        {
            return View();
        }
    }
}