using Doctorantura.App.Context;
using Doctorantura.App.Models;
using Doctorantura.App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Doctorantura.App.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public CalculatorController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Calculate()
        {


            var data = await _appDbContext.ColumnNums
                        .Include(x => x.LineNums)
                        .ToListAsync();

            var grouppedColumnIds = _appDbContext.LineNums
                       .GroupBy(i => i.ColumnNumId)
                       .Select(g=>g.Key)
                       .ToList();

            CalculateVM calculateVM = new CalculateVM
            {
                ColumnNums = data,
                LineNums = await _appDbContext.LineNums.ToListAsync(),
                GrouppedColumnIds= grouppedColumnIds
            };

            return View(calculateVM);
        }
    }
}
