using DSitemapTester.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.BLL.Interfaces
{
    public interface ITestService
    {
        PresentationWebResourceTestDto GetTest(int testId);

        int RunTest(string url, int timeout, int testsCount);

        int Count(int testId);
    }
}
