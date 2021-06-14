using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class CpuMetricsControllerUnitTests
    {
        private CpuMetricsController controller;
        private Mock<ILogger<CpuMetricsController>> _loggerMock;

        public CpuMetricsControllerUnitTests()
        {
            _loggerMock = new Mock<ILogger<CpuMetricsController>>();
            controller = new CpuMetricsController(_loggerMock.Object);
        }

        [Fact]
        public void CpuMetricsController_GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            //Act
            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void CpuMetricsController_GetMetricsFromAllCluster_ReturnsOk()
        {
            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            //Act
            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }

    public class DotNetMetricsControllerUnitTests
    {
        private DotNetMetricsController controller;
        public DotNetMetricsControllerUnitTests()
        {
            controller = new DotNetMetricsController();
        }
        [Fact]
        public void DotNetMetricsController_GetMetricsFromAgent_ReturnsOk()
        {
            var agentId = 1;
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void DotNetMetricsController_GetMetricsFromAllCluster_ReturnsOk()
        {
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }

    public class HddMetricsControllerUnitTests
    {
        private HddMetricsController controller;
        public HddMetricsControllerUnitTests()
        {
            controller = new HddMetricsController();
        }
        [Fact]
        public void HddMetricsController_GetMetricsFromAgent_ReturnsOk()
        {
            var agentId = 1;

            var result = controller.GetMetricsFromAgent(agentId);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void HddMetricsController_GetMetricsFromAllCluster_ReturnsOk()
        {
            var result = controller.GetMetricsFromAllCluster();

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }

    public class NetworkMetricsControllerUnitTests
    {
        private NetworkMetricsController controller;
        public NetworkMetricsControllerUnitTests()
        {
            controller = new NetworkMetricsController();
        }
        [Fact]
        public void NetworkMetricsController_GetMetricsFromAgent_ReturnsOk()
        {
            var agentId = 1;
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void NetworkMetricsController_GetMetricsFromAllCluster_ReturnsOk()
        {
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }

    public class RamMetricsControllerUnitTests
    {
        private RamMetricsController controller;
        public RamMetricsControllerUnitTests()
        {
            controller = new RamMetricsController();
        }
        [Fact]
        public void RamMetricsController_GetMetricsFromAgent_ReturnsOk()
        {
            var agentId = 1;

            var result = controller.GetMetricsFromAgent(agentId);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void RamMetricsController_GetMetricsFromAllCluster_ReturnsOk()
        {
            var result = controller.GetMetricsFromAllCluster();

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }

    public class AgentsControllerUnitTests
    {
        private AgentsController controller;
        public AgentsControllerUnitTests()
        {
            controller = new AgentsController();
        }
        [Fact]
        public void RegisterAgent_ReturnsOk()
        {

            var agentInfo = new AgentInfo() { };


            var result = controller.RegisterAgent(agentInfo);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void EnableAgentById_ReturnsOk()
        {
            var agentId = 1;

            var result = controller.EnableAgentById(agentId);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void DisableAgentById_ReturnsOk()
        {
            var agentId = 1;

            var result = controller.DisableAgentById(agentId);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }

}
