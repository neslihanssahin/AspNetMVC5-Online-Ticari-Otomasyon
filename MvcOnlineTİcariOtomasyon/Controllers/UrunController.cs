using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTİcariOtomasyon.Models.Siniflar;

namespace MvcOnlineTİcariOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        Context c=new Context();
        public ActionResult Index(string p)
        {
            var urunler=from x in c.Uruns select x;
            if (!string.IsNullOrEmpty(p))
            {
                urunler=urunler.Where(y=>y.UrunAd.Contains(p));
            }
            return View(urunler.ToList());
        }

        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text=x.KategoriAd,
                                               Value=x.KategoriID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }

        [HttpPost]
        public ActionResult UrunEkle(Urun u)
        {
           c.Uruns.Add(u);
           c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(int id)
        {
            var deger=c.Uruns.Find(id);
            deger.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UrunGuncelle(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            var deger = c.Uruns.Find(id);
            return View(deger);
        }
        [HttpPost]
        public ActionResult UrunGuncelle(Urun u)
        {
            var urun=c.Uruns.Find(u.UrunID);
            urun.UrunAd=u.UrunAd;
            urun.Marka=u.Marka;
            urun.Stok=u.Stok;
            urun.AlisFiyati=u.AlisFiyati;
            urun.SatisFiyati = u.SatisFiyati;
            urun.Kategoriid=u.Kategoriid;
            urun.UrunGorsel=u.UrunGorsel;
            urun.Durum=u.Durum;
            c.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult UrunListesi()
        {
            var degerler = c.Uruns.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult SatisYap(int id)
        {
            List<SelectListItem> deger3 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelID.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;
            var deger4 = c.Uruns.Find(id);
            ViewBag.dgr4 = deger4.UrunID;
            var deger5=c.Uruns.Find(id);
            ViewBag.dgr5=deger5.SatisFiyati;
            return View();
        }
        [HttpPost]
        public ActionResult SatisYap(SatisHareket s)
        {
            s.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(s);
            c.SaveChanges();
            return RedirectToAction("Index","Satis");
        }


    }
}