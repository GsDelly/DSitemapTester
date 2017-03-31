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
            return this.View();
        }

        [HttpPost]
        public ActionResult Checkup()
        {
            string selectedUrl = Request["select-url"];
            var a = Request.Form["timeout"];

            int timeout = Request.Form["timeout"] != string.Empty ? Convert.ToInt32(Request.Form["timeout"]) : 0;
            int testsCount = Request.Form["testsCount"] != string.Empty ? Convert.ToInt32(Request.Form["testsCount"]) : 0;
            int dynamicMode = Request.Form["sitemapMode"].ToString() == "0" ? 0 : 1;

            return this.RedirectToAction("Index", "Test", new { selectedUrl, timeout, testsCount, dynamicMode });
        }
    }
}