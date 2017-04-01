using DSitemapTester.Tester.PublicSuffix.RuleParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.Tester.PublicSuffix.DomainParser
{
    public class DomainParser
    {
        public Domain GetDomain(string domain)
        {
            Uri domainUri;
            try
            {
                domainUri = new Uri(domain);
                return this.GetDomain(domainUri);
            }
            catch
            {
                return null;
            }
        }

        public Domain GetDomain(Uri domainUri)
        {
            string host = domainUri.Host;

            if (string.IsNullOrEmpty(host))
            {
                return null;
            }

            var parts = host
                .Split('.')
                .Reverse()
                .ToList();

            if (parts.Count == 0 || parts.Any(x => x.Equals(string.Empty)))
            {
                return null;
            }

            IEnumerable<DomainRule> rules = DomainsCache<DomainRule>.GetRules();

            DomainRule matchedDomain = null;

            string domain = host;

            while (matchedDomain == null || domain.IndexOf('.') != -1)
            {
                domain = domain.Remove(0, domain.IndexOf('.') + 1).Trim();

                matchedDomain = rules.FirstOrDefault(x => x.Name == domain);
            }

            return new Domain(host, matchedDomain);
        } 
    }
}
