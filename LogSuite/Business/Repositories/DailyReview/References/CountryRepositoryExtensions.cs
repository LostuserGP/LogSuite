using System.ComponentModel;
using System.Linq;
using LogSuite.DataAccess.References;

namespace LogSuite.Business.Repositories.DailyReview.References
{
    public static class CountryRepositoryExtensions
    {
        public static IQueryable<Country> Search(this IQueryable<Country> source, string filter)
        {
            if (string.IsNullOrWhiteSpace(filter)) return source;
            var f = filter.Trim().ToLower();
            var result = source.Where(s => s.Name.ToLower().Contains(f)
                       || s.ShortName.ToLower().Contains(f));
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
                PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(Country)).Find(columnName, true);
                if (@ascending)
                {
                    source = source.OrderBy(x => prop.GetValue(x));
                }
                else
                {
                    source = source.OrderByDescending(x => prop.GetValue(x));
                }
            }
            return source;
        }

        public static IQueryable<Country> Sort(this IQueryable<Country> source, string sorter)
        {
            if (string.IsNullOrWhiteSpace(sorter))
            {
                source = source.OrderBy(s => s.Name);
            }
            else
            {
                var columnName = sorter.Split(' ')[0];
                var sortAsc = sorter.Split(' ')[1] == "asc" ? true : false;
                PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(Country)).Find(columnName, true);
                if (sortAsc)
                {
                    source = source.OrderBy(x => prop.GetValue(x));
                }
                else
                {
                    source = source.OrderByDescending(x => prop.GetValue(x));
                }
                
            }
            return source;
        }
    }
}
