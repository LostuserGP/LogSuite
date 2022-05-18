using LogSuite.DataAccess.DailyReview;
using LogSuite.DataAccess.References;
using System.Linq;

namespace LogSuite.Business.Repositories
{
    public static class GisAddonRepositoryExtensions
    {
        public static IQueryable<GisAddon> Search(this IQueryable<GisAddon> source, string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return source;
            var f = filter.Trim().ToLower();
            var result =  source.Where(s => s.Name.ToLower().Contains(f)
                        || s.Names.Any(n => n.Name.ToLower().Contains(f))
                        );
            return result;
        }

        public static IQueryable<GisAddon> Sort(this IQueryable<GisAddon> source, string columnName, bool ascendind)
        {
            if (string.IsNullOrWhiteSpace(columnName))
            {
                source = source.OrderBy(s => s.Name);
            } 
            else
            {
                if (ascendind)
                {
                    switch (columnName)
                    {
                        case "Id":
                            source = source.OrderBy(s => s.Id);
                            break;
                        case "Name":
                            source = source.OrderBy(s => s.Name);
                            break;
                        case "IsCalculated":
                            source = source.OrderBy(s => s.IsCalculated);
                            break;
                        case "Multiplicator":
                            source = source.OrderBy(s => s.Multiplicator);
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
                        case "IsCalculated":
                            source = source.OrderByDescending(s => s.IsCalculated);
                            break;
                        case "Multiplicator":
                            source = source.OrderByDescending(s => s.Multiplicator);
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
