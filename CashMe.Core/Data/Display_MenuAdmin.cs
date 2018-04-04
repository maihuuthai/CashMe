using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMe.Core.Data
{
    public class Display_MenuAdmin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RoleId { get; set; }
        //public string RoleName { get; set; }
        public string LinkedController { get; set; }
        public bool IsOfTheWebsite { get; set; }
        public bool IsOfTheAdmin { get; set; }
        public bool IsActive { get; set; }
    }
}
