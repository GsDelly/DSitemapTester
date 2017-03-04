using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.Tester.Configuration.Connection
{
    public class ConnectionSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public ConnectionInstanceCollection Instances
        {
            get { return (ConnectionInstanceCollection)this[string.Empty]; }
            set { this[string.Empty] = value; }
        }
    }
}
