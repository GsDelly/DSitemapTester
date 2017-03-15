using DSitemapTester.BLL.Dtos;
using DSitemapTester.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.BLL.Interfaces
{
    public interface ITestService
    {
        Action OnTestFinished { get; set; }

        void TestFinished();

        PresentationWebResourceTestDto GetTest(int testId);

        void RunTest(int testId, int timeout, int testsCount);

        int GetTestId(string url);

        int Count(int testId);
    }
}
