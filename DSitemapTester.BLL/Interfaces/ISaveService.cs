﻿using DSitemapTester.Tester.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.BLL.Interfaces
{
    public interface ISaveService
    {
        Task SaveData(WebResourceDto webResource);
    }
}
