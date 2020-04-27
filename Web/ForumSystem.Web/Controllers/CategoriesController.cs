namespace ForumSystem.Web.Controllers
{
    using ForumSystem.Services.Data;
    using ForumSystem.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;
    using System;

    public class CategoriesController : Controller
    {
        private readonly int itemsPerPage = 5;

        private readonly ICategoriesService categoriesService;
        private readonly IPostsService postsService;

        public CategoriesController(ICategoriesService categoriesService, IPostsService postsService)
        {
            this.categoriesService = categoriesService;
            this.postsService = postsService;
        }

        public IActionResult ByName(string name, int page = 1)
        {
            var viewModel = this.categoriesService.GetByName<CategoryViewModel>(name);
            viewModel.ForumPosts = this.postsService.GetPostsByCategoryIdPaged<PostInCategoryViewModel>(viewModel.Id, this.itemsPerPage, (page - 1) * this.itemsPerPage);

            var postsCount = this.postsService.GetPostsCountByCategory(viewModel.Id);
            viewModel.PagesCount = (int)Math.Ceiling((double)postsCount / this.itemsPerPage);
            viewModel.CurrentPage = page;
            return this.View(viewModel);
        }
    }
}
