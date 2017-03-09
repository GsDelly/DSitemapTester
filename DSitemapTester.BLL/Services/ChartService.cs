using DSitemapTester.BLL.Dtos;
using DSitemapTester.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace DSitemapTester.BLL.Services
{
    public class ChartService //: IChartService
    {
        //public Chart GetTotalChart(PresentationWebResourceDto webResource)
        //{
        //    int totalTestsCount = webResource.Tests.First().Tests.Sum(res => res.TestsCount);
        //    int totalWrongTestsCount = webResource.Tests.First().Tests.Sum(res => res.WrongTestsCount);

        //    return null;
        //}

        //public Chart GetUrlConnectionChart(PresentationWebResourceDto webResource)
        //{ 
        //    int successfulUrlsCount = webResource.Tests.First().Tests.Where(res => res.WrongTestsCount == 0).Count();
        //    int wrongUrlsCount = webResource.Tests.First().Tests.Where(res => res.WrongTestsCount == res.TestsCount).Count();
        //    int warningUrlsCount = webResource.Tests.First().Tests.Count() - (successfulUrlsCount + wrongUrlsCount);
        //}
    }
}
