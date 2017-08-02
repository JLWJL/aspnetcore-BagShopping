using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualityBags.Models
{
    public class PageList<T>
    {
        private bool hasPre;
        private bool hasNext;
        public int CurPage { get; private set; }
        public int TotalPages { get; private set; }
        public List<T> PagedBagList { get; private set; }

        public PageList(List<T> items, int count, int pageIndex, int pageSize)
        {
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            CurPage = pageIndex;
            hasPre = HasPreviousPage;
            hasNext = HasNextPage;
            PagedBagList=items;
        }

        public bool HasPreviousPage
        {
            get
            {
                return (CurPage > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (CurPage < TotalPages);
            }
        }

        public static async Task<PageList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PageList<T>(items, count, pageIndex, pageSize);
        }
    }
}
