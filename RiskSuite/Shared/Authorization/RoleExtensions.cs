using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSuite.Shared.Authorization
{
    public static class RoleExtensions
    {
        public static AuthorizationPolicyBuilder RoleExist(this AuthorizationPolicyBuilder policy, string role)
        {
            return null;
        }
    }
}
