using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunters.Models
{
    using System.ComponentModel.DataAnnotations;

    public class JobApplication
    {
        [Required]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public string Comment { get; set; }

        [Required]
        public string CvPath { get; set; }

        [Required]
        public int JobPostId { get; set; }

        public virtual JobPost JobPost { get; set; }
    }
}
