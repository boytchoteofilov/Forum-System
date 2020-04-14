namespace ForumSystem.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using ForumSystem.Data;
    using ForumSystem.Web.ViewModels;
    using ForumSystem.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        // one way of doing it using the DbContext class
        private readonly ApplicationDbContext db;

        public HomeController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();
            var ceategories = this.db.Categories.Select(x => new IndexCategoryViewModel
            {
                Description = x.Description,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Title = x.Title,
            }).ToList();

            viewModel.Categories = ceategories;

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
