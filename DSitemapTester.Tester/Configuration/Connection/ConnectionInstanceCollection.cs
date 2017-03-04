using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.Tester.Configuration.Connection
{
    public class ConnectionInstanceCollection : ConfigurationElementCollection
    {
        public ConnectionInstanceElement this[int index]
        {
            get { return this.BaseGet(index) as ConnectionInstanceElement; }
        }

        public new ConnectionInstanceElement this[string name]
        {
            get { return this.BaseGet(name) as ConnectionInstanceElement; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ConnectionInstanceElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ConnectionInstanceElement)element).Profile;
        }
    }
}
