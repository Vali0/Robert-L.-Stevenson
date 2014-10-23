namespace JobHunters.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using JobHunters.Data.Migrations;
    using JobHunters.Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual IDbSet<JobPost> JobPosts { get; set; }

        public virtual IDbSet<City> Cities { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<JobApplication> JobApplications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<JobApplication>().HasRequired(j=>j.JobPost).WithMany().WillCascadeOnDelete(false);
        }
    }
}