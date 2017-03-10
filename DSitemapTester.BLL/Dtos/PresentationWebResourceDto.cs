using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.BLL.Dtos
{
    public class PresentationWebResourceDto
    {
        public string Url { get; set; }

        public ICollection<PresentationWebResourceTestDto> Tests { get; set; }
    }
}
