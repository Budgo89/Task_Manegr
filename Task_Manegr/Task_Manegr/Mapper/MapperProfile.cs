using AutoMapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using System;

namespace MetricsManager
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, CpuMetricDto>();
            CreateMap<DotNetMetric, DotNetMetricDto>();
            CreateMap<HddMetrics, HddMetricDto>();
            CreateMap<NetworkMetric, NetworkMetricDto>();
            CreateMap<RamMetric, RamMetricDto>();
            CreateMap<HddMetricInquiry, HddMetricDto>();
            CreateMap<CpuMetricInquiry, CpuMetricDto>();
            CreateMap<DotNetMetricInquiry, DotNetMetricDto>();
            CreateMap<NetworkMetricInquiry, NetworkMetricDto>();
            CreateMap<RamMetricInquiry, RamMetricDto>();
            CreateMap<long, DateTimeOffset>().ConvertUsing(new LongToDateTimeOffsetConverter());
            CreateMap<DateTimeOffset, long>().ConvertUsing(new DateTimeOffsetToLongConverter());
        }
    }
}
