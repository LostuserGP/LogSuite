using LogSuite.DataAccess.References;
using Microsoft.AspNetCore.Identity;

namespace LogSuite.DataAccess
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string CustomClaim { get; set; }
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
