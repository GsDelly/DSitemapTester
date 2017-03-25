using DSitemapTester.Entities.Entities;
using System.Configuration;
using System.Data.Entity;

namespace DSitemapTester.DAL.EFContext
{
    public class SitemapContext : DbContext
    {

        public SitemapContext(string connectionName) : base(GetConnectionString(connectionName))
        {
            Database.SetInitializer(new SitemapDbInitializer());
        }

        public SitemapContext() : base("SitemapContext")
        {

        }

        public DbSet<WebResource> WebResources { get; set; }

        private static string GetConnectionString(string connectionName)
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }
    }
}
