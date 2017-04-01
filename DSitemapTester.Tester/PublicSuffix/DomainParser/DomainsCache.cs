using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.Tester.PublicSuffix.DomainParser
{
    public static class DomainsCache<T> where T : class
    {
        private static object locker = new object();
        private static IList<T> rules = new List<T>();

        public static void Add(T rule)
        {
            rules.Add(rule);
        }

        public static void AddRange(IEnumerable<T> rulesCollection)
        {
            foreach (var rule in rulesCollection)
            {
                rules.Add(rule);
            }
        }

        public static void Remove(T rule)
        {
            rules.Remove(rule);
        }

        public static void Clear()
        {
            rules.Clear();
        }

        public static IEnumerable<T> GetRules()
        {
            lock (locker)
            {
                return rules;
            }
        }
    }
}
