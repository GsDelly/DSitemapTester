﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.BLL.Dtos
{
    public class TestViewModel
    {
        public string Url { get; set; }

        public int TestId { get; set; }

        public int Timeout { get; set; }

        public int TestsCount { get; set; }

        public int DynamicMode { get; set; }
    }
}
