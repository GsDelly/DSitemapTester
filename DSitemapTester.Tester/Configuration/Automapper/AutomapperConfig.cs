using AutoMapper;
using DSitemapTester.Tester.Dtos;
using DSitemapTester.Tester.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.Tester.Configuration.Automapper
{
    public class AutomapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
            config.CreateMap<Test, TestDto>()
                .ForMember(
                         e => e.TestResults,
                         opt => opt.MapFrom(
                             res => res.TestResults))
                .ForMember(
                         e => e.Date,
                         opt => opt.MapFrom(
                             res => res.Date))
                .ForMember(
                         e => e.Url,
                         opt => opt.MapFrom(
                             res => res.Url))
                .ForMember(
                         e => e.TestsCount,
                         opt => opt.MapFrom(
                             res => res.TestsCount));

                config.CreateMap<TestResult, TestResultDto>()
                .ForMember(
                         e => e.ResponseTime,
                         opt => opt.MapFrom(
                             res => res.ResponseTime));
            });
        }
    }
}
