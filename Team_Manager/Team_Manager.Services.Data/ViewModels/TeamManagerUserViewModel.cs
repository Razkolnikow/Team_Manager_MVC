using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Team_Manager.Services.Data.ViewModels
{
    public class TeamManagerUserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string[] TeamNames { get; set; }
    }
}
