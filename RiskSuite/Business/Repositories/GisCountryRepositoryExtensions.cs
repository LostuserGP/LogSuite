using LogSuite.DataAccess.Operativka;
using LogSuite.DataAccess.References;
using System.Linq;

namespace LogSuite.Business.Repositories
{
    public static class GisCountryRepositoryExtensions
    {
        public static IQueryable<GisCountry> Search(this IQueryable<GisCountry> source, string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return source;
            var f = filter.Trim().ToLower();
            var result =  source.Where(s => s.Country.Name.ToLower().Contains(f)
                        || s.Country.DailyReviewName.ToLower().Contains(f)
                        || s.Country.Names.Any(n => n.Name.ToLower().Contains(f))
                        );
            return result;
        }

        public static IQueryable<GisCountry> Sort(this IQueryable<GisCountry> source, string columnName, bool ascendind)
        {
            if (string.IsNullOrWhiteSpace(columnName))
            {
                source = source.OrderBy(s => s.Country.Name);
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
                            source = source.OrderBy(s => s.Country.Name);
                            break;
                        case "DailyReviewName":
                            source = source.OrderBy(s => s.Country.DailyReviewName);
                            break;
                        case "Gis":
                            source = source.OrderBy(s => s.Gis.Name);
                            break;
                        case "IsCalculated":
                            source = source.OrderBy(s => s.IsCalculated);
                            break;
                        case "IsExcluded":
                            source = source.OrderBy(s => s.IsHidden);
                            break;
                        case "Multiplicator":
                            source = source.OrderBy(s => s.Multiplicator);
                            break;
                        default:
                            source = source.OrderBy(s => s.Country.Name);
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
                            source = source.OrderByDescending(s => s.Country.Name);
                            break;
                        case "DailyReviewName":
                            source = source.OrderByDescending(s => s.Country.DailyReviewName);
                            break;
                        case "Gis":
                            source = source.OrderByDescending(s => s.Gis.Name);
                            break;
                        case "IsCalculated":
                            source = source.OrderByDescending(s => s.IsCalculated);
                            break;
                        case "IsExcluded":
                            source = source.OrderByDescending(s => s.IsHidden);
                            break;
                        case "Multiplicator":
                            source = source.OrderByDescending(s => s.Multiplicator);
                            break;
                        default:
                            source = source.OrderByDescending(s => s.Country.Name);
                            break;
                    }
                }
            }
            return source;
        }
    }
}
