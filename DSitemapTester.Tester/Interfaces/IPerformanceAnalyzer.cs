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
        IEnumerable<Test> GetConnectionResults(IEnumerable<string> urls);

        IEnumerable<Test> GetConnectionResults(IEnumerable<string> urls, int testsCount, double interval, int timeout);
    }
}
