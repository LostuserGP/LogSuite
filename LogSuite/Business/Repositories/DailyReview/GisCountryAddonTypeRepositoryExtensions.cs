using System.Globalization;
using System.Linq;
using LogSuite.DataAccess.DailyReview;

namespace LogSuite.Business.Repositories.DailyReview
{
    public static class GisCountryAddonTypeRepositoryExtensions
    {
        // public static IQueryable<GisCountryAddonType> Search(this IQueryable<GisCountryAddonType> source, string filter)
        // {
        //     if (string.IsNullOrWhiteSpace(filter))
        //         return source;
        //     var f = filter.Trim().ToLower();
        //     var result =  source.Where(s => s.RequestedValue.ToString(CultureInfo.CurrentCulture).Contains(f)
        //                 || s.AllocatedValue.ToString(CultureInfo.CurrentCulture).Contains(f)
        //                 || s.EstimatedValue.ToString(CultureInfo.CurrentCulture).Contains(f)
        //                 || s.FactValue.ToString(CultureInfo.CurrentCulture).Contains(f)
        //                 );
        //     return result;
        // }

        public static IQueryable<GisCountryAddonType> Sort(this IQueryable<GisCountryAddonType> source, string columnName, bool @ascending)
        {
            if (string.IsNullOrWhiteSpace(columnName))
            {
                source = source.OrderByDescending(s => s.StartDate);
            } 
            else
            {
                if (@ascending)
                {
                    source = columnName switch
                    {
                        "Id" => source.OrderBy(s => s.Id),
                        "StartDate" => source.OrderBy(s => s.StartDate),
                        _ => source.OrderBy(s => s.StartDate)
                    };
                }
                else
                {
                    source = columnName switch
                    {
                        "Id" => source.OrderByDescending(s => s.Id),
                        "ReportDate" => source.OrderByDescending(s => s.StartDate),
                        _ => source.OrderByDescending(s => s.StartDate)
                    };
                }
            }
            return source;
        }
    }
}
