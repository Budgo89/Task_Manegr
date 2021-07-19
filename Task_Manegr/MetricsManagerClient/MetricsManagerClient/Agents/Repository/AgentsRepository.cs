using MetricsManagerClient.Agents.Interfaces;
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
    class AgentsRepository : IAgentsRepository
    {
        private readonly HttpClient _httpClient;
        private IConnectionManager _connectionManager;
        //ConnectionManager
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
                //var d = StreamToString(responseStream);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var a = JsonSerializer.DeserializeAsync<AgentApiResponse>(responseStream, options).Result;
                return a;
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
        public string StreamToString(Stream stream)
        {
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }


    }
}
