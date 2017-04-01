using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.Tester.PublicSuffix.RuleParser
{
    public class DomainRule
    {
        public string Name { get; private set; }
        public DomainRuleType Type { get; private set; }
        public int LabelCount { get; private set; }
        public DomainRuleSection Section { get; private set; }

        public DomainRule(string ruleData, DomainRuleSection section = DomainRuleSection.Unknown)
        {
            if (string.IsNullOrEmpty(ruleData))
            {
                throw new ArgumentException("RuleData is emtpy");
            }

            this.Section = section;

            var parts = ruleData.Split('.').Select(x => x.Trim()).ToList();

            foreach (var part in parts)
            {
                if (string.IsNullOrEmpty(part))
                {
                    throw new FormatException("Rule contains empty part");
                }

                if (part.Contains("*") && part != "*")
                {
                    throw new FormatException("Wildcard syntax not correct");
                }
            }


            if (ruleData.StartsWith("!", StringComparison.InvariantCultureIgnoreCase))
            {
                this.Type = DomainRuleType.WildcardException;
                this.Name = ruleData.Substring(1).ToLower();
                this.LabelCount = parts.Count - 1; //Left-most label is removed for Wildcard Exceptions
            }
            else if (ruleData.Contains("*"))
            {
                this.Type = DomainRuleType.Wildcard;
                this.Name = ruleData.ToLower();
                this.LabelCount = parts.Count;
            }
            else
            {
                this.Type = DomainRuleType.Normal;
                this.Name = ruleData.ToLower();
                this.LabelCount = parts.Count;
            }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
