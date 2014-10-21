namespace JobHunters.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class City
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
    }
}
