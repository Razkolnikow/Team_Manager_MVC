using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Team_Manager.Data.Common.Models;

namespace Team_Manager.Data.Models
{
    public class Topic : IAuditInfo, IDeletableEntity
    {
        private ICollection<Comment> comments;

        public Topic()
        {
            this.comments = new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual Team Team { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
