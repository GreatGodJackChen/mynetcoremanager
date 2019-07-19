using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJ.Repositories.BaseRepositories
{
    public class PaginatedList<TEntity> : List<TEntity>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public PaginatedList(List<TEntity> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            PageSize = pageSize;

            this.AddRange(items);
        }
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }
        public static async Task<PaginatedList<TEntity>> CreatePageAsync(
            IQueryable<TEntity> source, int pageIndex, int pageSize)
        {
            try
            {
                var count = await source.CountAsync();
                var items = await source.Skip(
                        (pageIndex - 1) * pageSize)
                    .Take(pageSize).ToListAsync();
                return new PaginatedList<TEntity>(items, count, pageIndex, pageSize);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        public static PaginatedList<TEntity> CreatePage(
            IQueryable<TEntity> source, int pageIndex, int pageSize)
        {
            try
            {
                var count = source.Count();
                var aa = source.Skip(
                        (pageIndex - 1) * pageSize)
                    .Take(pageSize);
                var items = aa.ToList();
                return new PaginatedList<TEntity>(items, count, pageIndex, pageSize);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
