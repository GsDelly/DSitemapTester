using AutoMapper;
using DSitemapTester.Entities.Entities;
using DSitemapTester.Tester.Dtos;
using DSitemapTester.Tester.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.BLL.Configuration
{
    public class EntitiesAutomapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<TesterTest, Test>()
                .ForMember(
                         e => e.TestsCount,
                         opt => opt.MapFrom(
                             res => res.TestsCount));

                config.CreateMap<TesterTestResult, TestResult>()
                .ForMember(
                         e => e.ResponseTime,
                         opt => opt.MapFrom(
                             res => res.ResponseTime));
            });
        }
    }
}
