using MetricsManagerClient.Metrics.ApiResponse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MetricsManagerClient.Agents.Repository
{
    class MetricsRepository : IMetricsRepository
    {
        private readonly HttpClient _httpClient;
        private IConnectionManager _connectionManager;
        public MetricsRepository()
        {
            _httpClient = new HttpClient();
            _connectionManager = new ConnectionManager();
        }

        public CpuMetricApiResponse CpuMetricLoading(string id, string fromTime, string toTime)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{_connectionManager.GetConnection()}/api/metrics/cpu/agent/{id}/from/{fromTime}/to/{toTime}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.DeserializeAsync<CpuMetricApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public CpuMetricApiResponse CpuMetricLoadingAll(string fromTime, string toTime)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{_connectionManager.GetConnection()}/api/metrics/cpu/cluster/from/{fromTime}/to/{toTime}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.DeserializeAsync<CpuMetricApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public DotNetMetricsApiResponse DotNetMetricLoading(string id, string fromTime, string toTime)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{_connectionManager.GetConnection()}/api/metrics/dotnet/agent/{id}/from/{fromTime}/to/{toTime}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.DeserializeAsync<DotNetMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public DotNetMetricsApiResponse DotNetMetricLoadingAll(string fromTime, string toTime)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{_connectionManager.GetConnection()}/api/metrics/dotnet/cluster/from/{fromTime}/to/{toTime}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.DeserializeAsync<DotNetMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public HddMetricsApiResponse HddMetricLoading(string id, string fromTime, string toTime)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{_connectionManager.GetConnection()}/api/metrics/hdd/agent/{id}/from/{fromTime}/to/{toTime}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.DeserializeAsync<HddMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public HddMetricsApiResponse HddMetricLoadingAll(string fromTime, string toTime)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{_connectionManager.GetConnection()}/api/metrics/hdd/cluster/from/{fromTime}/to/{toTime}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.DeserializeAsync<HddMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public NetworkMetricsApiResponse NetworkMetricLoading(string id, string fromTime, string toTime)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{_connectionManager.GetConnection()}/api/metrics/network/agent/{id}/from/{fromTime}/to/{toTime}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.DeserializeAsync<NetworkMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public NetworkMetricsApiResponse NetworkMetricLoadingAll(string fromTime, string toTime)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{_connectionManager.GetConnection()}/api/metrics/network/cluster/from/{fromTime}/to/{toTime}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.DeserializeAsync<NetworkMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public RamMetricsApiResponse RamkMetricLoading(string id, string fromTime, string toTime)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{_connectionManager.GetConnection()}/api/metrics/ram/agent/{id}/from/{fromTime}/to/{toTime}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.DeserializeAsync<RamMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public RamMetricsApiResponse RamMetricLoadingAll(string fromTime, string toTime)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{_connectionManager.GetConnection()}/api/metrics/ram/cluster/from/{fromTime}/to/{toTime}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.DeserializeAsync<RamMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
