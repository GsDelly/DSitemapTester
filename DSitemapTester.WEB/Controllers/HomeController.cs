using DSitemapTester.BLL.Dtos;
using DSitemapTester.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSitemapTester.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Checkup()
        {
            string selectedUrl = Request["select-url"];

            return RedirectToAction("Index", "Test", new { selectedUrl });
        }
    }
}