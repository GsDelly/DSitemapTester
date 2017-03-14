using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.Entities.Entities
{
    public class WebResourceTest
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        //public TimeSpan Duration { get; set; } 

        public virtual ICollection<Test> Tests { get; set; }

        public virtual WebResource WebResource { get; set; }
    }
}
