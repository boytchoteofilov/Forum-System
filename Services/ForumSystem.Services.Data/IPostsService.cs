namespace ForumSystem.Services.Data
{
    using System.Threading.Tasks;

    public interface IPostsService
    {
        // <int> for returning the Id of the newly created object
        Task<int> CreateAsync(string title, string content, int categoryId, string userId);

        T GetById<T>(int id);
    }
}
