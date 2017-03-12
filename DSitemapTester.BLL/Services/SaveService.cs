using AutoMapper;
using DSitemapTester.BLL.Configuration;
using DSitemapTester.BLL.Interfaces;
using DSitemapTester.DAL.Interfaces;
using DSitemapTester.Entities.Entities;
using DSitemapTester.Tester.Dtos;
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

        public async Task SaveData(WebResourceDto webResource)
        {
            Task dbSaving = Task.Run(() =>
            {
                return this.SaveTestData(webResource);
            });

            await dbSaving;
        }

        private bool SaveTestData(WebResourceDto webResource)
        {
            EntitiesAutomapperConfig.Configure();
            try
            {
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
                        testEntity.SitemapResource = sitemap;
                    }
                    webResourceTest.Tests.Add(testEntity);
                }
                resource.Tests.Add(webResourceTest);

                this.dataUnit.SaveChanges();

                Trace.WriteLine("Save changes done");

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
