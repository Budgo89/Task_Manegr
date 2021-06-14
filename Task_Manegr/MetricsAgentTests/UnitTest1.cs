using MetricsAgent;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MetricsAgentTests
{
    public class CpuAgentControllerUnitTests
    {
        private CpuAgentController controller;
        private Mock<ICpuMetricsRepository> repMock;
        private Mock<ILogger<CpuAgentController>> _loggerMock;
        public CpuAgentControllerUnitTests()
        {
            _loggerMock = new Mock<ILogger<CpuAgentController>>();
            repMock = new Mock<ICpuMetricsRepository>();
            controller = new CpuAgentController(repMock.Object, _loggerMock.Object);
            
        }

        [Fact]
        public void CpuAgentController_GetMetricsFromAgent_ReturnsOk()
        {            
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);

            repMock.Setup(repository => repository.GetAll()).Returns(new List<CpuMetric>());

            var result = controller.GetAgentFromAgent(fromTime, toTime);
            repMock.Verify(repository => repository.GetAll(), Times.AtLeastOnce());

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
    public class DotNetAgentControllerUnitTests
    {
        private DotNetAgentController controller;
        private Mock<IDotNetMetricsRepository> repMock;
        private Mock<ILogger<DotNetAgentController>> _loggerMock;

        public DotNetAgentControllerUnitTests()
        {
            _loggerMock = new Mock<ILogger<DotNetAgentController>>();
            repMock = new Mock<IDotNetMetricsRepository>();
            controller = new DotNetAgentController(repMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void DotNetAgentController_GetMetricsFromAgent_ReturnsOk()
        {
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            repMock.Setup(repository => repository.GetAll()).Returns(new List<DotNetMetric>());
            var result = controller.GetAgentFromAgent(fromTime, toTime);
            repMock.Verify(repository => repository.GetAll(), Times.AtLeastOnce());
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
    public class HddAgentControllerUnitTests
    {
        private HddAgentController controller;
        private Mock<IHddMetricsRepository> repMock;
        private Mock<ILogger<HddAgentController>> _loggerMock;

        public HddAgentControllerUnitTests()
        {
            _loggerMock = new Mock<ILogger<HddAgentController>>();
            repMock = new Mock<IHddMetricsRepository>();
            controller = new HddAgentController(repMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void DotNetAgentController_GetMetricsFromAgent_ReturnsOk()
        {
            repMock.Setup(repository => repository.GetAll()).Returns(new List<HddMetric>());
            var result = controller.GetAgentFromAgent();
            repMock.Verify(repository => repository.GetAll(), Times.AtLeastOnce());
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
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

            repMock.Setup(repository => repository.GetAll()).Returns(new List<NetworkMetric>());
            var result = controller.GetAgentFromAgent(fromTime, toTime);
            repMock.Verify(repository => repository.GetAll(), Times.AtLeastOnce());
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
    public class RamAgentControllerUnitTests
    {
        private RamAgentController controller;
        private Mock<IRamMetricsRepository> repMock;
        private Mock<ILogger<RamAgentController>> _loggerMock;

        public RamAgentControllerUnitTests()
        {
            _loggerMock = new Mock<ILogger<RamAgentController>>();
            repMock = new Mock<IRamMetricsRepository>();
            controller = new RamAgentController(repMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void DotNetAgentController_GetMetricsFromAgent_ReturnsOk()
        {
            repMock.Setup(repository => repository.GetAll()).Returns(new List<RamMetric>());
            var result = controller.GetAgentFromAgent();
            repMock.Verify(repository => repository.GetAll(), Times.AtLeastOnce());
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
