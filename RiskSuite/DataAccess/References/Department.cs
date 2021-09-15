using System;
using System.Collections.Generic;
using System.Text;

namespace LogSuite.DataAccess.References
{
    public class Department
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public ICollection<ApplicationUser> ApplicationUsers { get; set; } = new List<ApplicationUser>();
    }
}
