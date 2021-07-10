using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MetricsManager.Client
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;
        //public MetricsAgentClient(HttpClient httpClient, ILogger logger)
            public MetricsAgentClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            //_logger = logger;
        }
        public AllHddMetricsApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request)
        {
            var fromParameter = request.FromTime;
            var toParameter = request.ToTime;
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}/api/metrics/hdd/left/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;

                var a = JsonSerializer.DeserializeAsync<AllHddMetricsApiResponse>(responseStream).Result;
                return a;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                return null;
            }            
        }

        //public AllRamMetricsApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request)
        //{
        //    throw new NotImplementedException();
        //}

        //public AllCpuMetricsApiResponse GetCpuMetrics(GetAllCpuMetricsApiRequest request)
        //{
        //    throw new NotImplementedException();
        //}

        //public AllDonNetMetricsApiResponse GetDonNetMetrics(GetAllDonNetHeapMetrisApiRequest request)
        //{
        //    throw new NotImplementedException();
        //}

        //public AllNetworkMetricsApiRespodse GetNetworkMetrics(GetAllNetworkMetricsApiRespodse request)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
