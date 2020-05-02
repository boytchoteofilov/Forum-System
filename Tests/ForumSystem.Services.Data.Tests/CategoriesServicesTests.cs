namespace ForumSystem.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using ForumSystem.Data;
    using ForumSystem.Data.Models;
    using ForumSystem.Data.Repositories;
    using ForumSystem.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class CategoriesServicesTests
    {
        public CategoriesServicesTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(MyCategory).GetTypeInfo().Assembly);
        }

        [Fact]
        public void GetByName_Should_()
        {

        }

        [Fact]
        public void GetByName_Should_ReturnOneCorrectCategory()
        {
            var dbContext = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var categoriesRepository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(dbContext));

            var service = new CategoriesService(categoriesRepository);

            for (int i = 1; i < 9; i++)
            {
                categoriesRepository.AddAsync(new Category { Id = i, Name = "Test" + i.ToString() });
            }

            categoriesRepository.SaveChangesAsync();

            // TODO: no need to mock the database, use moq when testng controllers
            var category = service.GetByName<MyCategory>("Test4");
            var category2 = service.GetByName<MyCategory>("Test5");

            Assert.Equal("Test4", category.Name);
            Assert.NotEqual("Test4", category2.Name);
            Assert.Equal(4, category.Id);
        }

        public class MyCategory : IMapFrom<Category>
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
