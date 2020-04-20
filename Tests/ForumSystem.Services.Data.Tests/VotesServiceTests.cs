namespace ForumSystem.Services.Data.Tests
{
    using System.Threading.Tasks;

    using ForumSystem.Data;
    using ForumSystem.Data.Models;
    using ForumSystem.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class VotesServiceTests
    {
        [Fact]
        public async Task AnyNumberOfDownVoteShouldCountOnce()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("x").Options;

            var repository = new EfRepository<Vote>(
                new ApplicationDbContext(options));

            var service = new VotesService(repository);
            for (int i = 0; i < 5; i++)
            {
                await service.VoteAsync(1, "1", false);
            }

            var votesCount = service.GetVotes(1);
            Assert.Equal(-1, votesCount);
        }
    }
}
