using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Repository
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetByTimePeriod(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime);
    }
}
