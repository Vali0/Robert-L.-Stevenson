using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunters.Data.Repositories
{
    using System.Data.Entity;

    using JobHunters.Models;

    public class JobApplicationsRepository : GenericRepository<JobApplication>, IGenericRepository<JobApplication>
    {
        public JobApplicationsRepository(DbContext context)
            : base(context)
        {
        }
    }
}
