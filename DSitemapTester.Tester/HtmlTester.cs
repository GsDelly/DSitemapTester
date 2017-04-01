using DSitemapTester.Tester.PublicSuffix.DomainParser;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace DSitemapTester.Tester
{
    public class HtmlTester
    {
        private DomainParser domainParser = new DomainParser();

        public IList<string> GetUrls(string url, Domain domain, int testCount, CancellationToken token, out IList<double> responseTimes, out DateTime responseDate)
        {
            string html = string.Empty;

            IList<string> urls = new List<string>();
            IList<double> times = new List<double>();

            double responseTime;
            DateTime date = DateTime.Now;

            while (times.Count() != testCount && !urls.Any() && !token.IsCancellationRequested)
            {
                try
                {
                    html = this.GetResponse(url, out responseTime);

                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(html);

                    var links = doc.DocumentNode.SelectNodes("//a[@href]");
                    foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                    {
                        HtmlAttribute href = link.Attributes["href"];
                        urls.Add(href.Value);
                    }
                    urls = this.GetInnerLinks(url, urls, domain , token);
                }
                catch
                {
                    responseTime = 0;
                }
                times.Add(responseTime);
            }

            responseTimes = times;
            responseDate = date;

            return urls;
        }

        private IList<string> GetInnerLinks(string outerUrl, IList<string> urls, Domain domain, CancellationToken token)
        {
            IList<string> innerLinks = new List<string>();

            for (int i = 0;  i < urls.Count(); i++)
            {
                if (!token.IsCancellationRequested)
                {
                    string innerLink = string.Empty;
                    string url = urls.ElementAt(i);

                    try
                    {
                        Uri sitemapUri = new Uri(url);
                        if (sitemapUri.Scheme.Contains("http"))
                        {
                            Domain innerUrlDomain = this.GetUrlDomain(sitemapUri);

                            if ((innerUrlDomain.Name == domain.Name) && (innerUrlDomain.Tld == domain.Tld))
                            {
                                innerLink = sitemapUri.AbsoluteUri;
                            }
                        }
                    }
                    catch
                    {
                        if ((url.Any()) && (url.First().Equals('/')))
                        {
                            Uri uri = new Uri(outerUrl);
                            innerLink = uri.Scheme + "://" + uri.Host + url;
                        }
                    }

                    if (!String.IsNullOrEmpty(innerLink))
                    {
                        if (innerLink.Last() == '/')
                        {
                            innerLink = innerLink.Remove(innerLink.Length - 1);
                        }
                        if (!innerLinks.Contains(innerLink))
                        {
                            innerLinks.Add(innerLink);
                        }
                    }
                }
                else
                {
                    break;
                }           
            }
            return innerLinks;
        }

        private string GetResponse(string url, out double responseTime)
        {
            string html;

            HttpWebRequest request;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                Stopwatch timer = new Stopwatch();

                timer.Start();
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    html = new StreamReader(response.GetResponseStream()).ReadToEnd();
                }

                timer.Stop();

                TimeSpan timeTaken = timer.Elapsed;

                responseTime = timeTaken.TotalSeconds;
            }
            catch
            {
                html = string.Empty;
                responseTime = 0;
            }

            return html;                      
        }

        public Domain GetUrlDomain(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                return domainParser.GetDomain(uri);
            }
            catch
            {
                return null;
            }
        }

        public Domain GetUrlDomain(Uri uri)
        {
            return domainParser.GetDomain(uri);
        }
    }
}
