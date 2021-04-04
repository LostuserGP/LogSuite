using RiskSuite.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskSuite.Server.Helpers
{
    public static class AccountExtensions
    {
        public static IQueryable<ApplicationUser> Search(this IQueryable<ApplicationUser> source, string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return source;
            var f = filter.Trim().ToLower();
            var result = source.Where(s => s.Name.ToLower().Contains(f)
                       || s.Email.ToLower().Contains(f)
                        );
            return result;
        }

        public static IQueryable<ApplicationUser> Sort(this IQueryable<ApplicationUser> source, string columnName, bool ascending)
        {
            if (string.IsNullOrWhiteSpace(columnName))
            {
                source = source.OrderBy(s => s.Name);
            }
            else
            {
                if (ascending)
                {
                    switch (columnName)
                    {
                        case "Id":
                            source = source.OrderBy(s => s.Id);
                            break;
                        case "Name":
                            source = source.OrderBy(s => s.Name);
                            break;
                        case "Email":
                            source = source.OrderBy(s => s.Email);
                            break;
                        case "Department":
                            source = source.OrderBy(s => s.Department.Name);
                            break;
                        default:
                            source = source.OrderBy(s => s.Name);
                            break;
                    }
                }
                else
                {
                    switch (columnName)
                    {
                        case "Id":
                            source = source.OrderByDescending(s => s.Id);
                            break;
                        case "Name":
                            source = source.OrderByDescending(s => s.Name);
                            break;
                        case "Email":
                            source = source.OrderByDescending(s => s.Email);
                            break;
                        case "Department":
                            source = source.OrderByDescending(s => s.Department.Name);
                            break;
                        default:
                            source = source.OrderByDescending(s => s.Name);
                            break;
                    }
                }
            }
            return source;
        }
    }
}
