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

        public async Task<IActionResult> UpdateColumnName(int columnNum,string columnName)
        {
            try
            {
                await _calculateManager.UpdateColumnName(columnNum, columnName);
            }

            catch (System.Exception exp)
            {
                return Json(new { response = exp.Message });
            }

            return Json(new { response = "Success" });
        }


        public async Task<IActionResult> InsertData(int columnNum)
        {
            try
            {
                await _calculateManager.CreateAsync(columnNum);
            }

            catch (System.Exception exp)
            {
                return Json(new { response = exp.Message });
            }

            //return RedirectToAction("Calculate", "Calculator");
            return Json(new { response = "Success" });
        }

        public async Task<IActionResult> UpdateData(int columnNo, int lineNo, double val)
        {
            try
            {
                if(columnNo != 0 && lineNo!=0 && val!=0)
                {
                    await _calculateManager.UpdateAsync(columnNo, lineNo, val);
                    await _calculateManager.UpdateAsync(lineNo, columnNo, 1 / val);
                }
            }

            catch (System.Exception exp)
            {
                return Json(new { response = exp.Message });
            }

            //return RedirectToAction("Calculate", "Calculator");
            return Json(new { response = "Success" });
        }

        public async Task<IActionResult> DeleteData(int columnNo)
        {
            try
            {
                if (columnNo != 0)
                {
                    await _calculateManager.DeleteByColumnNoAsync(columnNo);
                   
                }
            }

            catch (System.Exception exp)
            {
                return Json(new { response = exp.Message });
            }

            //return RedirectToAction("Calculate", "Calculator");
            return Json(new { response = "Success" });
        }
    }
}
