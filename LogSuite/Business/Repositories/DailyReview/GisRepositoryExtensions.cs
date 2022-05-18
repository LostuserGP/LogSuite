using LogSuite.DataAccess.DailyReview;
using LogSuite.DataAccess.References;
using System.Linq;

namespace LogSuite.Business.Repositories
{
    public static class GisRepositoryExtensions
    {
        public static IQueryable<Gis> Search(this IQueryable<Gis> source, string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return source;
            var f = filter.Trim().ToLower();
            var result =  source.Where(s => s.Name.ToLower().Contains(f)
                        || s.Names.Any(n => n.ToLower().Contains(f))
                        );
            return result;
        }

        public static IQueryable<Gis> Sort(this IQueryable<Gis> source, string columnName, bool @ascending)
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
}
