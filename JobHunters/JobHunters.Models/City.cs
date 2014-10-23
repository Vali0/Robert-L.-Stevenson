namespace JobHunters.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class City:IComparable<City>,IComparable
    {
        private ICollection<JobPost> jobPosts;

        public City()
        {
            this.jobPosts = new HashSet<JobPost>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<JobPost> JobPosts
        {
            get
            {
                return this.jobPosts;
            }
            set
            {
                this.jobPosts = value;
            }
        }

        public int CompareTo(City other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public int CompareTo(object obj)
        {
            var other = obj as City;
            return this.Name.CompareTo(other.Name);
        }
    }
}
