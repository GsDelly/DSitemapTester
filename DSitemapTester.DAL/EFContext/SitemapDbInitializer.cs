using DSitemapTester.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.DAL.EFContext
{
    public class SitemapDbInitializer : CreateDatabaseIfNotExists<SitemapContext>
    {
        protected override void Seed(SitemapContext context)
        {
            WebResource web = new WebResource()
            {
                Url = "https://www.google.com",
            };
            WebResource web1 = new WebResource()
            {
                Url = "https://googlees.com",
            };
            SitemapResource sitemap1 = new SitemapResource()
            {
                Url = "https://edu.google.com/",
            };
            SitemapResource sitemap2 = new SitemapResource()
            {
                Url = "https://www.google.com/get",
            };
            SitemapResource sitemap3 = new SitemapResource()
            {
                Url = "https://edu.google.com/case-studies/",
            };
            SitemapResource sitemap4 = new SitemapResource()
            {
                Url = "https://edu.google.com/case-studies/academies-enterprise-trust/",
            };
            SitemapResource sitemap5 = new SitemapResource()
            {
                Url = "https://edu.google.com/case-studies/amherst-central-schools/",
            };
            SitemapResource sitemap6 = new SitemapResource()
            {
                Url = "https://edu.google.com/case-studies/arizona-state-university/",
            };
            TestResult result1 = new TestResult()
            {
                ResponseTime = 1.2,
                SitemapResource = sitemap1,
            };
            TestResult result2 = new TestResult()
            {
                ResponseTime = 2.1,
                SitemapResource = sitemap2,
            };

            TestResult result3 = new TestResult()
            {
                ResponseTime = 3.7,
                SitemapResource = sitemap3,
            };
            TestResult result4 = new TestResult()
            {
                ResponseTime = 1.8,
                SitemapResource = sitemap4,
            };
            TestResult result5 = new TestResult()
            {
                ResponseTime = 3.1,
                SitemapResource = sitemap5,
            };

            TestResult result6 = new TestResult()
            {
                ResponseTime = 3.23,
                SitemapResource = sitemap6,
            };

            Test test1 = new Test()
            {
                Date = DateTime.Now,
            };
            test1.TestResults = new List<TestResult>();
            test1.TestResults.Add(result1);
            test1.TestResults.Add(result2);

            Test test2 = new Test()
            {
                Date = DateTime.Now,
            };
            test2.TestResults = new List<TestResult>();
            test2.TestResults.Add(result3);
            test2.TestResults.Add(result4);

            Test test3 = new Test()
            {
                Date = DateTime.Now,
            };
            test3.TestResults = new List<TestResult>();
            test3.TestResults.Add(result5);
            test3.TestResults.Add(result6);

            web.Tests = new List<Test>();
            web.Tests.Add(test2);
            web.Tests.Add(test3);

            web.SitemapResources = new List<SitemapResource>();
            web.SitemapResources.Add(sitemap2);
            web.SitemapResources.Add(sitemap5);
            web.SitemapResources.Add(sitemap6);

            context.WebResources.Add(web);
            context.WebResources.Add(web1);

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
