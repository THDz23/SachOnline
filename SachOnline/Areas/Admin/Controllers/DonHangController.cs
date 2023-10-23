using PagedList;
using SachOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SachOnline.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        dbSachOnlineDataContext db = new dbSachOnlineDataContext();
        // GET: Admin/DonHang
        public ActionResult Index(int ?page)
        {
            int iPageNum = (page ?? 1);
            int ipageSize = 5;
            return View(db.DONDATHANGs.ToList().OrderBy(d=>d.MaDonHang).ToPagedList(iPageNum,ipageSize));
        }
        public ActionResult Details(int id)
        {
            /*var ddh = (from dh in db.DONDATHANGs join ct in db.CHITIETDATHANGs on dh.MaDonHang equals ct.MaDonHang
                      where dh.MaDonHang == id select new {
                          dh.MaDonHang,dh.MaKH,dh.NgayDat,dh.NgayGiao,dh.DaThanhToan,
                          ct.MaSach,ct.SoLuong,ct.DonGia
            }).ToList();*/
            var ddh = db.DONDATHANGs.SingleOrDefault(dh => dh.MaDonHang == id);
            return View(ddh);
        }
        public ActionResult Delete(int id)
        {
            var dh = db.DONDATHANGs.SingleOrDefault(d => d.MaDonHang == id);
            if(dh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dh);
        }

        public ActionResult Edit(int id)
        {
            var dh = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (dh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dh);
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConf(int id)
        {
            var ddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            var dh = db.DONDATHANGs.SingleOrDefault(d => d.MaDonHang == id && d.DaThanhToan == true && d.NgayGiao < DateTime.Now);
            if(dh == null)
            {
                ViewBag.ThongBao = "Vui lòng kiểm tra lại thông tin đơn hàng<br>" + "Muốn xóa phải cập nhập lại trạng thái thanh toán và hàng đã được giao đúng hẹn hay chưa" ;
                return View(ddh);
            }
            var ct = db.CHITIETDATHANGs.Where(c => c.MaDonHang == id);
            if(ct.Count() > 0)
            {
                db.CHITIETDATHANGs.DeleteAllOnSubmit(ct);
                db.SubmitChanges();
            }
            db.DONDATHANGs.DeleteOnSubmit(dh);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            var dh = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == int.Parse(f["kDH"]));
            if (ModelState.IsValid)
            {
                dh.DaThanhToan = bool.Parse(f["DaThanhToan"]);
                dh.TinhTrangGiaoHang = int.Parse(f["TinhTrang"]);
                dh.NgayDat = Convert.ToDateTime(f["dNgaydat"]);
                dh.NgayGiao = Convert.ToDateTime(f["dNgaygiao"]);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(dh);
        }
    }
}