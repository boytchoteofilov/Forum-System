namespace ForumSystem.Web.Controllers
{
    using System.Diagnostics;

    using ForumSystem.Services.Data;
    using ForumSystem.Web.ViewModels;
    using ForumSystem.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        #region
        //// one way of doing it using the DbContext class
        // private readonly ApplicationDbContext db;

        // public HomeController(ApplicationDbContext db)
        // {
        //    this.db = db;
        // }

        // public IActionResult Index()
        // {
        //    var viewModel = new IndexViewModel();
        //    var ceategories = this.db.Categories.Select(x => new IndexCategoryViewModel
        //    {
        //        Description = x.Description,
        //        Name = x.Name,
        //        ImageUrl = x.ImageUrl,
        //        Title = x.Title,
        //    }).ToList();

        // viewModel.Categories = ceategories;

        // return this.View(viewModel);
        // }
        #endregion

        #region
        // private readonly IDeletableEntityRepository<Category> categoriesRepository;
        //// using repository with automapper
        //// thus selecting only properties from the view model from the database, thus less traffic

        // public HomeController(IDeletableEntityRepository<Category> categoriesRepository)
        // {
        //    this.categoriesRepository = categoriesRepository;
        // }

        // public IActionResult Index()
        // {
        //    var viewModel = new IndexViewModel();
        //    var ceategories = this.categoriesRepository.All()
        //        .To<IndexCategoryViewModel>()

        //// .Select(x => new IndexCategoryViewModel
        //    // {
        //    //    Description = x.Description,
        //    //    Name = x.Name,
        //    //    ImageUrl = x.ImageUrl,
        //    //    Title = x.Title,
        //    // })
        //        .ToList();

        // viewModel.Categories = ceategories;

        // return this.View(viewModel);
        // }
        #endregion

        // using service automapper is in the service class
        private readonly ICategoriesService categoriesService;

        public HomeController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();
            var ceategories = this.categoriesService.GetAll<IndexCategoryViewModel>();

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
