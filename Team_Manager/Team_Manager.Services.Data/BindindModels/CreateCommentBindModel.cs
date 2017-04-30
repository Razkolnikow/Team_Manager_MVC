using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Manager.Services.Data.BindindModels
{
    public class CreateCommentBindModel
    {
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public int TopicId { get; set; }
    }
}
