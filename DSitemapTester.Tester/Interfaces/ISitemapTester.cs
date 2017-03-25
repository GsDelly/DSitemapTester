using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.Tester.Interfaces
{
    public interface ISitemapTester
    {
        ISitemapReader Reader { get; }
        IPerformanceAnalyzer Analyzer { get; }
    }
}
