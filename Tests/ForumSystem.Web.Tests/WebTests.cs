namespace ForumSystem.Web.Tests
{
    using System;
    using System.Net;
    using System.Reflection;
    using System.Threading.Tasks;

    using ForumSystem.Data;
    using ForumSystem.Data.Models;
    using ForumSystem.Data.Repositories;
    using ForumSystem.Services.Data;
    using ForumSystem.Services.Mapping;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class WebTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> server;

        public WebTests(WebApplicationFactory<Startup> server)
        {
            this.server = server;

            // TODO:(TOCHECK) If mapping registration is in the test only, the test runs correctly only
            // when is rum by itself. If you run all tests it fails.
            AutoMapperConfig.RegisterMappings(typeof(TestPost).GetTypeInfo().Assembly);
        }

        [Fact]
        public void TestGetPostById()
        {
            var dbCtxOptionB = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(dbCtxOptionB.Options));

            repository.AddAsync(new Post
            {
                Title = "Test",
            })
            .GetAwaiter().GetResult();

            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var postService = new PostsService(repository);

            var post = postService.GetById<TestPost>(1);

            Assert.Equal("Test", post.Title);
        }

        [Fact(Skip = "Example test. Disabled for CI.")]
        public async Task IndexPageShouldReturnStatusCode200WithTitle()
        {
            var client = this.server.CreateClient();
            var response = await client.GetAsync("/");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("<title>", responseContent);
        }

        [Fact(Skip = "Example test. Disabled for CI.")]
        public async Task AccountManagePageRequiresAuthorization()
        {
            var client = this.server.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
            var response = await client.GetAsync("Identity/Account/Manage");
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        }

        public class TestPost : IMapFrom<Post>
        {
            public string Title { get; set; }
        }
    }
}
