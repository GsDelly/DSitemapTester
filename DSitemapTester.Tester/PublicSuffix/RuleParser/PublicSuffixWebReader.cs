using DSitemapTester.Tester.PublicSuffix.DomainParser;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.Tester.PublicSuffix.RuleParser
{
    public class PublicSuffixWebReader
    {
        private readonly string url;

        public PublicSuffixWebReader()
        {
            try
            {
                this.url = ConfigurationManager.AppSettings["publicSuffixSource"];

                if (this.url == null)
                {
                    this.url = "https://publicsuffix.org/list/public_suffix_list.dat";
                }
            }
            catch
            {
                this.url = "https://publicsuffix.org/list/public_suffix_list.dat";
            }
        }

        private string LoadPublicSuffixContent(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.url);
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader sr = new StreamReader(responseStream))
                {
                    string strContent = sr.ReadToEnd();
                    return strContent;
                }
            }
        }

        public void PubliSuffixInit()
        {
            var ruleParser = new DomainRuleParser();

            Task<string> ruleData = Task.Factory.StartNew(() =>
            {
                return this.LoadPublicSuffixContent(this.url);
            });
            ruleData.Wait();

            string data = ruleData.Result;

            var rules = ruleParser.ParseRules(data);

            rules = rules.OrderByDescending(x => x.Type == DomainRuleType.WildcardException ? 1 : 0)
               .ThenByDescending(x => x.LabelCount)
               .ThenByDescending(x => x.Name);

            DomainsCache<DomainRule>.AddRange(rules);
        }
    }
}
