using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MetricsAgentTests
{
    public class NetworkAgentControllerUnitTests
    {

        private NetworkAgentController controller;
        private Mock<INetworkMetricsRepository> repMock;
        private Mock<ILogger<NetworkAgentController>> _loggerMock;

        public NetworkAgentControllerUnitTests()
        {
            _loggerMock = new Mock<ILogger<NetworkAgentController>>();
            repMock = new Mock<INetworkMetricsRepository>();
            controller = new NetworkAgentController(repMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void DotNetAgentController_GetMetricsFromAgent_ReturnsOk()
        {
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);

            repMock.Setup(repository => repository.GetByTimePeriod(fromTime, toTime)).Returns(new List<NetworkMetric>());
            var result = controller.GetAgentFromAgent(fromTime, toTime);
            repMock.Verify(repository => repository.GetByTimePeriod(fromTime, toTime), Times.AtLeastOnce());
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
