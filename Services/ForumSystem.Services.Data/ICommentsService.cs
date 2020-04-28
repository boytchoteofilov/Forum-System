namespace ForumSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task CreateAsync(int postId, string content, string userId, int? parentId);
    }
}
