namespace ForumSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ForumSystem.Data.Common.Repositories;
    using ForumSystem.Data.Models;
    using ForumSystem.Services.Mapping;

    public class PostsService : IPostsService
    {
        private readonly IDeletableEntityRepository<Post> postsRepository;

        public PostsService(IDeletableEntityRepository<Post> postsRepository)
        {
            this.postsRepository = postsRepository;
        }

        public async Task<int> CreateAsync(string title, string content, int categoryId, string userId)
        {
            var post = new Post
            {
                Title = title,
                Content = content,
                CategoryId = categoryId,
                UserId = userId,
            };

            await this.postsRepository.AddAsync(post);
            await this.postsRepository.SaveChangesAsync();

            return post.Id;
        }

        public T GetById<T>(int postId)
        {
            var post = this.postsRepository.All()
                .Where(x => x.Id == postId)
                .To<T>()
                .FirstOrDefault();

            return post;
        }

        public IEnumerable<T> GetPostsByCategoryIdPaged<T>(int categoryId, int postsPerPage, int postsSkiped)
        {
            var queryToPosts = this.postsRepository
                .All()
                .OrderByDescending(post => post.CreatedOn)
                .Where(category => category.CategoryId == categoryId)
                .Skip(postsSkiped)
                .Take(postsPerPage)
                .To<T>()
                .ToList();

            return queryToPosts;
        }

        public int GetPostsCountByCategory(int categoryId)
        {
            var postsCount = this.postsRepository
                .All()
                .Where(category => category.CategoryId == categoryId).Count();

            return postsCount;
        }
    }
}
