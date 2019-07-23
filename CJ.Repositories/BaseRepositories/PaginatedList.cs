using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CJ.Repositories.BaseRepositories
{
    public class Pagination
    {
        public int Total { get; set; }

        public int PageSize { get; set; }
        public int Current { get; set; }
        public Pagination(int total, int pageSize, int current)
        {
            Total = total;
            PageSize = pageSize;
            Current = current;
        }
    }
    public class PaginatedList<TEntity> : List<TEntity>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public Pagination Pagination { get; set; }
        public PaginatedList(List<TEntity> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            PageSize = pageSize;
            Pagination = new Pagination(count, pageSize, pageIndex);
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
            IQueryable<TEntity> source, int? pageIndex, int? pageSize)
        {
            try
            {
                pageIndex = pageIndex ?? 1;
                pageSize = pageSize ?? 10;
                pageIndex = pageIndex == 0 ? 1 : pageIndex;
                pageSize = pageSize == 0 ? 10 : pageSize;

                var count = await source.CountAsync();
                var list = await source.Skip(
                        (pageIndex??1 - 1) * pageSize??10)
                    .Take(pageSize??10).ToListAsync();
                return new PaginatedList<TEntity>(list, count, pageIndex??1, pageSize??10);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        public static PaginatedList<TEntity> CreatePage(
            IQueryable<TEntity> source, int? pageIndex, int? pageSize)
        {
            try
            {
                pageIndex = pageIndex ?? 1;
                pageSize = pageSize ?? 10;
                pageIndex = pageIndex == 0 ? 1 : pageIndex;
                pageSize = pageSize == 0 ? 10 : pageSize;

                var count = source.Count();
                var list = source.Skip(
                        ((pageIndex??1) - 1) * pageSize??10)
                    .Take(pageSize??10).ToList();
                return new PaginatedList<TEntity>(list, count, pageIndex??1, pageSize??10);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
