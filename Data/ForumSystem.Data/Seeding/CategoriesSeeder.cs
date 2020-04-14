namespace ForumSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using ForumSystem.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categories = new List<(string Name, string ImageUrl)>
            {
                ("Sport", "https://www.rutherford.school.nz/wp-content/uploads/2018/05/football.jpg"),
                ("Coronavirus", "https://i.guim.co.uk/img/media/33c5327d3e4ab08d4603e54bed7285180911dd0e/150_9_3708_2224/master/3708.jpg?width=300&quality=85&auto=format&fit=max&s=1126d502682356158ffcefe6adf870cd"),
                ("News", "https://www.lendacademy.com/wp-content/uploads/2015/05/Marketplace-Lending-News.jpg"),
                ("Music", "http://pamis.org.uk/site/uploads/musicworkshop-image.jpg"),
                ("Programming", "https://miro.medium.com/max/12032/0*Q8DDPKbl1Jek0i0a"),
                ("Cats", "https://www.rd.com/wp-content/uploads/2019/11/cat-10-e1573844975155.jpg"),
                ("Dogs", "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcSegYApNnkDiqxdGz3zm_AYtTPldMgfN8PNuGI_U1FCAJeIetWl&usqp=CAU"),
            };

            foreach (var category in categories)
            {
                await dbContext.Categories.AddAsync(new Category
                {
                    Name = category.Name,
                    Description = category.Name,
                    Title = category.Name,
                    ImageUrl = category.ImageUrl,
                });
            }
        }
    }
}
