namespace ForumSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPostsService
    {
        // <int> for returning the Id of the newly created object
        Task<int> CreateAsync(string title, string content, int categoryId, string userId);

        T GetById<T>(int id);

        IEnumerable<T> GetPostsByCategoryIdPaged<T>(int categoryId, int take, int skip);

        int GetPostsCountByCategory(int categoryId);
    }
}
