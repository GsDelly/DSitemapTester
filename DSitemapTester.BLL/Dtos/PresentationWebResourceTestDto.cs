using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.BLL.Dtos
{
    public class PresentationWebResourceTestDto
    {
        public string Date { get; set; }

        public double Duration { get; set; }

        public ICollection<PresentationTestDto> Tests { get; set; }
    }
}
