using DSitemapTester.DAL.EFContext;
using DSitemapTester.DAL.Interfaces;
using DSitemapTester.DAL.Repositories;
using DSitemapTester.Entities.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            TestConnectionService testService = new TestConnectionService();
            var Urls = testService.GetSitemapUrls("https://google.com");
            //}
        }
    }
}
