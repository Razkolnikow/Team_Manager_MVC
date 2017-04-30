using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Team_Manager.Data.Common.Models;

namespace Team_Manager.Data.Models
{
    public class ApplicationUser : IdentityUser, IDeletableEntity, IAuditInfo
    {
        private ICollection<Team> createdTeams;
        private ICollection<Team> memberTeams;
        private ICollection<Topic> topics;
        private ICollection<Invitation> invitations;
        private ICollection<TeamTask> teamTasks;

        public ApplicationUser()
        {
            this.CreatedOn = DateTime.Now;
            this.createdTeams = new HashSet<Team>();
            this.memberTeams = new HashSet<Team>();
            this.topics = new HashSet<Topic>();
            this.invitations = new HashSet<Invitation>();
            this.teamTasks = new HashSet<TeamTask>();
        }

        [InverseProperty("Creator")]
        public virtual ICollection<Team> CreatedTeams
        {
            get { return this.createdTeams; }
            set { this.createdTeams = value; }
        }

        public virtual ICollection<Team> MemberTeams
        {
            get { return this.memberTeams; }
            set { this.memberTeams = value; }
        }

        public virtual ICollection<Topic> Topics
        {
            get { return this.topics; }
            set { this.topics = value; }
        }

        public virtual ICollection<Invitation> Invitations
        {
            get { return this.invitations; }
            set { this.invitations = value; }
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

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
