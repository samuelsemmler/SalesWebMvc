using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordsService _salesRecordsService;

        public SalesRecordsController(SalesRecordsService salesRecordsService)
        {
            _salesRecordsService = salesRecordsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? MinDate, DateTime? MaxDate)
        {
            if (!MinDate.HasValue)
                MinDate = new DateTime(DateTime.Now.Year, 1, 1);

            if (!MaxDate.HasValue)
                MaxDate = DateTime.Now;

            ViewData["MinDate"] = MinDate.Value.ToString("yyyy-MM-dd");
            ViewData["MaxDate"] = MaxDate.Value.ToString("yyyy-MM-dd");
            var result = await _salesRecordsService.FindByDateAsync(MinDate, MaxDate);

            return View(result);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? MinDate, DateTime? MaxDate)
        {
            if (!MinDate.HasValue)
                MinDate = new DateTime(DateTime.Now.Year, 1, 1);

            if (!MaxDate.HasValue)
                MaxDate = DateTime.Now;

            ViewData["MinDate"] = MinDate.Value.ToString("yyyy-MM-dd");
            ViewData["MaxDate"] = MaxDate.Value.ToString("yyyy-MM-dd");
            var result = await _salesRecordsService.FindByDateGroupingAsync(MinDate, MaxDate);

            return View(result);
        }
    }
}
