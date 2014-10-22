﻿namespace JobHunters.Data.UnitOfWork
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using JobHunters.Data.Repositories;
    using JobHunters.Data.UnitOfWork;
    using JobHunters.Models;
   
    public class ApplicationData : IJobHuntersData
    {
        private DbContext context;
        private IDictionary<System.Type, object> repositories;

        public ApplicationData(ApplicationDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<System.Type, object>();
        }

        public JobPostsRepository JobPosts
        {
            get
            {
                return (JobPostsRepository)this.GetRepository<JobPost>();
            }
        }

        public UsersRepository Users
        {
            get
            {
                return (UsersRepository)this.GetRepository<ApplicationUser>();
            }
        }

        public CitiesRepository Cities
        {
            get
            {
                return (CitiesRepository)this.GetRepository<City>();
            }
        }

        public CategoriesRepository Categories
        {
            get
            {
                return (CategoriesRepository)this.GetRepository<Category>();
            }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);

            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(GenericRepository<T>);

                if (typeOfModel.IsAssignableFrom(typeof(JobPost)))
                {
                    type = typeof(JobPostsRepository);
                }
                else if (typeOfModel.IsAssignableFrom(typeof(ApplicationUser)))
                {
                    type = typeof(UsersRepository);
                }
                else if (typeOfModel.IsAssignableFrom(typeof(City)))
                {
                    type = typeof(CitiesRepository);
                }
                else if (typeOfModel.IsAssignableFrom(typeof(Category)))
                {
                    type = typeof(CategoriesRepository);
                }

                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IGenericRepository<T>)this.repositories[typeOfModel];
        }
    }
}