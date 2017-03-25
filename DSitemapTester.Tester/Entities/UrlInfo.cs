using DSitemapTester.Tester.Enums;
using System;

namespace DSitemapTester.Tester.Entities
{
    public class UrlInfo
    {
        public string Url { get; set; }

        public DateTime LastModification { get; set; }

        public Frequency Frequency { get; set; }

        public double Priority { get; set; }
    }
}
