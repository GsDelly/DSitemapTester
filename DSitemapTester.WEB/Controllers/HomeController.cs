using DSitemapTester.BLL.Interfaces;
using DSitemapTester.Tester.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSitemapTester.Controllers
{
    public class HomeController : Controller
    {
        private ITestService testService;

        public HomeController(ITestService testService)
        {
            this.testService = testService;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Checkup()
        {
            string selectedUrl = Request["select-url"];
            WebResourceDto webResource = testService.GetTestResults(selectedUrl);
            return View();
        }
    }
}