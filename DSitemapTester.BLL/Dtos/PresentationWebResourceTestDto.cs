﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.BLL.Dtos
{
    public class PresentationWebResourceTestDto
    {
        public int TotalTestsCount { get; set; }

        public int TotalWrongTestsCount { get; set; }

        public int TotalUrls { get; set; }

        public int SuccessfulUrls { get; set; }

        public int WrongUrls { get; set; }

        public ICollection<PresentationTestDto> Tests { get; set; }
    }
}
