﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class SalesRecordsService
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordsService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? MinDate, DateTime? MaxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (MinDate.HasValue)
                result = result.Where(x => x.Date >= MinDate.Value);
            if (MinDate.HasValue)
                result = result.Where(x => x.Date <= MaxDate.Value);

            return await result.Include(x => x.Seller).Include(x => x.Seller.Department).OrderByDescending(x => x.Date).ToListAsync();
        }

        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? MinDate, DateTime? MaxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (MinDate.HasValue)
                result = result.Where(x => x.Date >= MinDate.Value);
            if (MinDate.HasValue)
                result = result.Where(x => x.Date <= MaxDate.Value);

            return await result.Include(x => x.Seller).Include(x => x.Seller.Department).OrderByDescending(x => x.Date).GroupBy(x => x.Seller.Department).ToListAsync();
        }
    }
}
