using DSitemapTester.BLL.Dtos;
using DSitemapTester.BLL.Entities;
using DSitemapTester.BLL.Interfaces;
using DSitemapTester.Hubs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace DSitemapTester.Controllers
{
    public class TestController : Controller
    {
        private ITestService testService;
        private CancellationTokenSource cancelTokenSrc;
        private TestHub testHub;

        public TestController(ITestService testService)
        {
            this.testService = testService;
            this.cancelTokenSrc = new CancellationTokenSource();
            this.testHub = new TestHub();
        }

        // GET: Test
        public ActionResult Index(string selectedUrl, int timeout, int testsCount)
        {
            TestViewModel testModel = new TestViewModel();
            
            testModel.Url = selectedUrl;
            testModel.TestId = this.testService.GetTestId(selectedUrl);
            testModel.Timeout = timeout;
            testModel.TestsCount = testsCount;

            return View(testModel);
        }

        public void TestCompleted(string connectionId, int urlsCount)
        {
            this.testHub.SendUpdateMessage(connectionId, urlsCount);
        }

        public void UrlsFounded(string connectionId, int urlsCount)
        {
            this.testHub.SendUrlsFoundedMessage(connectionId, urlsCount);
        }

        [HttpPost]
        public ActionResult LoadTestResults(int testId = 0)
        {
            string start = this.Request.Form.GetValues("start").FirstOrDefault();
            //string draw = this.Request.Form.GetValues("draw").FirstOrDefault();
            //string start = this.Request.Form.GetValues("start").FirstOrDefault();

            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = this.testService.Count(testId);

            PresentationWebResourceTestDto results = this.testService.GetTest(testId);

            results.Tests = results.Tests
                .Skip(skip)
                .Take(1)
                .ToList();

            return this.Json(
                new
                {
                    //draw = draw,
                    //recordsFiltered = recordsTotal,
                    recordsTotal = recordsTotal,
                    data = results
                },
                JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RunTest(int testId, int timeout, int testsCount, string connectionId)
        {
            this.testService.OnTestFinished += this.TestCompleted;
            this.testService.OnUrlsFounded += this.UrlsFounded;

            Connections.Add(connectionId, cancelTokenSrc);

            Task test = Task.Factory.StartNew(() =>
            {
                this.testService.RunTest(testId, timeout, testsCount, this.cancelTokenSrc.Token, connectionId);
            },
            this.cancelTokenSrc.Token);

            test.Wait();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult StopTest(string connectionId)
        {
            Connections.GetToken(connectionId).Cancel();
            Connections.Remove(connectionId);

            return new HttpStatusCodeResult(HttpStatusCode.OK);  
        }
    }
}