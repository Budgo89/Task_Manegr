using AutoMapper;
using MetricsManager.Controllers;
using MetricsManager.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MetricsManagerTests
{
    public class DotNetMetricsControllerUnitTests
    {
        private DotNetMetricsController controller;
        private Mock<ILogger<DotNetMetricsController>> _loggerMock;
        private Mock<IDotNetMetricRepository> _repository;
        private Mock<IMapper> _mapper;
        public DotNetMetricsControllerUnitTests()
        {
            _loggerMock = new Mock<ILogger<DotNetMetricsController>>();
            _repository = new Mock<IDotNetMetricRepository>();
            _mapper = new Mock<IMapper>();
            controller = new DotNetMetricsController(_loggerMock.Object, _repository.Object, _mapper.Object);
        }
        [Fact]
        public void DotNetMetricsController_GetMetricsFromAgent_ReturnsOk()
        {
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            _repository.Setup(repository => repository.GetByTimePeriod(agentId, fromTime, toTime)).Returns(new List<DotNetMetricInquiry>());
            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void DotNetMetricsController_GetMetricsFromAllCluster_ReturnsOk()
        {
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            _repository.Setup(repository => repository.GetByAllTimePeriod(fromTime, toTime)).Returns(new List<DotNetMetricInquiry>());
            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
