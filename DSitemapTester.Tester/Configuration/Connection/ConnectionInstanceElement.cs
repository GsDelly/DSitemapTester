using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.Tester.Configuration.Connection
{
    public class ConnectionInstanceElement : ConfigurationElement
    {
        [ConfigurationProperty("profile", IsKey = true, IsRequired = true)]
        public string Profile
        {
            get { return (string)base["profile"]; }
            set { base["profile"] = value; }
        }
        
        [ConfigurationProperty("testsCount", IsRequired = true)]
        public int TestsCount
        {
            get { return (int)base["testsCount"]; }
            set { base["testsCount"] = value; }
        }
        [ConfigurationProperty("interval", IsRequired = true)]
        public double Interval
        {
            get { return (double)base["interval"]; }
            set { base["interval"] = value; }
        }
        [ConfigurationProperty("timeout", IsRequired = true)]
        public int Timeout
        {
            get { return (int)base["timeout"]; }
            set { base["timeout"] = value; }
        }
    }
}
