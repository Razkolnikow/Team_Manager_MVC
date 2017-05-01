using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Manager.Data.Common.Models;

namespace Team_Manager.Data.Models
{
    public class TeamTask : IAuditInfo, IDeletableEntity
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime FinalDate { get; set; }

        public virtual ApplicationUser TeamMember { get; set; }

        //Added
        public bool IsAccepted { get; set; }

        //Added
        public bool IsRejected { get; set; }

        public string RejectionReason { get; set; }

        public virtual Team Team { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
