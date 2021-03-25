using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiskSuite.DataAccess
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
