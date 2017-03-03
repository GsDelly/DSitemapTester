using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.Tester.Dtos
{
    public class TestDto
    {
        public DateTime Date { get; set; }

        public int TestsCount { get; set; }

        public IEnumerable<TestResultDto> TestResults { get; set; }
    }
}
