using DSitemapTester.BLL.Interfaces;
using DSitemapTester.DAL.Interfaces;
using DSitemapTester.Entities.Entities;
using DSitemapTester.Tester;
using DSitemapTester.Tester.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.BLL.Services
{
    public class TestService : ITestService
    {
        private readonly IUnitOfWork dataUnit;
        private readonly SitemapTester sitemapTester = new SitemapTester();

        public TestService(IUnitOfWork unitOfWork)
        {
            this.dataUnit = unitOfWork;
        }

        public WebResourceDto GetTestResults(string url)
        {
            WebResourceDto testResults = sitemapTester.GetTestResults(url);
            this.SaveTestData(testResults);

            return testResults;
        }

        public bool SaveTestData(WebResourceDto webResource)
        {
            try
            {
                var resource1 = this.dataUnit.GetRepository<SitemapResource>().Get();
                var resource = this.dataUnit.GetRepository<SitemapResource>().Get((x) => x.Url == webResource.Url);

                IEnumerable<SitemapResource> sitemaps = this.dataUnit.GetRepository<SitemapResource>().Get();
     
                foreach (TestDto test in webResource.Tests)
                {

                }
            }
            catch
            {

            }

            return true;
        }
    }
}
