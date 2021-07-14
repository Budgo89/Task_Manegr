using AutoMapper;
using MetricsManager.Controllers;
using MetricsManager.DAL.Models;
using MetricsManager.Jobs;
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
    public class NetworkMetricsControllerUnitTests
    {
        private NetworkMetricsController controller;
        private Mock<ILogger<NetworkMetricsController>> _loggerMock;
        private Mock<INetworkMetricRepository> _repository;
        private Mock<IMapper> _mapper;
        public NetworkMetricsControllerUnitTests()
        {
            _loggerMock = new Mock<ILogger<NetworkMetricsController>>();
            _repository = new Mock<INetworkMetricRepository>();
            _mapper = new Mock<IMapper>();
            controller = new NetworkMetricsController(_loggerMock.Object, _repository.Object, _mapper.Object);
        }
        [Fact]
        public void NetworkMetricsController_GetMetricsFromAgent_ReturnsOk()
        {
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            _repository.Setup(repository => repository.GetByTimePeriod(agentId, fromTime, toTime)).Returns(new List<NetworkMetricInquiry>());
            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void NetworkMetricsController_GetMetricsFromAllCluster_ReturnsOk()
        {
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            _repository.Setup(repository => repository.GetByAllTimePeriod(fromTime, toTime)).Returns(new List<NetworkMetricInquiry>());
            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
