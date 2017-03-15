﻿using DSitemapTester.BLL.Dtos;
using DSitemapTester.BLL.Interfaces;
using DSitemapTester.Hubs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
        public ActionResult Index(string selectedUrl, int timeout, int testsCount)
        {
            TestViewModel testModel = new TestViewModel();

            testModel.Url = selectedUrl;
            testModel.TestId = this.testService.GetTestId(selectedUrl);
            testModel.Timeout = timeout;
            testModel.TestsCount = testsCount;
            //testModel.TestId = this.testService.RunTest(selectedUrl, timeout, testsCount);
            return View(testModel);
        }

        [HttpPost]
        public ActionResult LoadTestResults(int testId = 0)
        {
            string draw = this.Request.Form.GetValues("draw").FirstOrDefault();
            string start = this.Request.Form.GetValues("start").FirstOrDefault();
            string length = this.Request.Form.GetValues("length").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = this.testService.Count(testId);

            PresentationWebResourceTestDto results = this.testService.GetTest(testId);
            
            results.Tests = results.Tests
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            return this.Json(
                new
                {
                    draw = draw,
                    recordsFiltered = recordsTotal,
                    recordsTotal = recordsTotal,
                    data = results.Tests
                },
                JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RunTest(int testId, int timeout, int testsCount)
        {
            this.testService.OnTestFinished += this.TestCompleted;
            Task test = Task.Factory.StartNew(() =>
            {
                this.testService.RunTest(testId, timeout, testsCount);
            });

            test.Wait();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public void TestCompleted()
        {
            Debug.WriteLine("Debug");
            TestHub.SendUpdateMessage();
        }
    }
}