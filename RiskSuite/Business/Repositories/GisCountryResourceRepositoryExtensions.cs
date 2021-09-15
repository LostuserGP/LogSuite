using LogSuite.DataAccess.Operativka;
using LogSuite.DataAccess.References;
using System.Linq;

namespace LogSuite.Business.Repositories
{
    public static class GisCountryResourceRepositoryExtensions
    {
        public static IQueryable<GisCountryResource> Search(this IQueryable<GisCountryResource> source, string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return source;
            var f = filter.Trim().ToLower();
            var result =  source.Where(s => s.Month.ToString().Contains(f)
                        || s.Value.ToString().Contains(f)
                        );
            return result;
        }

        public static IQueryable<GisCountryResource> Sort(this IQueryable<GisCountryResource> source, string columnName, bool ascendind)
        {
            if (string.IsNullOrWhiteSpace(columnName))
            {
                source = source.OrderBy(s => s.Month);
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
                        case "MonthNumber":
                            source = source.OrderBy(s => s.Month);
                            break;
                        case "Value":
                            source = source.OrderBy(s => s.Value);
                            break;
                        default:
                            source = source.OrderBy(s => s.Month);
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
                        case "MonthNumber":
                            source = source.OrderByDescending(s => s.Month);
                            break;
                        case "Value":
                            source = source.OrderByDescending(s => s.Value);
                            break;
                        default:
                            source = source.OrderByDescending(s => s.Month);
                            break;
                    }
                }
            }
            return source;
        }
    }
}
