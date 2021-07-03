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
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetAgentController : ControllerBase
    {
        private readonly ILogger<DotNetAgentController> _logger;
        private IDotNetMetricsRepository repository;
        private readonly IMapper mapper;
        public DotNetAgentController(IDotNetMetricsRepository repository, ILogger<DotNetAgentController> logger, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в DotNetAgentController");
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetAgentFromAgent([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Входные данные {fromTime} , {toTime}", fromTime, toTime);
            fromTime = new DateTimeOffset(fromTime.UtcDateTime);
            toTime = new DateTimeOffset(toTime.UtcDateTime);
            var metrics = repository.GetByTimePeriod(fromTime, toTime);
            var response = new AllDotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(mapper.Map<DotNetMetricDto>(metric));
            }

            return Ok(response);
        }
    }

}
