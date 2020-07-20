namespace ForumSystem.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using System.Threading.Tasks;

    using ForumSystem.Web;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Xunit;
    using Xunit.Abstractions;

    public class HomePageTests
    {
        private readonly ITestOutputHelper outputHelper;

        public HomePageTests(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
        }

        [Fact]
        public async Task HomePageShouldHaveH1Title()
        {
            var serverFactory = new WebApplicationFactory<Startup>();
            var client = serverFactory.CreateClient();

            Stopwatch sw = Stopwatch.StartNew();

            var response = await client.GetAsync("/");
            var responseAsString = await response.Content.ReadAsStringAsync();

            this.outputHelper.WriteLine(sw.Elapsed.ToString());
            if (sw.Elapsed > new TimeSpan(0, 0, 5))
            {
                throw new Exception("Too long");
            }

            Assert.Contains("<h1", responseAsString);
        }
    }
}
