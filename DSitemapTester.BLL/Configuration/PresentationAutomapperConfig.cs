using AutoMapper;
using DSitemapTester.BLL.Dtos;
using DSitemapTester.Tester.Dtos;
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
                config.CreateMap<WebResourceDto, PresentationWebResourceDto>()
                .ForMember(
                         e => e.Url,
                         opt => opt.MapFrom(
                             res => res.Url));

                config.CreateMap<WebResourceDto, PresentationWebResourceTestDto>()
                .ForMember(
                         e => e.Date,
                         opt => opt.MapFrom(
                             res => res.Tests.First().Date))
                .ForMember(
                         e => e.Duration,
                         opt => opt.MapFrom(
                             res => Convert.ToDouble((res.Tests.Last().Date - res.Tests.First().Date).TotalSeconds)))
                .ForMember(
                         e => e.Tests,
                         opt => opt.MapFrom(
                             res => res.Tests));

                config.CreateMap<TestDto, PresentationTestDto>()
                 .ForMember(
                         e => e.Url,
                         opt => opt.MapFrom(
                             res => res.Url))
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
                                         new TestResultDto()
                                         {
                                             ResponseTime = pres.TestResults.Average(resp => resp.ResponseTime)
                                         }
                                     ).First()));

                config.CreateMap<TestResultDto, PresentationTestResultDto>()
                .ForMember(
                         e => e.ResponseTime,
                         opt => opt.MapFrom(
                             res => res.ResponseTime));
            });
        }
    }
}
