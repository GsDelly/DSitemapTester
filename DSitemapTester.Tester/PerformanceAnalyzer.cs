using DSitemapTester.Tester.Configuration.Connection;
using DSitemapTester.Tester.Entities;
using DSitemapTester.Tester.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.Tester
{
    public class PerformanceAnalyzer
    {
        public IEnumerable<Test> GetConnectionResults(IEnumerable<string> urls)
        {
            int testsCount = ConnectionSettings.GetTestsCount();
            double interval = ConnectionSettings.GetInterval();
            int timeout = ConnectionSettings.GetTimeout();

            return this.GetResults(urls, testsCount, interval, timeout);
        }

        public IEnumerable<Test> GetConnectionResults(IEnumerable<string> urls, int testsCount, double interval, int timeout)
        {

            IEnumerable<Test> performanceTest = this.GetResults(urls, testsCount, interval, timeout);

            return performanceTest;
        }

        private Test GetTestResults (string url, int testsCount, double interval, int timeout)
        {
            IList<Task<TestResult>> tasks = new List<Task<TestResult>>();
            IList<TestResult> testResults = new List<TestResult>();

            Test test = new Test();
            Trace.WriteLine(String.Format("New task created {0} url", url));
            for (int i = 0; i < testsCount; i++)
            {
                tasks.Add(Task<TestResult>.Factory.StartNew(() =>
                {
                    TestResult result = new TestResult();
                    double time = 0;

                    try
                    {
                        time = this.GetConnectionTime(url, timeout);
                        result.Status = ConnectionStatus.Connected;
                    }
                    catch (WebException e)
                    {
                        if (e.Status == WebExceptionStatus.Timeout)
                        {
                            result.Status = ConnectionStatus.DisconnectedByTimeout;
                        }
                        else
                        {
                            result.Status = ConnectionStatus.Disconnected;
                        }
                    }
                    finally
                    {
                        result.ResponseTime = time;
                        Trace.WriteLine(String.Format("Task finished {0} time ", time));
                    }

                    return result;
                }));

                Task.Delay(Convert.ToInt32(interval * 1000)).Wait();
            }
            Task.WaitAll(tasks.ToArray());

            foreach (Task<TestResult> task in tasks)
            {
                testResults.Add(task.Result);
            }

            test.TestResults = testResults;

            return test;
        }

        private IEnumerable<Test> GetResults(IEnumerable<string> urls, int testsCount, double interval, int timeout)
        {
            IList<Test> tests = new List<Test>();

            foreach (string url in urls)
            {
                Test test = new Test();

                test = this.GetTestResults(url, testsCount, interval, timeout);
                test.Url = url;
                test.TestsCount = testsCount;
                test.Date = DateTime.Now;

                tests.Add(test);
            }
            return tests;
        }

        private double GetConnectionTime(string url, int timeout)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = timeout * 1000;

            Stopwatch timer = new Stopwatch();
            timer.Start();

            try
            {
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            }
            catch
            {
                throw;
            }

            finally
            {
                timer.Stop();
            }

            TimeSpan timeTaken = timer.Elapsed;

            return timeTaken.TotalSeconds;
        }
    }
}
