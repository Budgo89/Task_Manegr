using AutoMapper;
using MetricsManager.Controllers;
using MetricsManager.DAL.Models;
using MetricsManager.Repository;
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
    public class RamMetricsControllerUnitTests
    {
        private RamMetricsController controller;
        private Mock<ILogger<RamMetricsController>> _loggerMock;
        private Mock<IRamMetricRepository> _repository;
        private Mock<IMapper> _mapper;
        public RamMetricsControllerUnitTests()
        {
            _loggerMock = new Mock<ILogger<RamMetricsController>>();
            _repository = new Mock<IRamMetricRepository>();
            _mapper = new Mock<IMapper>();
            controller = new RamMetricsController(_loggerMock.Object, _repository.Object, _mapper.Object);
        }
        [Fact]
        public void RamMetricsController_GetMetricsFromAgent_ReturnsOk()
        {
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            _repository.Setup(repository => repository.GetByTimePeriod(agentId, fromTime, toTime)).Returns(new List<RamMetricInquiry>());
            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void RamMetricsController_GetMetricsFromAllCluster_ReturnsOk()
        {
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            _repository.Setup(repository => repository.GetByAllTimePeriod(fromTime, toTime)).Returns(new List<RamMetricInquiry>());
            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
