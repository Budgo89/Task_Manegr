using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsAgentTests
{
    public class CpuAgentControllerUnitTests
    {
        private CpuAgentController controller;

        public CpuAgentControllerUnitTests()
        {
            controller = new CpuAgentController();
        }

        [Fact]
        public void CpuAgentController_GetMetricsFromAgent_ReturnsOk()
        {            
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
                        
            var result = controller.GetAgentFromAgent(fromTime, toTime);
                       
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
    public class DotNetAgentControllerUnitTests
    {
        private DotNetAgentController controller;

        public DotNetAgentControllerUnitTests()
        {
            controller = new DotNetAgentController();
        }

        [Fact]
        public void DotNetAgentController_GetMetricsFromAgent_ReturnsOk()
        {
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            var result = controller.GetAgentFromAgent(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
    public class HddAgentControllerUnitTests
    {
        private HddAgentController controller;

        public HddAgentControllerUnitTests()
        {
            controller = new HddAgentController();
        }

        [Fact]
        public void DotNetAgentController_GetMetricsFromAgent_ReturnsOk()
        {
            
            var result = controller.GetAgentFromAgent();

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
    public class NetworkAgentControllerUnitTests
    {
        private NetworkAgentController controller;

        public NetworkAgentControllerUnitTests()
        {
            controller = new NetworkAgentController();
        }

        [Fact]
        public void DotNetAgentController_GetMetricsFromAgent_ReturnsOk()
        {
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            var result = controller.GetAgentFromAgent(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
    public class RamAgentControllerUnitTests
    {
        private RamAgentController controller;

        public RamAgentControllerUnitTests()
        {
            controller = new RamAgentController();
        }

        [Fact]
        public void DotNetAgentController_GetMetricsFromAgent_ReturnsOk()
        {

            var result = controller.GetAgentFromAgent();

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
