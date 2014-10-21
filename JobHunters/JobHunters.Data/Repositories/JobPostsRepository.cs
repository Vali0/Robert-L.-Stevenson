namespace JobHunters.Data.Repositories
{
    using System.Data.Entity;

    using JobHunters.Models;

    public class JobPostsRepository : GenericRepository<JobPost>, IGenericRepository<JobPost>
    {
        public JobPostsRepository(DbContext context)
            : base(context)
        {
        }
    }
}