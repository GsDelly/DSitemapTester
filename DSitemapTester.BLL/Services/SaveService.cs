using AutoMapper;
using DSitemapTester.BLL.Configuration;
using DSitemapTester.BLL.Interfaces;
using DSitemapTester.DAL.Interfaces;
using DSitemapTester.Entities.Entities;
using DSitemapTester.Tester.Dtos;
using DSitemapTester.Tester.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.BLL.Services
{

    public class SaveService : ISaveService
    {
        private readonly IUnitOfWork dataUnit;

        public SaveService(IUnitOfWork dataUnit)
        {
            this.dataUnit = dataUnit;
        }

        public WebResourceTest GetNewTest(string url)
        {
            try
            {
                IEnumerable<WebResource> resources = this.dataUnit.GetRepository<WebResource>().Get((x) => x.Url == url);
                WebResource resource;

                if (resources.Count() > 0)
                {
                    resource = resources.First();
                }
                else
                {
                    resource = new WebResource()
                    {
                        Url = url,
                        SitemapResources = new List<SitemapResource>(),
                        Tests = new List<WebResourceTest>()
                    };
                    this.dataUnit.GetRepository<WebResource>().Insert(resource);
                }

                WebResourceTest webResourceTest = new WebResourceTest()
                {
                    Tests = new List<Test>(),
                    Date = DateTime.Now
                };
      
                resource.Tests.Add(webResourceTest);

                this.dataUnit.SaveChanges();

                return webResourceTest;
            }
            catch
            {
                throw;
            }
        }

        public bool SaveTestData(WebResourceTest webResourceTest, TesterTest test)
        {
            EntitiesAutomapperConfig.Configure();
            try
            {
                Test testEntity = new Test();

                testEntity = Mapper.Map<TesterTest, Test>(test);

                IEnumerable<SitemapResource> sitemaps = webResourceTest.WebResource.SitemapResources.Where((x) => x.Url == test.Url);
                
                if (sitemaps.Count() > 0)
                {
                    testEntity.SitemapResource = sitemaps.First();
                }
                else
                {
                    SitemapResource sitemap = new SitemapResource()
                    {
                        Url = test.Url,
                        WebResource = webResourceTest.WebResource
                    };
                    testEntity.SitemapResource = sitemap;
                }

                webResourceTest.Tests.Add(testEntity);

                this.dataUnit.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
