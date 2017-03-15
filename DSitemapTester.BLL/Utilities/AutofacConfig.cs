using Autofac;
using DSitemapTester.BLL.Interfaces;
using DSitemapTester.BLL.Services;
using DSitemapTester.DAL.EFContext;
using DSitemapTester.DAL.Interfaces;
using DSitemapTester.DAL.Repositories;
using DSitemapTester.Tester;
using DSitemapTester.Tester.Interfaces;

namespace DSitemapTester.BLL.Utilities
{
    public class AutofacConfig
    {
        public static void Configure(ref ContainerBuilder builder)
        {
            // Data access config
            builder.Register(db => new SitemapContext("SitemapContext")).InstancePerRequest();
            builder.RegisterType<EFUnitOfWork>().As<IUnitOfWork>();
            // Services config
            builder.RegisterType<TestService>().As<ITestService>();
            builder.RegisterType<SaveService>().As<ISaveService>();
            builder.RegisterType<PerformanceAnalyzer>().As<IPerformanceAnalyzer>();
            builder.RegisterType<SitemapReader>().As<ISitemapReader>();
            builder.RegisterType<SitemapTester>().As<ISitemapTester>();
        }
    }
}
