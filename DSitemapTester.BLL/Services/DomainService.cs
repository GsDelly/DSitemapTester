using DSitemapTester.BLL.Interfaces;
using DSitemapTester.Tester.PublicSuffix.RuleParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.BLL.Services
{
    public static class DomainService
    {
        public static void ParseDomains()
        {
            PublicSuffixWebReader domainParser = new PublicSuffixWebReader();
            Task domainParsing = Task.Factory.StartNew(() => {
                domainParser.PubliSuffixInit();
            });
            domainParsing.Wait();
        }
    }
}
