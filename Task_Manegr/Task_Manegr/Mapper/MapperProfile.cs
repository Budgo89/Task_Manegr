using AutoMapper;
using MetricsManager.Client;
using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            CreateMap<long, DateTimeOffset>().ConvertUsing(new LongToDateTimeOffsetConverter());
            CreateMap<DateTimeOffset, long>().ConvertUsing(new DateTimeOffsetToLongConverter());
        }
    }
}
