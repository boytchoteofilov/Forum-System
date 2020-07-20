namespace ForumSystem.IntegrationTests
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using ForumSystem.Web;
    using ForumSystem.Web.ViewModels.Votes;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Newtonsoft.Json;
    using Xunit;

    public class VotesControllerTests : IntegrationTest
    {
        private readonly HttpClient client;

        public VotesControllerTests()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            this.client = appFactory.CreateClient();
        }

        [Fact]
        public void VotesController_GetVotes_ShouldReturnCorrectCount()
        {
            var uri = new Uri("https://localhost:44319/api/votes?id=3");
            var response = this.client.GetAsync(uri);
        }

        [Fact]
        public async Task Post_Should_()
        {
            var path = "Api/Votes";

            var baseAddress = new Uri("https://localhost:5005");
            var client = new HttpClient();
            client.BaseAddress = baseAddress;

            var viewModel = new VoteInputModel();
            viewModel.PostId = 1;
            viewModel.IsUpVote = true;

            var viewModelAsJSON = JsonConvert.SerializeObject(viewModel);

            var buffer = System.Text.Encoding.UTF8.GetBytes(viewModelAsJSON);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(path, byteContent);
        }
    }
}
