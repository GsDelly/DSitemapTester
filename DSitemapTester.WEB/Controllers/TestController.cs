using DSitemapTester.BLL.Dtos;
using DSitemapTester.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSitemapTester.Controllers
{
    public class TestController : Controller
    {
        private ITestService testService;

        public TestController(ITestService testService)
        {
            this.testService = testService;
        }
        // GET: Test
        public ActionResult Index(string selectedUrl)
        {
            PresentationWebResourceDto webResource = testService.GetTestResults(selectedUrl);

            return View(webResource);
        }
    }
}