using System.Linq;
using LogSuite.DataAccess.DailyReview;

namespace LogSuite.Business.Repositories.DailyReview.References
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
                        || s.Country.Names.ToList().Any(n => n.ToLower().Contains(f))
                        );
            return result;
        }

        public static IQueryable<GisCountry> Sort(this IQueryable<GisCountry> source, string columnName, bool ascending)
        {
            if (string.IsNullOrWhiteSpace(columnName))
            {
                source = source.OrderBy(s => s.Country.Name);
            } 
            else
            {
                if (ascending)
                {
                    source = columnName switch
                    {
                        "Id" => source.OrderBy(s => s.Id),
                        "Name" => source.OrderBy(s => s.Country.Name),
                        "DailyReviewName" => source.OrderBy(s => s.Country.DailyReviewName),
                        "Gis" => source.OrderBy(s => s.Gis.Name),
                        "IsNotCalculated" => source.OrderBy(s => s.IsNotCalculated),
                        "IsHidden" => source.OrderBy(s => s.IsHidden),
                        _ => source.OrderBy(s => s.Country.Name)
                    };
                }
                else
                {
                    source = columnName switch
                    {
                        "Id" => source.OrderByDescending(s => s.Id),
                        "Name" => source.OrderByDescending(s => s.Country.Name),
                        "DailyReviewName" => source.OrderByDescending(s => s.Country.DailyReviewName),
                        "Gis" => source.OrderByDescending(s => s.Gis.Name),
                        "IsNotCalculated" => source.OrderByDescending(s => s.IsNotCalculated),
                        "IsHidden" => source.OrderByDescending(s => s.IsHidden),
                        _ => source.OrderByDescending(s => s.Country.Name)
                    };
                }
            }
            return source;
        }
    }
}
