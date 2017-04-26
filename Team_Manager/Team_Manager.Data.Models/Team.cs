using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Manager.Data.Models.Enums;

namespace Team_Manager.Data.Models
{
    public class Team
    {
        private ICollection<ApplicationUser> teamMembers;

        public Team()
        {
            this.teamMembers = new HashSet<ApplicationUser>();
        }

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
    }
}
