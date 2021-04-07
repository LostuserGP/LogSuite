using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskSuite.Client.Helper
{
    public static class RoleExtensions
    {

        public static AuthorizationPolicyBuilder RoleExist(this AuthorizationPolicyBuilder policy, string role)
        {
            return null;
        }
    }
}
