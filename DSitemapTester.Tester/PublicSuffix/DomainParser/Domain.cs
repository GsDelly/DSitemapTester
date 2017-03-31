using DSitemapTester.Tester.PublicSuffix.RuleParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.Tester.PublicSuffix.DomainParser
{
    public class Domain
    {
        public string Name { get; private set; }
        public string Tld { get; private set; }
        public string SubDomain { get; private set; }
        public string MainDomain { get; private set; }
        public string Host { get; private set; }
        public DomainRule DomainRule { get; private set; }

        public Domain(string domain, DomainRule rule)
        {
            string tld = string.Empty;
            string name = string.Empty;
            string subDomain = string.Empty;
            string mainDomain = string.Empty;
            string host = string.Empty;
            //www.example.com.ua

            IList<string> parts = domain.Split('.').Select(x => x.Trim()).Reverse().ToList();

            if (parts.Count() == rule.LabelCount)
            {

            }

            //com.ua
            tld = rule.Name;

            //example
            name = parts.Skip(rule.LabelCount).Take(1).Single();

            //example.com.ua
            mainDomain = name + '.' + tld;

            //www
            if (parts.Count != rule.LabelCount + 1)
            {
                subDomain = parts.Skip(rule.LabelCount + 1).Take(1).Single();
            }
            //host
            host = domain;

            this.DomainRule = rule;
            this.Name = name;
            this.SubDomain = subDomain;
            this.MainDomain = mainDomain;
            this.Tld = tld;
            this.Host = host;
        }
    }
}
