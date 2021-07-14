using MetricsManager.DAL.Models;
using MetricsManager.Responses;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MetricsManager.Client
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;
        public MetricsAgentClient(HttpClient httpClient, ILogger<MetricsAgentClient> logger)
        {
            _httpClient = httpClient;
            
            _logger = logger;
        }
        public AllHddMetricsApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request)
        {
            var fromParameter = request.FromTime;
            var toParameter = request.ToTime;
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}api/metrics/hdd/left/from/{fromParameter.ToString("yyyy-MM-ddTHH:mm:ssZ")}/to/{toParameter.ToString("yyyy-MM-ddTHH:mm:ssZ")}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var a = JsonSerializer.DeserializeAsync<AllHddMetricsApiResponse>(responseStream, options).Result;
                return a;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

            }
            return new AllHddMetricsApiResponse();
        }

        public AllRamMetricsApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request)
        {
            var fromParameter = request.FromTime;
            var toParameter = request.ToTime;
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}api/metrics/ram/available/from/{fromParameter.ToString("yyyy-MM-ddTHH:mm:ssZ")}/to/{toParameter.ToString("yyyy-MM-ddTHH:mm:ssZ")}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var a = JsonSerializer.DeserializeAsync<AllRamMetricsApiResponse>(responseStream, options).Result;
                return a;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

            }
            return new AllRamMetricsApiResponse();
        }

        public AllCpuMetricsApiResponse GetAllCpuMetrics(GetAllCpuMetricsApiRequest request)
        {
            var fromParameter = request.FromTime;
            var toParameter = request.ToTime;
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}api/metrics/cpu/from/{fromParameter.ToString("yyyy-MM-ddTHH:mm:ssZ")}/to/{toParameter.ToString("yyyy-MM-ddTHH:mm:ssZ")}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var a = JsonSerializer.DeserializeAsync<AllCpuMetricsApiResponse>(responseStream, options).Result;
                return a;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return new AllCpuMetricsApiResponse();
        }

        public AllDotNetMetricsApiResponse GetAllDotNetMetrics(GetAllDotNetHeapMetrisApiRequest request)
        {
            var fromParameter = request.FromTime;
            var toParameter = request.ToTime;
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}api/metrics/dotnet/errors-count/from/{fromParameter.ToString("yyyy-MM-ddTHH:mm:ssZ")}/to/{toParameter.ToString("yyyy-MM-ddTHH:mm:ssZ")}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var a = JsonSerializer.DeserializeAsync<AllDotNetMetricsApiResponse>(responseStream, options).Result;
                return a;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

            }
            return new AllDotNetMetricsApiResponse();
        }

        public AllNetworkMetricsApiRespodse GetAllNetworkMetrics(GetAllNetworkMetricsApiRespodse request)
        {
            var fromParameter = request.FromTime;
            var toParameter = request.ToTime;
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}api/metrics/network/from/{fromParameter.ToString("yyyy-MM-ddTHH:mm:ssZ")}/to/{toParameter.ToString("yyyy-MM-ddTHH:mm:ssZ")}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var a = JsonSerializer.DeserializeAsync<AllNetworkMetricsApiRespodse>(responseStream, options).Result;
                return a;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

            }
            return new AllNetworkMetricsApiRespodse();
        }
    }
}
