using DSitemapTester.Tester.Entities;
using DSitemapTester.Tester.Enums;
using DSitemapTester.Tester.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DSitemapTester.Tester
{
    public class SitemapReader : ISitemapReader
    {
        public IEnumerable<string> GetSitemapUrls(string url)
        {
            string sitemapUrl = url + "/sitemap.xml";
            IList <string> urls = new List<string>();
            try
            {
                IEnumerable<XElement> childUrls = this.GetUrls(sitemapUrl);
                IEnumerable<UrlInfo> sitemaps = this.GetSitemapEntities(childUrls);

                foreach (UrlInfo sitemap in sitemaps)
                {
                    urls.Add(sitemap.Url);
                }
            }
            catch
            {

            }

            return urls;
        }

        private IEnumerable<UrlInfo> GetSitemapEntities(IEnumerable<XElement> sitemaps)
        {
            IList<string> pageUrls = new List<string>();
            IList<UrlInfo> sitemapEntities = new List<UrlInfo>();
            UrlInfo sitemapEntity = new UrlInfo();

            try
            {
                foreach (XElement webPage in sitemaps)
                {
                    XName locName = XName.Get("loc", webPage.Name.NamespaceName);
                    XElement locElement = webPage.Element(locName);

                    XName changeFreqName = XName.Get("changefreq", webPage.Name.NamespaceName);
                    XElement changeFreqElement = webPage.Element(changeFreqName);

                    XName priorityName = XName.Get("priority", webPage.Name.NamespaceName);
                    XElement priorityElement = webPage.Element(priorityName);

                    XName lastModName = XName.Get("lastmod", webPage.Name.NamespaceName);
                    XElement lastModElement = webPage.Element(lastModName);

                    sitemapEntity = new UrlInfo();

                    sitemapEntity.Url = locElement.Value;

                    if (changeFreqElement != null)
                    {
                        Frequency frequency;
                        Enum.TryParse(changeFreqElement.Value.ToUpper(), out frequency);
                        sitemapEntity.Frequency = frequency;
                    }

                    sitemapEntity.Priority = priorityElement != null ? Convert.ToDouble(priorityElement.Value.Replace('.', ',')) : 0.5;

                    if (lastModElement != null)
                    {
                        sitemapEntity.LastModification = Convert.ToDateTime(lastModElement.Value);
                    }

                    sitemapEntities.Add(sitemapEntity);
                }
                return sitemapEntities;
            }
            catch
            {
                throw;
            }
        }

        private IEnumerable<XElement> GetUrls(string url)
        {
            try
            {
                List<XElement> res = new List<XElement>();

                XElement sitemap = XElement.Load(url);

                XName sitemapName = XName.Get("sitemap", sitemap.Name.NamespaceName);

                List<XElement> childSitemaps = sitemap.Elements(sitemapName).ToList();

                foreach (var childSitemap in childSitemaps)
                {
                    res.AddRange(GetUrls(GetLoc(childSitemap)));
                }

                XName urlName = XName.Get("url", sitemap.Name.NamespaceName);
                res.AddRange(sitemap.Elements(urlName));

                return res;
            }
            catch
            {
                throw;
            }
        }

        private string GetLoc(XElement element)
        {
            XName sitemapLoc = XName.Get("loc", element.Name.NamespaceName);
            XElement locElement = element.Element(sitemapLoc);

            return locElement.Value;
        }
    }
}
