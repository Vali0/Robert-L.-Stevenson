﻿namespace JobHunters.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Policy;

    public class JobPost
    {
        private ICollection<JobApplication> applicants;

        private ICollection<ApplicationUser> viewers; 

        public JobPost()
        {
            this.applicants=new HashSet<JobApplication>();
            this.viewers=new HashSet<ApplicationUser>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        [MinLength(3)]
        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string AuthorId { get; set; }

        [Required]
        public virtual ApplicationUser Author { get; set; }

        public int Views { get; set; }

        [Required]
        public int CityId { get; set; }

        public virtual City City { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Required]
        public OfferType OfferType { get; set; }

        [Required]
        public HierarchyLevel HierarchyLevel { get; set; }

        [Required]
        public WorkEmployment WorkEmployement { get; set; }

        public string ProfileImage { get; set; }

        public virtual ICollection<JobApplication> Applicants
        {
            get
            {
                return this.applicants;
            }
            set
            {
                this.applicants = value;
            }
        }

        public virtual ICollection<ApplicationUser> Viewers
        {
            get
            {
                return this.viewers;
            }
            set
            {
                this.viewers = value;
            }
        } 
    }
}