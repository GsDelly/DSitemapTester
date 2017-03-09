using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.BLL.Utilities
{
    public static class UrlAdaptor
    {
        public static string GetUrl(string url)
        {
            if (url.Contains("www."))
            {
                url = url.Remove(url.IndexOf("www."), 4);
            }

            if (url.Last() == '/')
            {
                url = url.Remove(url.Length - 1, 1);
            }

            return url;
        }
    }
}
