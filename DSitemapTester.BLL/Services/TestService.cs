using AutoMapper;
using DSitemapTester.BLL.Configuration;
using DSitemapTester.BLL.Interfaces;
using DSitemapTester.DAL.Interfaces;
using DSitemapTester.Entities.Entities;
using DSitemapTester.Tester;
using DSitemapTester.Tester.Dtos;
using DSitemapTester.Tester.Interfaces;
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
        private readonly ISitemapTester tester;

        public TestService(IUnitOfWork unitOfWork, ISitemapTester tester)
        {
            this.dataUnit = unitOfWork;
            this.tester = tester;
        }

        public WebResourceDto GetTestResults(string url)
        {
            WebResourceDto testResults = tester.GetTestResults(url);
            this.SaveTestData(testResults);

            return testResults;
        }

        public bool SaveTestData(WebResourceDto webResource)
        {
            try
            {
                GlobalAutomapperConfig.Configure();

                IEnumerable<WebResource> resources = this.dataUnit.GetRepository<WebResource>().Get((x) => x.Url == webResource.Url);
                WebResource resource;

                if (resources.Count() > 0)
                {
                    resource = resources.First();
                }
                else
                {
                    resource = new WebResource()
                    {
                        Url = webResource.Url,
                        SitemapResources = new List<SitemapResource>(),
                        Tests = new List<WebResourceTest>()                    
                    };
                    this.dataUnit.GetRepository<WebResource>().Insert(resource);
                }

                WebResourceTest webResourceTest = new WebResourceTest()
                {
                    Date = webResource.Tests.First().Date,
                    Duration = webResource.Tests.Last().Date - webResource.Tests.First().Date,
                    Tests = new List<Test>()
                };

                foreach (TestDto test in webResource.Tests)
                {
                    Test testEntity = new Test();

                    IEnumerable<SitemapResource> sitemaps = resource.SitemapResources.Where((x) => x.Url == test.Url);

                    testEntity = Mapper.Map<TestDto, Test>(test);

                    if (resource.SitemapResources.Count() > 0)
                    {
                        testEntity.SitemapResource = sitemaps.First();
                    }
                    else
                    {
                        SitemapResource sitemap = new SitemapResource()
                        {
                            Url = test.Url,
                            WebResource = resource
                        };
                        //this.dataUnit.GetRepository<SitemapResource>().Insert(sitemap);
                        testEntity.SitemapResource = sitemap;
                    }
                    //tests.Add(testEntity);
                    webResourceTest.Tests.Add(testEntity);
                }
                resource.Tests.Add(webResourceTest);

                this.dataUnit.SaveChanges();
            }
            catch (Exception e)
            {

            }

            return true;
        }
    }
}
