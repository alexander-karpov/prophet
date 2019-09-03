using NUnit.Framework;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;

namespace Prophet.Tests.Server
{
    public class ProviderStateApiFactory : WebApplicationFactory<Prophet.Startup>
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder()
                .UseStartup<Prophet.Startup>();
        }
    }

    [TestFixture()]
    public class Tests
    {
        ProviderStateApiFactory _factory;

        [OneTimeSetUp]
        public void GivenARequestToTheController()
        {
            _factory = new ProviderStateApiFactory();
        }

        // [Test]
        // public async Task Test1()
        // {
        //     var client = _factory.CreateClient();

        //     var response = await client.PostAsync("/webhook", new StringContent("{\"action\": \"мое событие\", \"queryText\": \"ноги\", \"userId\": \"123\" }"));

        //     Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        // }

        [Test]
        public async Task Test_Health()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/webhook");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
