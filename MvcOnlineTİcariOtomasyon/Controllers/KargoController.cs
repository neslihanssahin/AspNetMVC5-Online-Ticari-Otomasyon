using MvcOnlineTİcariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTİcariOtomasyon.Controllers
{
    public class KargoController : Controller
    {
        // GET: Kargo
         Context c =new Context();
        public ActionResult Index()
        {
            var kargolar=c.kargoDetays.ToList();
            return View(kargolar);
        }
        [HttpGet]
        public ActionResult YeniKargo()
        {
            return View();
        }


        [HttpPost]
        public ActionResult YeniKargo(KargoDetay d)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    c.kargoDetays.Add(d);
                    c.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Hata mesajı görüntüleme veya loglama işlemi yapılabilir
                    ModelState.AddModelError("", "Bir hata oluştu: " + ex.Message);
                }
            }

            // Hata durumunda form yeniden gösterilir
            return View(d);
        }
       
    }
}