using DSitemapTester.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSitemapTester
{
    public static class DomainsConfig
    {
        public static void RegisterDomains()
        {
            DomainService.ParseDomains();
        }
    }
}