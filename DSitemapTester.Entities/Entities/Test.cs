using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.Entities.Entities
{
    public class Test
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int TestsCount { get; set; }

        public virtual ICollection<TestResult> TestResults { get; set; }
    }
}
