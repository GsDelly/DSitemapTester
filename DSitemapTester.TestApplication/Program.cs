using DSitemapTester.DAL.EFContext;
using DSitemapTester.DAL.Interfaces;
using DSitemapTester.DAL.Repositories;
using DSitemapTester.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IUnitOfWork unit = new EFUnitOfWork(new SitemapContext("SitemapContext")))
            {
                IEnumerable<WebResource> web = unit.GetRepository<WebResource>().Get();
            }
        }
    }
}
