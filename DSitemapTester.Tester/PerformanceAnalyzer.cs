using DSitemapTester.Tester.Configuration.Connection;
using DSitemapTester.Tester.Entities;
using DSitemapTester.Tester.Enums;
using DSitemapTester.Tester.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.Tester
{
    public class PerformanceAnalyzer : IPerformanceAnalyzer
    {
        public TesterTest GetResult(string url, int timeout, int testsCount)
        {
            TesterTest test = new TesterTest();

            test = this.GetTestResult(url, timeout, testsCount);
            test.Url = url;
            test.TestsCount = testsCount;
            test.Date = DateTime.Now;

            return test;
        }

        private TesterTest GetTestResult(string url, int timeout, int testsCount)
        {
            IList<Task<TesterTestResult>> tasks = new List<Task<TesterTestResult>>();
            IList<TesterTestResult> testResults = new List<TesterTestResult>();

            TesterTest test = new TesterTest();
            Trace.WriteLine(String.Format("New task created {0} url", url));
            for (int i = 0; i < testsCount; i++)
            {
                tasks.Add(Task<TesterTestResult>.Factory.StartNew(() =>
                {
                    TesterTestResult result = new TesterTestResult();
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
            }
            Task.WaitAll(tasks.ToArray());

            foreach (Task<TesterTestResult> task in tasks)
            {
                testResults.Add(task.Result);
            }

            test.TestResults = testResults;

            return test;
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
