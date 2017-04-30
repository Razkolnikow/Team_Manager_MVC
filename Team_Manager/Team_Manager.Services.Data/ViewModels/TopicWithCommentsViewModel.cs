using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Manager.Services.Data.ViewModels
{
    public class TopicWithCommentsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}
