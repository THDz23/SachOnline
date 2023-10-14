using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnline.Models;
using PagedList;
using PagedList.Mvc;

namespace SachOnline.Controllers
{
    public class SachOnlineController : Controller
    {
        dbSachOnlineDataContext data = new dbSachOnlineDataContext();
        // GET: SachOnline
        private List<SACH>LaySachMoi(int count)
        {
            return data.SACHes.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }
        private List<SACH> SachBanNhieu(int count)
        {
            return data.SACHes.OrderByDescending(a => a.SoLuongBan).Take(count).ToList();
        }
        public ActionResult Index(int ? page)
        {
            var listSachMoi = LaySachMoi(20);
            int iPageSize = 6;
            int iPageNum = (page ?? 1);
            return View(listSachMoi.ToPagedList(iPageNum,iPageSize));
        }
        public ActionResult ChuDePartial()
        {
            var listChuDe = from cd in data.CHUDEs select cd;
            return PartialView(listChuDe);
        }
        public ActionResult NhaXuatBanPartial()
        {
            var listNXB = from cd in data.NHAXUATBANs select cd;
            return PartialView(listNXB);
        }
        public ActionResult NavPartial()
        {
            return PartialView();
        }
        public ActionResult SachBanNhieuPartial()
        {
            var listSachBanNhieu = SachBanNhieu(6);
            return PartialView(listSachBanNhieu);
        }
        public ActionResult FooterPartial()
        {
            return PartialView();
        }

        public ActionResult SachTheoChuDe(int iMaCD,int ? page)
        {
            ViewBag.MaCD = iMaCD;
            int iSize = 3;
            int iPageNum = (page ?? 1);
            var sach = from s in data.SACHes where s.MaCD == iMaCD select s;
            return View(sach.ToPagedList(iPageNum,iSize));
        }
        public ActionResult SachTheoNXB(int iMaCD,int ? page)
        {
            ViewBag.MaCD = iMaCD;
            int iSize = 3;
            int iPageNum = (page ?? 1);
            var sach = from s in data.SACHes where s.MaNXB == iMaCD select s;
            return View(sach.ToPagedList(iPageNum,iSize));
        }
        public ActionResult ChiTietSach(int id)
        {
            var sach = from s in data.SACHes where s.MaSach == id select s;
            return View(sach.Single());
        }
        public ActionResult LoginLogout()
        {
            return PartialView("LoginLogoutPartial");
        }
    }
}