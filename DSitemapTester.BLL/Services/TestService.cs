using DSitemapTester.BLL.Interfaces;
using DSitemapTester.DAL.Interfaces;
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
            return testResults;
        }
    }
}
