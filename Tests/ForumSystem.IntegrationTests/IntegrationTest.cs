namespace ForumSystem.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using ForumSystem.Data;
    using ForumSystem.Web;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public class IntegrationTest
    {
        private readonly HttpClient testClient;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        // setting the use of in memory database and excluding the real
                        services.RemoveAll(typeof(ApplicationDbContext));
                        services.AddDbContext<ApplicationDbContext>(options => { options.UseInMemoryDatabase("InMemoryTest"); });
                    });
                });
            this.testClient = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            this.testClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await this.GetJwtAsync());
        }

        private async Task<string> GetJwtAsync()
        {
            var user = await this.testClient.PostAsJsonAsync(" ", new { });
            return " ";
        }
    }
}
