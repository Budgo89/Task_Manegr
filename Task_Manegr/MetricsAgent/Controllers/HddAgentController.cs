using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddAgentController : ControllerBase
    {
        private IHddMetricsRepository repository;
        private readonly ILogger<HddAgentController> _logger;

        public HddAgentController(IHddMetricsRepository repository,ILogger<HddAgentController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в HddAgentController");
            this.repository = repository;
        }

        [HttpGet("left/from/{fromTime}/to/{toTime}")]
        public IActionResult GetAgentFromAgent([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Входные данные {fromTime} , {toTime}", fromTime, toTime);
            fromTime = new DateTimeOffset(fromTime.UtcDateTime);
            toTime = new DateTimeOffset(toTime.UtcDateTime);

            var metrics = repository.GetByTimePeriod(fromTime, toTime);

            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new HddMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }
    }
    public class HddMetric
    {
        public int Id { get; set; }

        public long Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
