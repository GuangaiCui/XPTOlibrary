using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPTOlibrary.Models.ViewModels
{
    public class UserRolesVM
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public DateTime RegisterTime { get; set; }
        public string Status { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }

}
