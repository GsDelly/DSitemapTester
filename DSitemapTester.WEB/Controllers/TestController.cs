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
            testModel.Timeout = timeout;
            testModel.TestsCount = testsCount;
            testModel.Url = selectedUrl;
            try
            {
                testModel.TestId = this.testService.GetTestId(selectedUrl);
            }
            catch
            {

            }
            return this.View(testModel);
        }

        public void TestCompleted(string connectionId, int urlsCount)
        {
            this.testHub.SendUpdateMessage(connectionId, urlsCount);
        }

        public void UrlsFounded(string connectionId, int urlsCount)
        {
            this.testHub.SendUrlsFoundedMessage(connectionId, urlsCount);
        }

        public void TestDone(string connectionId)
        {
            this.testHub.SendTestDoneMessage(connectionId);
        }

        [HttpPost]
        public ActionResult LoadTestResults(int testId = 0)
        {
            PresentationWebResourceTestDto results = new PresentationWebResourceTestDto();
            int recordsTotal;
            try
            {
                string start = this.Request.Form.GetValues("start").FirstOrDefault();

                int skip = start != null ? Convert.ToInt32(start) : 0;
                recordsTotal = this.testService.Count(testId);

                results = this.testService.GetTest(testId);

                results.Tests = results.Tests
                    .Skip(skip)
                    .Take(1)
                    .ToList();

                return this.Json(
                   new
                   {
                       recordsTotal = recordsTotal,
                       data = results
                   },
                   JsonRequestBehavior.AllowGet);
            }
            catch
            {
                
            }

            return this.Json(
            new
            {
               recordsTotal = 0,
               data = results
            },
            JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RunTest(int testId, int timeout, int testsCount, string connectionId)
        {
            this.testService.OnTestFinished += this.TestCompleted;
            this.testService.OnUrlsFounded += this.UrlsFounded;
            this.testService.OnTestDone += this.TestDone;

            try
            {
                Connections.Add(connectionId, this.cancelTokenSrc);

                Task test = Task.Factory.StartNew(
                    () =>
                    {
                        this.testService.RunTest(testId, timeout, testsCount, this.cancelTokenSrc.Token, connectionId);
                    },
                    this.cancelTokenSrc.Token);
                test.Wait();

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.Conflict);
            }
        }

        [HttpPost]
        public ActionResult StopTest(string connectionId)
        {
            try
            {
                Connections.GetToken(connectionId).Cancel();
                Connections.Remove(connectionId);

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.Conflict);
            } 
        }
    }
}