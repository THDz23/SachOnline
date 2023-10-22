using PagedList;
using SachOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SachOnline.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        dbSachOnlineDataContext db = new dbSachOnlineDataContext();
        // GET: Admin/KhachHang
        public ActionResult Index(int ?page)
        {
            int ipageNum = (page ?? 1);
            int ipageSize = 5;
            return View(db.KHACHHANGs.ToList().OrderBy(kh=>kh.MaKH).ToPagedList(ipageNum,ipageSize));
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(KHACHHANG kh,FormCollection f)
        {
            if (ModelState.IsValid)
            {
                kh.HoTen = f["kTenKhach"];
                kh.TaiKhoan = f["kTaiKhoan"];
                kh.MatKhau = f["kMatKhau"];
                kh.Email = f["kEmail"];
                kh.DiaChi = f["kDiaChi"];
                kh.DienThoai = f["kDT"];
                kh.NgaySinh = Convert.ToDateTime(f["kNgaySinh"]);
                db.KHACHHANGs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Details(int id)
        {
            var khach = db.KHACHHANGs.SingleOrDefault(kh => kh.MaKH == id);
            if(khach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(khach);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var khach = db.KHACHHANGs.SingleOrDefault(kh => kh.MaKH == id);
            if (khach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(khach);
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfi(int id,FormCollection f)
        {
            var kh = db.KHACHHANGs.SingleOrDefault(d => d.MaKH == id);
            var dh = db.DONDATHANGs.Where(h => h.MaKH == id);
            if(dh.Count() > 0)
            {
                ViewBag.ThongBao = "Khách có trong đơn đặt hàng <br> " + "Nếu muốn xóa phải xóa thông tin trong bảng đặt hàng";
                return View(kh);
            }
            db.KHACHHANGs.DeleteOnSubmit(kh);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var kh = db.KHACHHANGs.SingleOrDefault(k => k.MaKH == id);
            if(kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            var kh = db.KHACHHANGs.SingleOrDefault(k => k.MaKH == int.Parse(f["kMaKH"]));
            if (ModelState.IsValid)
            {
                kh.HoTen = f["kTenKhach"];
                kh.TaiKhoan = f["kTk"];
                kh.MatKhau = f["kMK"];
                kh.NgaySinh = Convert.ToDateTime(f["kNgaySinh"]);
                kh.Email = f["kemail"];
                kh.DiaChi = f["kDiaChi"];
                kh.DienThoai = f["kSDT"];
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(kh);
        }
    }
}