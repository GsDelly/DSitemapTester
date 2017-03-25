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
        private string url;

        public IEnumerable<string> GetSitemapUrls(string url)
        {
            IList<string> urls = new List<string>();
            try
            {
                IEnumerable<XElement> childSitemaps = this.GetChildSitemaps(url);
                IEnumerable<TesterSitemap> sitemaps = this.GetSitemapEntities(childSitemaps);

                foreach (TesterSitemap sitemapUrl in sitemaps)
                {
                    urls.Add(sitemapUrl.Url);
                }
            }
            catch
            {

            }

            return urls;
        }

        private IEnumerable<XElement> GetChildSitemaps(string url)
        {
            this.url = url + "/sitemap.xml";
            try
            {
                XElement sitemap = XElement.Load(this.url);

                XName sitemapName = XName.Get("sitemap", sitemap.Name.NamespaceName);

                IList<XElement> topSitemaps = new List<XElement>();
                IList<XElement> bottomSitemaps = sitemap.Elements(sitemapName).ToList();
                topSitemaps.Add(sitemap);

                if (bottomSitemaps.Count() > 0)
                {
                    while (!topSitemaps.SequenceEqual(bottomSitemaps))
                    {
                        topSitemaps = new List<XElement>(bottomSitemaps);
                        foreach (XElement topSitemapsElement in topSitemaps)
                        {
                            XName sitemapLoc = XName.Get("loc", topSitemapsElement.Name.NamespaceName);
                            XElement locElement = topSitemapsElement.Element(sitemapLoc);

                            sitemap = XElement.Load(locElement.Value);
                            sitemapName = XName.Get("sitemap", sitemap.Name.NamespaceName);

                            if (sitemap.Elements(sitemapName).Count() > 0)
                            {
                                bottomSitemaps.Remove(topSitemapsElement);
                                foreach (XElement sitemapChildElement in sitemap.Elements(sitemapName))
                                {
                                    bottomSitemaps.Add(sitemapChildElement);
                                }
                            }
                        }
                    }
                }
                return topSitemaps;
            }
            catch
            {
                throw;
            }
        }   

        private IEnumerable<TesterSitemap> GetSitemapEntities(IEnumerable<XElement> sitemaps)
        {
            IList<string> pageUrls = new List<string>();
            IList<TesterSitemap> sitemapEntities = new List<TesterSitemap>();
            TesterSitemap sitemapEntity = new TesterSitemap();

            try
            {
                foreach (XElement sitemapElement in sitemaps)
                {
                    XElement sitemap = sitemapElement;
                    XName urlName = XName.Get("url", sitemapElement.Name.NamespaceName);

                    if (sitemapElement.Elements(urlName).Count() == 0)
                    {
                        XName sitemapLoc = XName.Get("loc", sitemapElement.Name.NamespaceName);
                        XElement locElement = sitemapElement.Element(sitemapLoc);

                        this.url = locElement.Value;
                        sitemap = XElement.Load(this.url);
                        urlName = XName.Get("url", sitemap.Name.NamespaceName);
                    }

                    foreach (XElement webPage in sitemap.Elements(urlName))
                    {
                        XName locName = XName.Get("loc", sitemap.Name.NamespaceName);
                        XElement locElement = webPage.Element(locName);

                        XName changeFreqName = XName.Get("changefreq", sitemap.Name.NamespaceName);
                        XElement changeFreqElement = webPage.Element(changeFreqName);

                        XName priorityName = XName.Get("priority", sitemap.Name.NamespaceName);
                        XElement priorityElement = webPage.Element(priorityName);

                        XName lastModName = XName.Get("lastmod", sitemap.Name.NamespaceName);
                        XElement lastModElement = webPage.Element(lastModName);

                        sitemapEntity = new TesterSitemap();

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
                }
                return sitemapEntities;
            }
            catch
            {
                throw;
            }
        }
    }
}
