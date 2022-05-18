using System.Globalization;
using System.Linq;
using LogSuite.DataAccess.DailyReview;

namespace LogSuite.Business.Repositories.DailyReview
{
    public static class GisInputValueRepositoryExtensions
    {
        public static IQueryable<GisInputValue> Search(this IQueryable<GisInputValue> source, string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return source;
            var f = filter.Trim().ToLower();
            var result =  source.Where(s => s.RequestedValue.ToString(CultureInfo.CurrentCulture).Contains(f)
                        || s.AllocatedValue.ToString(CultureInfo.CurrentCulture).Contains(f)
                        || s.EstimatedValue.ToString(CultureInfo.CurrentCulture).Contains(f)
                        || s.FactValue.ToString(CultureInfo.CurrentCulture).Contains(f)
                        );
            return result;
        }

        public static IQueryable<GisInputValue> Sort(this IQueryable<GisInputValue> source, string columnName, bool @ascending)
        {
            if (string.IsNullOrWhiteSpace(columnName))
            {
                source = source.OrderByDescending(s => s.ReportDate);
            } 
            else
            {
                if (@ascending)
                {
                    source = columnName switch
                    {
                        "Id" => source.OrderBy(s => s.Id),
                        "ReportDate" => source.OrderBy(s => s.ReportDate),
                        "RequestedValue" => source.OrderBy(s => s.RequestedValue),
                        "AllocatedValue" => source.OrderBy(s => s.AllocatedValue),
                        "EstimatedValue" => source.OrderBy(s => s.EstimatedValue),
                        "FactValue" => source.OrderBy(s => s.FactValue),
                        _ => source.OrderBy(s => s.ReportDate)
                    };
                }
                else
                {
                    source = columnName switch
                    {
                        "Id" => source.OrderByDescending(s => s.Id),
                        "ReportDate" => source.OrderByDescending(s => s.ReportDate),
                        "RequestedValue" => source.OrderByDescending(s => s.RequestedValue),
                        "AllocatedValue" => source.OrderByDescending(s => s.AllocatedValue),
                        "EstimatedValue" => source.OrderByDescending(s => s.EstimatedValue),
                        "FactValue" => source.OrderByDescending(s => s.FactValue),
                        _ => source.OrderByDescending(s => s.ReportDate)
                    };
                }
            }
            return source;
        }
    }
}
