namespace JobHunters.Data.Repositories
{
    using System.Data.Entity;

    using JobHunters.Models;

    public class CitiesRepository : GenericRepository<City>, IGenericRepository<City>
    {
        public CitiesRepository(DbContext context)
            : base(context)
        {
        }
    }
}