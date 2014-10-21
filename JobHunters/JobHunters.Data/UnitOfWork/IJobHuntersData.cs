namespace JobHunters.Data.UnitOfWork
{
    using JobHunters.Data.Repositories;

    public interface IJobHuntersData
    {
        UsersRepository Users { get; }

        JobPostsRepository JobPosts { get; }

        CitiesRepository Cities { get; }

        CategoriesRepository Categories { get; }

        void SaveChanges();
    }
}