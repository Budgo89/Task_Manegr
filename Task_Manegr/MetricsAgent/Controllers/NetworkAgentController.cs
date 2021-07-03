using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkAgentController : ControllerBase
    {
        private INetworkMetricsRepository repository;
        private readonly ILogger<NetworkAgentController> _logger;
        private readonly IMapper mapper;
        public NetworkAgentController(INetworkMetricsRepository repository, ILogger<NetworkAgentController> logger, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в NetworkAgentController");
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetAgentFromAgent([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Входные данные {fromTime} , {toTime}", fromTime, toTime);
            fromTime = new DateTimeOffset(fromTime.UtcDateTime);
            toTime = new DateTimeOffset(toTime.UtcDateTime);
            var metrics = repository.GetByTimePeriod(fromTime, toTime);
            var response = new AllNetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(mapper.Map<NetworkMetricDto>(metric));
            }

            return Ok(response);
        }
    }

}
