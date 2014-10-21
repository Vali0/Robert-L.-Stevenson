namespace JobHunters.Data.Repositories
{
    using System.Data.Entity;

    using JobHunters.Models;

    public class UsersRepository : GenericRepository<ApplicationUser>, IGenericRepository<ApplicationUser>
    {
        public UsersRepository(DbContext context)
            : base(context)
        {
        }
    }
}