using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Team_Manager.Data.Common.Models;
using Team_Manager.Data.Models.Enums;

namespace Team_Manager.Data.Models
{
    public class Team : IDeletableEntity, IAuditInfo
    {
        private ICollection<ApplicationUser> teamMembers;
        private ICollection<Topic> topics;
        private ICollection<TeamTask> teamTasks;

        public Team()
        {
            this.teamMembers = new HashSet<ApplicationUser>();
            this.topics = new HashSet<Topic>();
            this.teamTasks = new HashSet<TeamTask>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public TeamCategory Category { get; set; }

        public string Subcategory { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public virtual ICollection<ApplicationUser> TeamMembers
        {
            get { return this.teamMembers; }
            set { this.teamMembers = value; }
        }

        public virtual ICollection<Topic> Topics
        {
            get { return this.topics; }
            set { this.topics = value; }
        }

        public virtual ICollection<TeamTask> TeamTasks
        {
            get { return this.teamTasks; }
            set { this.teamTasks = value; }
        }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
