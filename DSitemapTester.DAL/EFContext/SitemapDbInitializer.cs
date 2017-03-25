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
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
