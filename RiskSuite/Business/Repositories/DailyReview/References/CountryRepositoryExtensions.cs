using LogSuite.DataAccess.References;
using System.Linq;

namespace LogSuite.Business.Repositories.References
{
    public static class CountryRepositoryExtensions
    {
        public static IQueryable<Country> Search(this IQueryable<Country> source, string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return source;
            var f = filter.Trim().ToLower();
            var result =  source.Where(s => s.Name.ToLower().Contains(f)
                        || s.ShortName.ToLower().Contains(f)
                        );
            return result;
        }

        public static IQueryable<Country> Sort(this IQueryable<Country> source, string columnName, bool ascending)
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
                        "ShortName" => source.OrderBy(s => s.ShortName),
                        "NameEn" => source.OrderBy(s => s.NameEn),
                        _ => source.OrderBy(s => s.Name)
                    };
                }
                else
                {
                    source = columnName switch
                    {
                        "Id" => source.OrderByDescending(s => s.Id),
                        "Name" => source.OrderByDescending(s => s.Name),
                        "ShortName" => source.OrderByDescending(s => s.ShortName),
                        "NameEn" => source.OrderByDescending(s => s.NameEn),
                        _ => source.OrderByDescending(s => s.Name)
                    };
                }
            }
            return source;
        }
    }
}
