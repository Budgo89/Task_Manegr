using MetricsManager.DAL.Models;
using MetricsManager.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Interfaces
{
    public interface IDotNetMetricRepository : IRepository<DotNetMetricInquiry>
    {
        public DateTimeOffset FromTime();
        public void Create(List<DotNetMetricDto> Metrics);
    }
}
