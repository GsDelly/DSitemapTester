using AutoMapper;
using DSitemapTester.BLL.Configuration;
using DSitemapTester.BLL.Dtos;
using DSitemapTester.BLL.Interfaces;
using DSitemapTester.BLL.Utilities;
using DSitemapTester.DAL.Interfaces;
using DSitemapTester.Entities.Entities;
using DSitemapTester.Tester.Configuration.Connection;
using DSitemapTester.Tester.Dtos;
using DSitemapTester.Tester.Entities;
using DSitemapTester.Tester.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DSitemapTester.BLL.Services
{
    public class TestService : ITestService
    {
        private readonly ISitemapTester tester;
        private readonly ISaveService saver;
        private readonly IUnitOfWork dataUnit;

        public TestService(ISitemapTester tester, ISaveService saver, IUnitOfWork dataUnit)
        {
            this.tester = tester;
            this.saver = saver;
            this.dataUnit = dataUnit;
        }

        public Action<string, int> OnTestFinished { get; set; }
        public Action<string, int> OnUrlsFounded { get; set; }

        public void TestFinished(string coonectionId, int urlsCount)
        {
            this.OnTestFinished(coonectionId, urlsCount);
        }

        public void UrlsFounded(string coonectionId, int totalUrlsCount)
        {
            this.OnUrlsFounded(coonectionId, totalUrlsCount);
        }

        public PresentationWebResourceTestDto GetTest(int testId)
        {
            PresentationAutomapperConfig.Configure();

            WebResourceTest test = this.dataUnit.GetRepository<WebResourceTest>().GetById(testId);

            PresentationWebResourceTestDto presentationTestResults = new PresentationWebResourceTestDto();

            presentationTestResults = Mapper.Map<WebResourceTest, PresentationWebResourceTestDto>(test);

            presentationTestResults.TotalTestsCount = presentationTestResults.Tests.Sum(res => res.TestsCount);
            presentationTestResults.TotalWrongTestsCount = presentationTestResults.Tests.Sum(res => res.WrongTestsCount);
            presentationTestResults.WrongUrls = presentationTestResults.Tests.Where(res => res.WrongTestsCount == res.TestsCount).Count();
            presentationTestResults.SuccessfulUrls = presentationTestResults.Tests.Where(res => res.WrongTestsCount == 0).Count();
            presentationTestResults.TotalUrls = presentationTestResults.Tests.Count();

            return presentationTestResults;
        }

        public int GetTestId(string url)
        {
            url = UrlAdaptor.GetUrl(url);

            WebResourceTest webResourceTest = saver.GetNewTest(url);

            return webResourceTest.Id;
        }

        public void RunTest(int testId,  int timeout, int testsCount, CancellationToken token, string connectionId)
        {
            if (!token.IsCancellationRequested)
            {
                PresentationAutomapperConfig.Configure();
                if (timeout == 0)
                {
                    timeout = ConnectionSettings.GetTimeout();
                }
                if (testsCount == 0)
                {
                    testsCount = ConnectionSettings.GetTestsCount();
                }
                double interval = ConnectionSettings.GetInterval();

                WebResourceTest webResourceTest = this.dataUnit.GetRepository<WebResourceTest>().GetById(testId);

                IEnumerable<string> sUrls = this.tester.Reader.GetSitemapUrls(webResourceTest.WebResource.Url);

                this.UrlsFounded(connectionId, sUrls.Count());

                for (int i = 0; i < sUrls.Count(); i++)
                {
                    if (!token.IsCancellationRequested)
                    {
                        string sUrl = sUrls.ElementAt(i);

                        TesterTest test = this.tester.Analyzer.GetResult(sUrl, timeout, testsCount);

                        saver.SaveTestData(webResourceTest, test);

                        this.TestFinished(connectionId, i + 1);

                        Task.Delay(Convert.ToInt32(interval * 1000)).Wait();
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public int Count(int testId)
        {
            int testsCount = this.dataUnit.GetRepository<WebResourceTest>().GetById(testId).Tests.Count();

            return testsCount;
        }

    }
}
