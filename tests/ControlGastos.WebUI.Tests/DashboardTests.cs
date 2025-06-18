using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;

namespace ControlGastos.WebUI.Tests
{
    public class DashboardTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public DashboardTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Root_SeRedirigeADashboard()
        {
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            var resp = await client.GetAsync("/");
            Assert.Equal(HttpStatusCode.Redirect, resp.StatusCode);
            Assert.Equal("/Dashboard/Index", resp.Headers.Location?.OriginalString);
        }

        [Fact]
        public async Task Get_DashboardIndex_OkAndContainsTitulo()
        {
            var client = _factory.CreateClient();
            var html = await client.GetStringAsync("/Dashboard/Index");
            Assert.Contains("<h1>Dashboard Mensual</h1>", html);
        }
    }
}
