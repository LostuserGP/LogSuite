﻿using LogSuite.DataAccess.References;
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
                        || s.Names.Any(n => n.Name.ToLower().Contains(f))
                        );
            return result;
        }

        public static IQueryable<Country> Sort(this IQueryable<Country> source, string columnName, bool ascendind)
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
                        case "ShortName":
                            source = source.OrderBy(s => s.ShortName);
                            break;
                        case "NameEn":
                            source = source.OrderBy(s => s.NameEn);
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
                        case "ShortName":
                            source = source.OrderByDescending(s => s.ShortName);
                            break;
                        case "NameEn":
                            source = source.OrderByDescending(s => s.NameEn);
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
