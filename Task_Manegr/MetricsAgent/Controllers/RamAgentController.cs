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
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamAgentController : ControllerBase
    {
        private IRamMetricsRepository repository;
        private readonly ILogger<RamAgentController> _logger;
        private readonly IMapper mapper;
        public RamAgentController(IRamMetricsRepository repository, ILogger<RamAgentController> logger, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в RamAgentController");
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public IActionResult GetAgentFromAgent([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Входные данные {fromTime} , {toTime}", fromTime, toTime);

            fromTime = new DateTimeOffset(fromTime.UtcDateTime);
            toTime = new DateTimeOffset(toTime.UtcDateTime);
            var metrics = repository.GetByTimePeriod(fromTime, toTime);
            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(mapper.Map<RamMetricDto>(metric));
            }

            return Ok(response);
        }
    }

}
