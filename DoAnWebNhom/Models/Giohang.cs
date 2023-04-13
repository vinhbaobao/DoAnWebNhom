using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace DoAnWebNhom.Models
{
    public class Giohang
    {
        // tao đối tượng data chứa dữ liệu từ model BanXe đã tạo
        DataClasses1DataContext data = new DataClasses1DataContext();

        public int iMaXe { get; set; }
        public string sTenXe { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhtien
        {
            get { return iSoLuong * dDonGia; }

        }
        // khởi tạo giỏ hàng theo mã xe được truyền vào với số lượng mặc định là 1
        public Giohang(int MaXe)
        {
            iMaXe = MaXe;
            XE xe = data.XEs.Single(n => n.MaXe == iMaXe);
            sTenXe = xe.TenXe;
            sAnhBia = xe.AnhBia;
            dDonGia = double.Parse(xe.GiaBan.ToString());
            iSoLuong = 1;
        }
    }
}