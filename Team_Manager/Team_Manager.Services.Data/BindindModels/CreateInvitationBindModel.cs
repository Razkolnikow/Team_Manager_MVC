using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Manager.Services.Data.BindindModels
{
    public class CreateInvitationBindModel
    {
        [Required]
        public string TargetedUserId { get; set; }

        [Required]
        public int TeamId { get; set; }

        public string SenderId { get; set; }
    }
}
