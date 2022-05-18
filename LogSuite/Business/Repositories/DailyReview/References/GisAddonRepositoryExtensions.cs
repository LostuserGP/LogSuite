using System.Linq;
using LogSuite.DataAccess.DailyReview;

namespace LogSuite.Business.Repositories.DailyReview.References
{
    public static class GisAddonRepositoryExtensions
    {
        public static IQueryable<GisAddon> Search(this IQueryable<GisAddon> source, string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return source;
            var f = filter.Trim().ToLower();
            var result =  source.Where(s => s.Name.ToLower().Contains(f)
                        || s.Names.ToString().ToLower().Contains(f)
                        );
            return result;
        }

        public static IQueryable<GisAddon> Sort(this IQueryable<GisAddon> source, string columnName, bool @ascending)
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
                        "DailyReviewName" => source.OrderBy(s => s.DailyReviewName),
                        "IsHidden" => source.OrderBy(s => s.IsHidden),
                        "IsInput" => source.OrderBy(s => s.IsInput),
                        "IsOutput" => source.OrderBy(s => s.IsOutput),
                        _ => source.OrderBy(s => s.Name)
                    };
                }
                else
                {
                    source = columnName switch
                    {
                        "Id" => source.OrderByDescending(s => s.Id),
                        "Name" => source.OrderByDescending(s => s.Name),
                        "DailyReviewName" => source.OrderByDescending(s => s.DailyReviewName),
                        "IsHidden" => source.OrderByDescending(s => s.IsHidden),
                        "IsInput" => source.OrderByDescending(s => s.IsInput),
                        "IsOutput" => source.OrderByDescending(s => s.IsOutput),
                        _ => source.OrderByDescending(s => s.Name)
                    };
                }
            }
            return source;
        }
    }
}
