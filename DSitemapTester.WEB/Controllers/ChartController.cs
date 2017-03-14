using DSitemapTester.BLL.Dtos;
using DSitemapTester.BLL.Interfaces;
using DSitemapTester.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSitemapTester.Controllers
{
    public class ChartController : Controller
    {
        private ITestService testService;

        public ChartController(ITestService testService)
        {
            this.testService = testService;
        }
        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LoadChartResults(int testId = 0)
        {
            PresentationWebResourceTestDto test = this.testService.GetTest(testId);

            return this.Json(
               new
               {
                   data = test
               },
               JsonRequestBehavior.AllowGet);
        }
    }
}