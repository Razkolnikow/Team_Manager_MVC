using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team_Manager.Data.Common.Models;

namespace Team_Manager.Data.Models
{
    public class Invitation : IAuditInfo, IDeletableEntity
    {
        public int Id { get; set; }

        public virtual Team Team { get; set; }

        public virtual ApplicationUser User { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
