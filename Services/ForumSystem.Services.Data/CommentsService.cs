namespace ForumSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using ForumSystem.Data.Common.Repositories;
    using ForumSystem.Data.Models;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public async Task CreateAsync(int postId, string content, string userId, int? parentId = null)
        {
            var comment = new Comment
            {
                PostId = postId,
                ParentId = parentId,
                Content = content,
                UserId = userId,
            };

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
        }

        public bool IsInPostId(int? commentId, int postId)
        {
            var commentPostId = this.commentsRepository
                .All()
                .Where(comm => comm.Id == commentId)
                .Select(x => x.PostId)
                .FirstOrDefault();

            return commentPostId == postId;
        }
    }
}
