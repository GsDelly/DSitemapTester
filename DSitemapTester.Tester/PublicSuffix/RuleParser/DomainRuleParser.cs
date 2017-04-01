using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.Tester.PublicSuffix.RuleParser
{
    public class DomainRuleParser
    {
        public IEnumerable<DomainRule> ParseRules(string data)
        {
            var lines = data.Split(new char[] { '\n', '\r' });
            return this.ParseRules(lines);
        }

        public IEnumerable<DomainRule> ParseRules(IEnumerable<string> lines)
        {
            var items = new List<DomainRule>();
            var section = DomainRuleSection.Unknown;

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                if (line.StartsWith("//"))
                {
                    if (line.StartsWith("// ===BEGIN ICANN DOMAINS==="))
                    {
                        section = DomainRuleSection.ICANN;
                    }
                    else if (line.StartsWith("// ===END ICANN DOMAINS==="))
                    {
                        section = DomainRuleSection.Unknown;
                    }
                    else if (line.StartsWith("// ===BEGIN PRIVATE DOMAINS==="))
                    {
                        section = DomainRuleSection.Private;
                    }
                    else if (line.StartsWith("// ===END PRIVATE DOMAINS==="))
                    {
                        section = DomainRuleSection.Unknown;
                    }

                    continue;
                }

                DomainRule domainRule = new DomainRule(line.Trim(), section);

                items.Add(domainRule);
            }

            return items;
        }
    }
}
