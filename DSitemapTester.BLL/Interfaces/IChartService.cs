using DSitemapTester.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace DSitemapTester.BLL.Interfaces
{
    public interface IChartService
    {
        Chart GetTotalChart(PresentationWebResourceDto webResource);

        Chart GetUrlConnectionChart(PresentationWebResourceDto webResource);
    }
}
