using DSitemapTester.BLL.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DSitemapTester
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalAutomapperConfig.Configure();
            AutofacConfig.Configure();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
