using DSitemapTester.Entities.Entities;
using DSitemapTester.Tester.Dtos;
using DSitemapTester.Tester.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.BLL.Interfaces
{
    public interface ISaveService
    {
        bool SaveTestData(WebResourceTest webResourceTest, TesterTest test);

        WebResourceTest GetNewTest(string url);
    }
}
