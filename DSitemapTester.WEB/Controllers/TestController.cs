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
        //private IChartService chartService;

        public TestController(ITestService testService)
        {
            this.testService = testService;
            //this.chartService = chartService;
        }
        // GET: Test
        public ActionResult Index(string selectedUrl)
        {
            PresentationWebResourceDto webResource = this.testService.GetTestResults(selectedUrl);
            //this.chartService.GetChartPie(webResource);

            return View(webResource);
        }
    }
}