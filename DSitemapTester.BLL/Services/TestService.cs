using AutoMapper;
using DSitemapTester.BLL.Configuration;
using DSitemapTester.BLL.Dtos;
using DSitemapTester.BLL.Entities;
using DSitemapTester.BLL.Interfaces;
using DSitemapTester.BLL.Utilities;
using DSitemapTester.DAL.Interfaces;
using DSitemapTester.Entities.Entities;
using DSitemapTester.Tester;
using DSitemapTester.Tester.Configuration.Connection;
using DSitemapTester.Tester.Entities;
using DSitemapTester.Tester.Interfaces;
using DSitemapTester.Tester.PublicSuffix.DomainParser;
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
        private HtmlTester htmlTester = new HtmlTester();

        public TestService(ISitemapTester tester, ISaveService saver, IUnitOfWork dataUnit)
        {
            this.tester = tester;
            this.saver = saver;
            this.dataUnit = dataUnit;
        }

        public Action<string, int> OnTestFinished { get; set; }
        public Action<string, int> OnUrlsFound { get; set; }
        public Action<string> OnTestDone { get; set; }

        public void TestDone(string connectionId)
        {
            Connections.Remove(connectionId);
            this.OnTestDone(connectionId);
        }

        public void TestFinished(string connectionId, int urlsCount)
        {
            this.OnTestFinished(connectionId, urlsCount);
        }

        public void UrlsFound(string connectionId, int totalUrlsCount)
        {
            this.OnUrlsFound(connectionId, totalUrlsCount);
        }

        public PresentationWebResourceTestDto GetTest(int testId)
        {
            try
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
            catch
            {
                throw;
            }
        }

        public int GetTestId(string url)
        {
            url = UrlAdaptor.GetUrl(url);

            try
            {
                WebResourceTest webResourceTest = this.saver.GetNewTest(url);
                return webResourceTest.Id;
            }
            catch
            {
                throw;
            }
        }

        public void RunTest(int testId, int timeout, int testsCount, CancellationToken token, string connectionId, bool dynamicMode)
        {
            try
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

                WebResourceTest webResourceTest;

                webResourceTest = this.dataUnit.GetRepository<WebResourceTest>().GetById(testId);

                string globalUrl = webResourceTest.WebResource.Url;

                if (!dynamicMode)
                {
                    IEnumerable<string> urls = this.tester.Reader.GetSitemapUrls(globalUrl);

                    //change ! to ''
                    if (urls.Any())
                    {
                        this.RunSitemapTest(webResourceTest, urls, timeout, testsCount, interval, token, connectionId);
                    }
                    else
                    {
                        //code for htmltester
                        this.RunHtmlTest(webResourceTest, timeout, interval, testsCount, token, connectionId);
                    }
                }
                else
                {
                    this.RunHtmlTest(webResourceTest, timeout, interval, testsCount, token, connectionId);
                }
            }
            catch
            {

            }
            this.TestDone(connectionId);
        }    

        private void RunSitemapTest(WebResourceTest webResourceTest, IEnumerable<string> urls, 
            int timeout, int testsCount, double interval, CancellationToken token, string connectionId)
        {
            this.UrlsFound(connectionId, urls.Count());

            for (int i = 0; i < urls.Count(); i++)
            {
                if (!token.IsCancellationRequested)
                {
                    string url = urls.ElementAt(i);

                    TesterTest test = this.tester.Analyzer.GetResult(url, timeout, testsCount);

                    bool saving = this.saver.SaveTestData(webResourceTest, test);

                    if (saving)
                    {
                        this.TestFinished(connectionId, i + 1);
                    }

                    Task.Delay(Convert.ToInt32(interval * 1000)).Wait();
                }
                else
                {
                    break;
                }
            }
        }

        public void RunHtmlTest(WebResourceTest webResourceTest, int timeout, double interval, int testsCount,
            CancellationToken token, string connectionId)
        {
            string globalUrl = webResourceTest.WebResource.Url;

            IList<string> urlsToTest = new List<string>();
            List<string> testedUrls = new List<string>();

            Domain globalDomain = htmlTester.GetUrlDomain(globalUrl);
            urlsToTest.Add(globalUrl);

            while ((urlsToTest.Any()) && (!token.IsCancellationRequested))
            {
                this.UrlsFound(connectionId, urlsToTest.Count() + testedUrls.Count());
                urlsToTest = this.HtmlTest(webResourceTest, globalDomain, urlsToTest, ref testedUrls, timeout, interval, testsCount, token, connectionId);
            }
        }

        private IList<string> HtmlTest(WebResourceTest webResourceTest, Domain domain, IList<string> urlsForTest, 
            ref List<string> testedUrls, int timeout, double interval, int testsCount, CancellationToken token, string connectionId)
        {
            IList<double> responseTimes;
            DateTime responseDate;

            IList<string> urls = new List<string>();
            IList<string> elementUrls = new List<string>();

            foreach (var url in urlsForTest)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                elementUrls = htmlTester.GetUrls(url, domain, testsCount, token, out responseTimes, out responseDate);

                if (token.IsCancellationRequested)
                {
                    break;
                }

                foreach (string elementUrl in elementUrls)
                {
                    if (!testedUrls.Contains(elementUrl) && !urlsForTest.Contains(elementUrl) && !urls.Contains(elementUrl))
                    {
                        urls.Add(elementUrl);
                    }
                }

                if (!testedUrls.Contains(url))
                {
                    TesterTest test = this.tester.Analyzer.GetResult(url, timeout, testsCount - responseTimes.Count());
                    for (int i = responseTimes.Count() - 1; i >= 0; i--)
                    {
                        test.TestsCount++;
                        test.TestResults.Insert(0, new TesterTestResult() { ResponseTime = responseTimes.ElementAt(i) });
                    }
                    test.Date = responseDate;
                    bool saving = this.saver.SaveTestData(webResourceTest, test);

                    if (saving)
                    {
                        this.TestFinished(connectionId, testedUrls.Count + 1);
                    }

                    testedUrls.Add(url);

                    Task.Delay(Convert.ToInt32(interval * 1000)).Wait();
                }
            }

            return urls;
        }

        public int Count(int testId)
        {
            try
            {
                int testsCount = this.dataUnit.GetRepository<WebResourceTest>().GetById(testId).Tests.Count();
                return testsCount;
            }
            catch
            {
                throw;
            }
        }
    }
}
