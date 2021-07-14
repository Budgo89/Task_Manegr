using MetricsManager.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Client
{
    public interface IMetricsAgentClient
    {
        AllRamMetricsApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request);

        AllHddMetricsApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request);

        AllDotNetMetricsApiResponse GetAllDotNetMetrics(GetAllDotNetHeapMetrisApiRequest request);

        AllCpuMetricsApiResponse GetAllCpuMetrics(GetAllCpuMetricsApiRequest request);

        AllNetworkMetricsApiRespodse GetAllNetworkMetrics(GetAllNetworkMetricsApiRespodse request);
    }
}
