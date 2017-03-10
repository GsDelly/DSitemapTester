using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.Tester.Configuration.Connection
{
    public static class ConnectionSettings
    {
        public static double GetInterval()
        {
            var config = ConfigurationManager.GetSection("ConnectionSettings") as ConnectionSection;
            return config.Instances[ConfigurationManager.AppSettings.Get("currentConnectionProfile")].Interval;
        }

        public static int GetTestsCount()
        {
            var config = ConfigurationManager.GetSection("ConnectionSettings") as ConnectionSection;
            return config.Instances[ConfigurationManager.AppSettings.Get("currentConnectionProfile")].TestsCount;
        }

        public static int GetTimeout()
        {
            var config = ConfigurationManager.GetSection("ConnectionSettings") as ConnectionSection;
            return config.Instances[ConfigurationManager.AppSettings.Get("currentConnectionProfile")].Timeout;
        }
    }
}
