using LogSuite.DataAccess.References;
using System.Linq;
using LogSuite.DataAccess.CredRisk;
using LogSuite.DataAccess.DailyReview;

namespace LogSuite.Business.Repositories.References
{
    public static class FinancialStatementStandardRepositoryExtensions
    {
        public static IQueryable<FinancialStatementStandard> Search(this IQueryable<FinancialStatementStandard> source, string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return source;
            var f = filter.Trim().ToLower();
            var result =  source.Where(s => s.Name.ToLower().Contains(f));
            return result;
        }

        public static IQueryable<FinancialStatementStandard> Sort(this IQueryable<FinancialStatementStandard> source, string columnName, bool ascendind)
        {
            if (string.IsNullOrWhiteSpace(columnName))
            {
                source = source.OrderBy(s => s.Name);
            } 
            else
            {
                if (ascendind)
                {
                    source = columnName switch
                    {
                        "Id" => source.OrderBy(s => s.Id),
                        "Name" => source.OrderBy(s => s.Name),
                        _ => source.OrderBy(s => s.Name)
                    };
                }
                else
                {
                    source = columnName switch
                    {
                        "Id" => source.OrderByDescending(s => s.Id),
                        "Name" => source.OrderByDescending(s => s.Name),
                        _ => source.OrderByDescending(s => s.Name)
                    };
                }
            }
            return source;
        }
    }
}
