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
            var a = Request.Form["timeout"];

            int timeout = Request.Form["timeout"] != string.Empty ? Convert.ToInt32(Request.Form["timeout"]) : 0;
            int testsCount = Request.Form["testsCount"] != string.Empty ? Convert.ToInt32(Request.Form["testsCount"]) : 0;

            return RedirectToAction("Index", "Test", new { selectedUrl, timeout, testsCount });
        }
    }
}