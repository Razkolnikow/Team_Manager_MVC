using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Manager.Services.Data.BindindModels
{
    public class TopicBindModel
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public string AuthorId { get; set; }

        public int TeamId { get; set; }
    }
}
