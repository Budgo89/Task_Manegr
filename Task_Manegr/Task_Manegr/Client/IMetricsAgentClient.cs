using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Client
{
    public interface IMetricsAgentClient
    {
        //AllRamMetricsApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request);

        AllHddMetricsApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request);

        //AllDonNetMetricsApiResponse GetDonNetMetrics(GetAllDonNetHeapMetrisApiRequest request);

        //AllCpuMetricsApiResponse GetCpuMetrics(GetAllCpuMetricsApiRequest request);

        //AllNetworkMetricsApiRespodse GetNetworkMetrics(GetAllNetworkMetricsApiRespodse request);
    }
}
