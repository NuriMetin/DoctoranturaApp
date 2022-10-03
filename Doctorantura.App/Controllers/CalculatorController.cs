using Doctorantura.App.Context;
using Doctorantura.App.Models;
using Doctorantura.App.Services;
using Doctorantura.App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Doctorantura.App.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly CalculateManager _calculateManager;

        public CalculatorController(CalculateManager calculateManager)
        {
            _calculateManager = calculateManager;
        }

        public async Task<IActionResult> Calculate()
        {

            var calculateVm = await _calculateManager.GetAllAsync();


            return View(calculateVm);
        }

        public async Task<IActionResult> Insert(int columnNo, int lineNo, double val, string columnName)
        {
            try
            {
                if(columnNo != 0 && lineNo!=0 && val!=0 && !String.IsNullOrEmpty(columnName))
                {
                    await _calculateManager.InsertAsync(columnNo, lineNo, val, columnName);
                    await _calculateManager.InsertAsync(lineNo, columnNo, 1 / val, columnName);
                }
            }

            catch (System.Exception exp)
            {
                return Json(new { response = exp.Message });
            }

            return Json(new { response = "success" });
        }
    }
}
