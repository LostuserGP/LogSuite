using Microsoft.EntityFrameworkCore;
using LogSuite.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Business
{
    public class PagedList<T> : List<T>
    {
        public MetaData MetaData { get; set; }
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            AddRange(items);
        }

        public PagedList(IEnumerable<T> items, MetaData metaData)
        {
            MetaData = metaData;
            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var enumerable = source.ToList();
            var count = enumerable.Count();
            var items = enumerable
              .Skip((pageNumber - 1) * pageSize)
              .Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

        public static async Task<PagedList<T>> ToPagedListAsync(IQueryable<T> source, int skip, int top)
        {
            var count = await source.CountAsync();
            var items = await source.Skip(skip).Take(top).ToListAsync();
            var pageNumber = skip / top;
            return new PagedList<T>(items, count, pageNumber, top);
        }
    }
}
