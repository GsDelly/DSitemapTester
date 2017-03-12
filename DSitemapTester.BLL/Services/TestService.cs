using AutoMapper;
using DSitemapTester.BLL.Configuration;
using DSitemapTester.BLL.Dtos;
using DSitemapTester.BLL.Interfaces;
using DSitemapTester.BLL.Utilities;
using DSitemapTester.DAL.Interfaces;
using DSitemapTester.Tester.Dtos;
using DSitemapTester.Tester.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSitemapTester.BLL.Services
{
    public class TestService : ITestService
    {
        private readonly ISitemapTester tester;
        private readonly ISaveService saver;

        public TestService(ISitemapTester tester, ISaveService saver)
        {
            this.tester = tester;
            this.saver = saver;
        }

        public PresentationWebResourceDto GetTestResults(string url, int timeout, int testsCount)
        {
            url = UrlAdaptor.GetUrl(url);

            WebResourceDto testResults = tester.GetTestResults(url, timeout, testsCount);

            PresentationAutomapperConfig.Configure();

            PresentationWebResourceDto presentationTestResults = new PresentationWebResourceDto()
            {
                Tests = new List<PresentationWebResourceTestDto>()
            };

            presentationTestResults.Url = testResults.Url;

            presentationTestResults.Tests.Add(Mapper.Map<WebResourceDto, PresentationWebResourceTestDto>(testResults));

            foreach (PresentationWebResourceTestDto test in presentationTestResults.Tests)
            {
                test.TotalTestsCount = test.Tests.Sum(res => res.TestsCount);
                test.TotalWrongTestsCount = test.Tests.Sum(res => res.WrongTestsCount);
                test.WrongUrls = test.Tests.Where(res => res.WrongTestsCount == res.TestsCount).Count();
                test.SuccessfulUrls = test.Tests.Where(res => res.WrongTestsCount == 0).Count();
                test.Tests = test.Tests.OrderByDescending(res => res.AverageResponseTime.ResponseTime).ToList();
            }

            this.saver.SaveData(testResults);

            return presentationTestResults;
        }
    }
}
