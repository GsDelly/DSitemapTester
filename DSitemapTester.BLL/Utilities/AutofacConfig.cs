using Autofac;
using DSitemapTester.DAL.EFContext;
using DSitemapTester.DAL.Interfaces;
using DSitemapTester.DAL.Repositories;

namespace DSitemapTester.BLL.Utilities
{
    public class AutofacConfig
    {
        public static void Configure(ref ContainerBuilder builder)
        {
            // Data access config
            builder.Register(db => new SitemapContext("SitemapContext")).InstancePerLifetimeScope();
            builder.RegisterType<EFUnitOfWork>().As<IUnitOfWork>();
            // Services config
            //builder.RegisterType<EventsService>().As<IEventService>();
        }
    }
}
