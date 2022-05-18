using System.Linq;
using LogSuite.DataAccess.CredRisk;

namespace LogSuite.Business.Repositories.References;

public static class RiskClassRepositoryExtensions
{
    public static IQueryable<RiskClass> Search(this IQueryable<RiskClass> source,
        string filter)
    {
        if (string.IsNullOrWhiteSpace(filter))
            return source;
        var f = filter.Trim().ToLower();
        var result = source.Where(s => s.Name.ToLower().Contains(f));
        return result;
    }

    public static IQueryable<RiskClass> Sort(this IQueryable<RiskClass> source,
        string columnName, bool @ascending)
    {
        if (string.IsNullOrWhiteSpace(columnName))
        {
            source = source.OrderBy(s => s.Name);
        }
        else
        {
            if (@ascending)
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