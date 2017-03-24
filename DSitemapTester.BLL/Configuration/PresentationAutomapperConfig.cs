using AutoMapper;
using DSitemapTester.BLL.Dtos;
using DSitemapTester.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSitemapTester.BLL.Configuration
{
    public class PresentationAutomapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<WebResource, PresentationWebResourceDto>()
                .ForMember(
                         e => e.Url,
                         opt => opt.MapFrom(
                             res => res.Url));

                config.CreateMap<WebResourceTest, PresentationWebResourceTestDto>()
                .ForMember(
                         e => e.Tests,
                         opt => opt.MapFrom(
                             res => res.Tests));

                config.CreateMap<Test, PresentationTestDto>()
                 .ForMember(
                         e => e.Url,
                         opt => opt.MapFrom(
                             res => res.SitemapResource.Url))
                .ForMember(
                         e => e.TestsCount,
                         opt => opt.MapFrom(
                             res => res.TestsCount))
                .ForMember(
                         e => e.MaximalResponseTime,
                         opt => opt.MapFrom(
                             res => res.TestResults.FirstOrDefault(max => max.ResponseTime == res.TestResults.Max(resp => resp.ResponseTime))))
                .ForMember(
                         e => e.MinimalResponseTime,
                         opt => opt.MapFrom(
                             res => res.TestResults.FirstOrDefault(min => min.ResponseTime == res.TestResults.Min(resp => resp.ResponseTime))))
                .ForMember(
                         e => e.AverageResponseTime,
                         opt => opt.MapFrom(
                             pres => pres.TestResults.Select(obj =>
                                         new TestResult()
                                         {
                                             ResponseTime = Math.Round(pres.TestResults.Average(resp => resp.ResponseTime), 3)
                                         }
                                     ).First()))
                .ForMember(
                         e => e.WrongTestsCount,
                         opt => opt.MapFrom(
                             res => res.TestResults.Where(test => test.ResponseTime == 0).Count()));

                config.CreateMap<TestResult, PresentationTestResultDto>()
                .ForMember(
                         e => e.ResponseTime,
                         opt => opt.MapFrom(
                             res => Math.Round(res.ResponseTime, 3)));
            });
        }
    }
}
