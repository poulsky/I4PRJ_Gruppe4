﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BargainBarterV2.Models;

namespace BargainBarterV2.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string adcatagory, string searchString)
        {
            var GenreLst = new List<string>();

            var GenreQry = from d in db.BarterAdds
                           orderby d.Category
                           select d.Category;

            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.adcatagory = new SelectList(GenreLst);

            var ads = from m in db.BarterAdds
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                ads = ads.Where(s => s.Category.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(adcatagory))
            {
                ads = ads.Where(x => x.Category == adcatagory);
            }

            return View(ads);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // GET: Home/UploadPicture
        public ActionResult UploadPicture()
        {
            return View();
        }

        // POST: BarterAds//5
        [HttpPost]
        public ActionResult UploadPicture(HttpPostedFileBase BarterPicture)
        {
            BarterAdd add=new BarterAdd();
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            if (BarterPicture.ContentLength>0)
            {
                byte[] imgData;
                using (BinaryReader reader = new BinaryReader(BarterPicture.InputStream))
                {
                    imgData = reader.ReadBytes((int) BarterPicture.InputStream.Length);
                }
                add.Picture = new byte[BarterPicture.ContentLength];
                BarterPicture.InputStream.Read(imgData, 0, BarterPicture.ContentLength);


            }
            db.BarterAdds.Add(add);
            db.SaveChanges();
            return RedirectToAction("Create","BarterAds");
        }
    }
}