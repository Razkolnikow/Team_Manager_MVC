using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Team_Manager.Services.Data.BindindModels
{
    public class CreateTaskBindModel
    {
        [Required]
        [StringLength(200)]
        public string Content { get; set; }

        public int TeamId { get; set; }

        [Required]
        public DateTime FinalDate { get; set; }

        [Required]
        public string TeamMemberId { get; set; }
    }
}
