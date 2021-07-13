using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Models
{
    public class HddMetricsJsonSerializerOptions
    {
        public int id { get; set; }

        public long value { get; set; }

        public DateTimeOffset time { get; set; }
    }
}
