using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Team_Manager.Services.Data.BindindModels
{
    public class CreateCommentBindModel
    {
        [AllowHtml]
        [Required]
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public int TopicId { get; set; }
    }
}
