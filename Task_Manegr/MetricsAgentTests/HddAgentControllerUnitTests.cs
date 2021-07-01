using AutoMapper;
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
    public class HddAgentControllerUnitTests
    {
        private HddAgentController controller;
        private Mock<IHddMetricsRepository> repMock;
        private Mock<ILogger<HddAgentController>> _loggerMock;
        private Mock<IMapper> _mapper;

        public HddAgentControllerUnitTests()
        {
            _loggerMock = new Mock<ILogger<HddAgentController>>();
            repMock = new Mock<IHddMetricsRepository>();
            _mapper = new Mock<IMapper>();
            controller = new HddAgentController(repMock.Object, _loggerMock.Object, _mapper.Object);
        }

        [Fact]
        public void DotNetAgentController_GetMetricsFromAgent_ReturnsOk()
        {
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            repMock.Setup(repository => repository.GetByTimePeriod(fromTime, toTime)).Returns(new List<HddMetric>());
            var result = controller.GetAgentFromAgent(fromTime, toTime);
            repMock.Verify(repository => repository.GetByTimePeriod(fromTime, toTime), Times.AtLeastOnce());
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
