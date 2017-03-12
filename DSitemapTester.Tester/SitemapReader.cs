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

        public IEnumerable<Sitemap> GetSitemapUrls(string url)
        {
            IEnumerable<XElement> xSitemaps = this.GetBottomSitemaps(url);
            IEnumerable<Sitemap> sitemaps  = this.GetSitemapEntities(xSitemaps);

            return sitemaps;
        }

        private IEnumerable<XElement> GetBottomSitemaps(string url)
        {
            this.url = url + "/sitemap.xml";
            XElement sitemap = XElement.Load(this.url);

            // ... XNames.
            XName xLoc = XName.Get("loc", sitemap.Name.NamespaceName);
            XName xSitemap = XName.Get("sitemap", sitemap.Name.NamespaceName);

            // ... Loop over url elements.
            // ... Then access each loc element.

            IList<XElement> topSitemaps = new List<XElement>();
            IList<XElement> bottomSitemaps = sitemap.Elements(xSitemap).ToList();
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
                        xSitemap = XName.Get("sitemap", sitemap.Name.NamespaceName);

                        if (sitemap.Elements(xSitemap).Count() > 0)
                        {
                            bottomSitemaps.Remove(topSitemapsElement);
                            foreach (XElement sitemapChildElement in sitemap.Elements(xSitemap))
                            {
                                bottomSitemaps.Add(sitemapChildElement);
                            }
                        }
                    }
                }
            }
            return topSitemaps;
        }
   
        private IEnumerable<Sitemap> GetSitemapEntities(IEnumerable<XElement> sitemaps)
        {
            IList<string> pageUrls = new List<string>();
            IList<Sitemap> sitemapEntities = new List<Sitemap>();
            Sitemap sitemapEntity = new Sitemap();

            foreach (XElement sitemapElement in sitemaps)
            {
                XElement sitemap = sitemapElement;
                XName xUrl = XName.Get("url", sitemapElement.Name.NamespaceName);

                if (sitemapElement.Elements(xUrl).Count() == 0)
                {
                    XName sitemapLoc = XName.Get("loc", sitemapElement.Name.NamespaceName);
                    XElement locElement = sitemapElement.Element(sitemapLoc);

                    this.url = locElement.Value;
                    sitemap = XElement.Load(this.url);
                    xUrl = XName.Get("url", sitemap.Name.NamespaceName);
                }

                foreach (XElement webPage in sitemap.Elements(xUrl))
                {
                    XName xLoc = XName.Get("loc", sitemap.Name.NamespaceName);
                    XElement locElement = webPage.Element(xLoc);

                    XName xChangeFreq = XName.Get("changefreq", sitemap.Name.NamespaceName);
                    XElement changeFreqElement = webPage.Element(xChangeFreq);

                    XName xPriority = XName.Get("priority", sitemap.Name.NamespaceName);
                    XElement priorityElement = webPage.Element(xPriority);

                    XName xLastMod= XName.Get("lastmod", sitemap.Name.NamespaceName);
                    XElement lastModElement = webPage.Element(xLastMod);

                    sitemapEntity = new Sitemap();

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
    }
}
