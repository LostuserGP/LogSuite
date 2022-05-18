using System.Globalization;
using System.Linq;
using LogSuite.DataAccess.DailyReview;

namespace LogSuite.Business.Repositories.DailyReview
{
    public static class GisCountryResourceRepositoryExtensions
    {
        public static IQueryable<GisCountryResource> Search(this IQueryable<GisCountryResource> source, string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return source;
            var f = filter.Trim().ToLower();
            var result =  source.Where(s => s.Month.ToString().Contains(f)
                        || s.Value.ToString(CultureInfo.CurrentCulture).Contains(f)
                        );
            return result;
        }

        public static IQueryable<GisCountryResource> Sort(this IQueryable<GisCountryResource> source, string columnName, bool @ascending)
        {
            if (string.IsNullOrWhiteSpace(columnName))
            {
                source = source.OrderBy(s => s.Month);
            } 
            else
            {
                if (@ascending)
                {
                    source = columnName switch
                    {
                        "Id" => source.OrderBy(s => s.Id),
                        "Month" => source.OrderBy(s => s.Month),
                        "Value" => source.OrderBy(s => s.Value),
                        _ => source.OrderBy(s => s.Month)
                    };
                }
                else
                {
                    source = columnName switch
                    {
                        "Id" => source.OrderByDescending(s => s.Id),
                        "Month" => source.OrderByDescending(s => s.Month),
                        "Value" => source.OrderByDescending(s => s.Value),
                        _ => source.OrderByDescending(s => s.Month)
                    };
                }
            }
            return source;
        }
    }
}
