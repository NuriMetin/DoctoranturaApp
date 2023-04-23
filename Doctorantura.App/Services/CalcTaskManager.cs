using Doctorantura.App.Context;
using Doctorantura.App.Models;
using Doctorantura.App.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doctorantura.App.Services
{
    public class CalcTaskManager
    {
        private readonly AppDbContext _appDbContext;

        public CalcTaskManager(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<CalcTask> CreateTaskAsync(string name, string appUserId)
        {
            CalcTask existTask = await GetTaskByNameAsync(name);

            if (existTask == null || existTask == default)
            {
                await _appDbContext.CalcTasks.AddAsync(new CalcTask { Name = name , AppUserId = appUserId, CreatedDate = DateTime.Now});

                await _appDbContext.SaveChangesAsync();

                return await GetTaskByNameAsync(name);
            }

            else
                return existTask;
        }

        public async Task<List<CalcTaskVM>> GetAllAsync()
        {
            List<CalcTaskVM> taskList = new List<CalcTaskVM>();

            List<CalcTask> calcTasks = await _appDbContext.CalcTasks.Include(x => x.AppUser).ToListAsync();

            foreach (var calcTask in calcTasks)
            {
                taskList.Add(new CalcTaskVM
                {
                    ID = calcTask.Id,
                    Name = calcTask.Name,
                    Username = $@"{calcTask.AppUser.Name}  {calcTask.AppUser.Surname}",
                    CreatedDate = calcTask.CreatedDate
                });
            }

            return taskList;
        }

        public async Task<CalcTask> GetTaskByNameAsync(string name)
        {
            return await _appDbContext.CalcTasks.Where(x => x.Name == name).FirstOrDefaultAsync();
        }

        public async Task DeleteTaskByIdAsync(int taskId, string userId)
        {
            CalcTask calcTask = await _appDbContext.CalcTasks.Where(x=>x.Id== taskId).FirstOrDefaultAsync();

            if (userId == calcTask.AppUserId)
            {
                // await _calculateManager.DeleteAllByTaskId(id);
                List<XLine> xLines = await _appDbContext.XLines.Where(x => x.CalcTaskId == taskId).ToListAsync();
                List<QamLine> qamLines = await _appDbContext.QamLines.Where(x => x.CalcTaskId == taskId).ToListAsync();
                List<AlfLine> alfLines = await _appDbContext.AlfLines.Where(x => x.CalcTaskId == taskId).ToListAsync();
                List<WLine> wLines = await _appDbContext.WLines.Where(x => x.CalcTaskId == taskId).ToListAsync();
                List<LineSum> linesSum = await _appDbContext.LinesSum.Where(x => x.CalcTaskId == taskId).ToListAsync();
                List<ColumnLine> columnLines = await _appDbContext.ColumnsLines.Where(x => x.CalcTaskId == taskId).ToListAsync();
                List<Column> columns = await _appDbContext.Columns.Where(x => x.CalcTaskId == taskId).ToListAsync();
                List<Line> lines = await _appDbContext.Lines.Where(x => x.CalcTaskId == taskId).ToListAsync();

                _appDbContext.XLines.RemoveRange(xLines);
                _appDbContext.QamLines.RemoveRange(qamLines);
                _appDbContext.AlfLines.RemoveRange(alfLines);
                _appDbContext.WLines.RemoveRange(wLines);
                _appDbContext.LinesSum.RemoveRange(linesSum);
                _appDbContext.ColumnsLines.RemoveRange(columnLines);
                _appDbContext.Columns.RemoveRange(columns);
                _appDbContext.Lines.RemoveRange(lines);

                _appDbContext.CalcTasks.Remove(calcTask);

                await _appDbContext.SaveChangesAsync();

            }

        }
    }
}
