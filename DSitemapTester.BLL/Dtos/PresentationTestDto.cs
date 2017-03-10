using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.BLL.Dtos
{
    public class PresentationTestDto
    {
        public string Url { get; set; }

        public int WrongTestsCount { get; set; }

        public int TestsCount { get; set; }

        public PresentationTestResultDto MinimalResponseTime { get; set; }

        public PresentationTestResultDto AverageResponseTime { get; set; }

        public PresentationTestResultDto MaximalResponseTime { get; set; }
    }
}
