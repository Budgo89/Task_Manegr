using MetricsManagerClient.Agents.Interfaces;
using MetricsManagerClient.Agents.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MetricsManagerClient.Agents.Repository
{
    class AgentsRepository : IAgentsRepository
    {
        private readonly HttpClient _httpClient;
        private IConnectionManager _connectionManager;
        public AgentsRepository()
        {
            _httpClient = new HttpClient();
            _connectionManager = new ConnectionManager();
        }
        public AgentApiResponse ReceivingAgentById()
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{_connectionManager.GetConnection()}/api/Agents/receiving");
            try
            {                
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.DeserializeAsync<AgentApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void EnableAgent(string IdClientText)
        {            
            var httpRequest = new HttpRequestMessage(HttpMethod.Put, $"{_connectionManager.GetConnection()}/api/Agents/enable/{IdClientText}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void DisableAgent(string IdClientText)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Put, $"{_connectionManager.GetConnection()}/api/Agents/disable/{IdClientText}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void RegisterAgent(string IdClientText, string url)
        {
            var agentInfo = new AgentInfo
            {
                AgentId = Convert.ToInt32(IdClientText),
                AgentAddress = url
            };
            var agentInfoJson = JsonSerializer.Serialize(agentInfo);
            var client = new HttpClient();
            client.BaseAddress = new Uri($"{_connectionManager.GetConnection()}/api/Agents/register");
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, client.BaseAddress);
                request.Headers.Add("Accept", "application/json");
                request.Content = new StringContent(
                    agentInfoJson.ToString(),
                    Encoding.UTF8,
                    "application/json"
                    );
                client.SendAsync(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
