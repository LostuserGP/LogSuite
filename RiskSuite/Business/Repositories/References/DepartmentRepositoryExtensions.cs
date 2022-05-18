using LogSuite.DataAccess.References;
using System.Linq;

namespace LogSuite.Business.Repositories
{
    public static class DepartmentRepositoryExtensions
    {
        public static IQueryable<Department> Search(this IQueryable<Department> source, string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return source;
            var f = filter.Trim().ToLower();
            var result = source.Where(s => s.Name.ToLower().Contains(f)
                       || s.ShortName.ToLower().Contains(f)
                        );
            return result;
        }

        public static IQueryable<Department> Sort(this IQueryable<Department> source, string columnName, bool ascending)
        {
            if (string.IsNullOrWhiteSpace(columnName))
            {
                source = source.OrderBy(s => s.Name);
            }
            else
            {
                if (ascending)
                {
                    //source = source.OrderBy(a => a.GetType().GetProperty(columnName).GetValue(a, null));
                    switch (columnName)
                    {
                        case "Id":
                            source = source.OrderBy(s => s.Id);
                            break;
                        case "Name":
                            source = source.OrderBy(s => s.Name);
                            break;
                        case "ShortName":
                            source = source.OrderBy(s => s.ShortName);
                            break;
                        case "Code":
                            source = source.OrderBy(s => s.Code);
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
                        case "ShortName":
                            source = source.OrderByDescending(s => s.ShortName);
                            break;
                        case "Code":
                            source = source.OrderByDescending(s => s.Code);
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
