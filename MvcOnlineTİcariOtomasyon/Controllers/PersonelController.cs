using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTİcariOtomasyon.Models.Siniflar;
namespace MvcOnlineTİcariOtomasyon.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.Personels.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAdı,
                                               Value = x.DepartmanID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult PersonelEkle(Personel p, HttpPostedFileBase PersonelGorsel)
        {
            if (PersonelGorsel != null && PersonelGorsel.ContentLength > 0)
            {
                string dosyaadi = Path.GetFileNameWithoutExtension(PersonelGorsel.FileName);
                string uzanti = Path.GetExtension(PersonelGorsel.FileName);
                string yeniDosyaAdi = dosyaadi + "_" + DateTime.Now.Ticks + uzanti; // Dosya adını benzersiz yap
                string yol = Path.Combine(Server.MapPath("~/Image/"), yeniDosyaAdi);

                // Dosyayı sunucuya kaydet
                PersonelGorsel.SaveAs(yol);

                // Kaydedilecek yolu modele ekle
                p.PersonelGorsel = "/Image/" + yeniDosyaAdi;
            }

            c.Personels.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult PersonelGuncelle(int id)
        {
            var prs = c.Personels.Find(id);

            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAdı,
                                               Value = x.DepartmanID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View("PersonelGuncelle", prs);
        }

        [HttpPost]
        public ActionResult PersonelGuncelle(Personel p, HttpPostedFileBase PersonelGorsel)
        {
            try
            {
                var prs = c.Personels.Find(p.PersonelID); // Güncellenecek personeli bul

                if (prs == null)
                {
                    return HttpNotFound(); // Eğer personel bulunamazsa hata döndür
                }

                prs.PersonelAd = p.PersonelAd;
                prs.PersonelSoyad = p.PersonelSoyad;
                prs.Departmanid = p.Departmanid;

                // Yeni fotoğraf seçildiyse güncelle
                if (PersonelGorsel != null && PersonelGorsel.ContentLength > 0)
                {
                    string dosyaAdi = Path.GetFileNameWithoutExtension(PersonelGorsel.FileName);
                    string uzanti = Path.GetExtension(PersonelGorsel.FileName);
                    string yeniDosyaAdi = dosyaAdi + "_" + DateTime.Now.Ticks + uzanti;
                    string kayitYolu = Path.Combine(Server.MapPath("~/Image/"), yeniDosyaAdi);

                    // Sunucudaki dizinin var olup olmadığını kontrol et, yoksa oluştur
                    string dizinYolu = Server.MapPath("~/Image/");
                    if (!Directory.Exists(dizinYolu))
                    {
                        Directory.CreateDirectory(dizinYolu);
                    }

                    // Dosyayı sunucuya kaydet
                    PersonelGorsel.SaveAs(kayitYolu);

                    // Yeni görsel yolunu modele ata
                    prs.PersonelGorsel = "/Image/" + yeniDosyaAdi;
                }

                c.SaveChanges(); // Veritabanını güncelle
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content("Hata oluştu: " + ex.Message);
            }
        }



        public ActionResult PersonelSil(int id)
        {
            var prs = c.Personels.Find(id);
            c.Personels.Remove(prs);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelListe()
        {
            var sorgu = c.Personels.ToList();
            return View(sorgu);
        }
    }
}