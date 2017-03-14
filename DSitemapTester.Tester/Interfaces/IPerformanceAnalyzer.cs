using DSitemapTester.Tester.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.Tester.Interfaces
{
    public interface IPerformanceAnalyzer
    {
        TesterTest GetResult(string url, int timeout, int testsCount, double interval);
    }
}
