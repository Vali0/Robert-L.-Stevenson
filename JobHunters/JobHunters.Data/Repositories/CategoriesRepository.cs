namespace JobHunters.Data.Repositories
{
    using System.Data.Entity;

    using JobHunters.Models;

    public class CategoriesRepository : GenericRepository<Category>, IGenericRepository<Category>
    {
        public CategoriesRepository(DbContext context)
            : base(context)
        {
        }
    }
}