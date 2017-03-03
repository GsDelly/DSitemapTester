using DSitemapTester.TestApplication.Entities;
using DSitemapTester.TestApplication.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DSitemapTester.TestApplication
{
    public class TestConnectionService
    {
        private string sitemapTesterUrl;

        public IEnumerable<Sitemap> GetSitemapUrls(string url)
        {
            //HttpWebResponse response = this.GetResponse(url);
            var a = this.GetBottomSitemaps(url);
            var b = this.GetSitemapEntities(a);
            Console.ReadKey();
            return null;
        }

        private IEnumerable<XElement> GetBottomSitemaps(string url)
        {
            sitemapTesterUrl = "D://Sitemap.xml";
            //sitemapTesterUrl = "https://google.com/sitemap.xml";
            //sitemapTesterUrl = "https://www.google.com/forms/sitemaps.xml";
            //sitemapTesterUrl = "https://www.google.com/slides/sitemaps.xml";
            XElement sitemap = XElement.Load(sitemapTesterUrl);

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
                        sitemap = XElement.Load(topSitemapsElement.Value);
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
                    sitemapTesterUrl = sitemapElement.Value;
                    sitemap = XElement.Load(sitemapTesterUrl);
                    xUrl = XName.Get("url", sitemap.Name.NamespaceName);
                }

                foreach (XElement webPage in sitemap.Elements(xUrl))
                {
                    //    string Url { get; set; }
                    //    DateTime LastModification { get; set; }
                    //    Frequency Frequency { get; set; }
                    //    double Priority { get; set; }
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
