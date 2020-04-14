namespace ForumSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using ForumSystem.Data.Common.Repositories;
    using ForumSystem.Data.Models;
    using ForumSystem.Services.Mapping;

    // if there is available repository we use it to create the service,
    // otherwise we use the dbContext in the constructor.
    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Category> query = this.categoriesRepository.All().OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query.Take(count.Value);
            }

            // tolist() materializes the query to the database and returns the list of values
            // good to be done here not to return IQueryable object
            return query.To<T>().ToList();
        }

        public T GetByName<T>(string name)
        {
            var category = this.categoriesRepository.All().Where(x => x.Name == name)
                .To<T>().FirstOrDefault();

            return category;
        }
    }
}
