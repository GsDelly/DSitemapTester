﻿using DSitemapTester.BLL.Dtos;
using DSitemapTester.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DSitemapTester.BLL.Interfaces
{
    public interface ITestService
    {
        Action<string, int> OnTestFinished { get; set; }
        Action<string, int> OnUrlsFounded { get; set; }

        void TestFinished(string connectionId, int urlsCount);
        void UrlsFounded(string connectionId, int totalUrlsCount);

        PresentationWebResourceTestDto GetTest(int testId);

        void RunTest(int testId, int timeout, int testsCount, CancellationToken token/*, string connectionId*/);

        int GetTestId(string url);

        int Count(int testId);
    }
}
