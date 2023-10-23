using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnline.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
namespace SachOnline.Areas.Admin.Controllers
{
    public class NhaXuatBanController : Controller
    {
        dbSachOnlineDataContext db = new dbSachOnlineDataContext();
        // GET: Admin/Sach
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.NHAXUATBANs.ToList().OrderBy(n => n.MaNXB).ToPagedList(iPageNum, iPageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(NHAXUATBAN nxb, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                nxb.TenNXB = f["nTenNXB"];
                nxb.DiaChi = f["nDiaChi"];
                nxb.DienThoai = f["nSDT"];
                db.NHAXUATBANs.InsertOnSubmit(nxb);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Details(int id)
        {
            var nxb = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nxb);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var nxb = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nxb);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfi(int id, FormCollection f)
        {
            var n = db.NHAXUATBANs.SingleOrDefault(d => d.MaNXB == id);
            var sach = db.SACHes.Where(h => h.MaNXB == id);
            if (sach.Count() > 0)
            {
                ViewBag.ThongBao = "Nhà xuất bản có trong sách <br> " + "Nếu muốn xóa phải xóa thông tin trong bảng sách";
                return View(n);
            }
            db.NHAXUATBANs.DeleteOnSubmit(n);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var nxb = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nxb);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            var nxb = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == int.Parse(f["nMaNXB"]));
            if (ModelState.IsValid)
            {
                nxb.TenNXB = f["nTenNXB"];
                nxb.DiaChi = f["nDiaChi"];
                nxb.DienThoai = f["nSDT"];
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(nxb);
        }
    }
}