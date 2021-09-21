using LogSuite.DataAccess.DailyReview;
using System.Linq;

namespace LogSuite.Business.Repositories
{
    public static class GisOutputNameRepositoryExtensions
    {
        public static IQueryable<GisOutputName> Search(this IQueryable<GisOutputName> source, string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return source;
            var f = filter.Trim().ToLower();
            var result =  source.Where(s => s.Name.ToLower().Contains(f)
                        );
            return result;
        }

        public static IQueryable<GisOutputName> Sort(this IQueryable<GisOutputName> source, string columnName, bool ascendind)
        {
            if (string.IsNullOrWhiteSpace(columnName))
            {
                source = source.OrderBy(s => s.Name);
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
                            source = source.OrderBy(s => s.Name);
                            break;
                        default:
                            source = source.OrderBy(s => s.Name);
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
                            source = source.OrderByDescending(s => s.Name);
                            break;
                        default:
                            source = source.OrderByDescending(s => s.Name);
                            break;
                    }
                }
            }
            return source;
        }
    }
}
