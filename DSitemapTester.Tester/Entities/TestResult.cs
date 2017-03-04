using DSitemapTester.Tester.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.Tester.Entities
{
    public class TestResult
    {
        public double ResponseTime { get; set; }

        public ConnectionStatus Status { get; set; }
    }
}
