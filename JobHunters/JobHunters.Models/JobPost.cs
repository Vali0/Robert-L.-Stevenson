namespace JobHunters.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class JobPost
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
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
        public Type Type { get; set; }

        [Required]
        public HierarchyLevel HierarchyLevel { get; set; }

        [Required]
        public WorkEmployment WorkEmployement { get; set; }

    }
}