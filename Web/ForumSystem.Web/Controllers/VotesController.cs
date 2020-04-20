namespace ForumSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using ForumSystem.Data.Models;
    using ForumSystem.Services.Data;
    using ForumSystem.Web.ViewModels.Votes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : ControllerBase
    {
        private readonly IVotesService votesService;
        private readonly UserManager<ApplicationUser> userManager;

        public VotesController(IVotesService votesService, UserManager<ApplicationUser> userManager)
        {
            this.votesService = votesService;
            this.userManager = userManager;
        }

        // Route api/votes  with post method
        // Request body params -> {"postId":1, "isUpVote:true"}
        // Response -> {"VotesCount = 13"}
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<VoteResponseModel>> Post(VoteInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.votesService.VoteAsync(input.PostId, userId, input.IsUpVote);

            var votes = this.votesService.GetVotes(input.PostId);

            return new VoteResponseModel { VotesCount = votes };
        }

        // Another way to get back the number of votes but if we need to return other data than int
        // we have to add the complex object later so its better to add a complex object or a model at an
        // early stage, then we can add properties easily.
        // public async Task<ActionResult<int>> Post(VoteInputModel input)
        // {
        //    var userId = this.userManager.GetUserId(this.User);
        //    await this.votesService.VoteAsync(input.PostId, userId, input.IsUpVote);
        //    var votes = this.votesService.GetVotes(input.PostId);
        //    return votes;
        // }
    }
}
