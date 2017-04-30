using System;
using System.ComponentModel.DataAnnotations;
using Team_Manager.Data.Common.Models;

namespace Team_Manager.Data.Models
{
    public class Comment : IAuditInfo, IDeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual Topic Topic { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
