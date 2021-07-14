using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager
{
    public class DateTimeOffsetToLongConverter : ITypeConverter<DateTimeOffset, long>
    {
        public long Convert(DateTimeOffset source, long destination, ResolutionContext context)
        {
            return source.ToUnixTimeSeconds();
        }
    }
}
