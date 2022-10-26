using Doctorantura.App.Context;
using Doctorantura.App.Models;
using Doctorantura.App.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doctorantura.App.Services
{
    public class CalculateManager
    {
        private readonly AppDbContext _appDbContext;

        public CalculateManager(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<CalculateVM> GetAllAsync()
        {
            CalculateVM calculateVM = new CalculateVM();

            calculateVM.ColumnLines = await _appDbContext.ColumnsLines
                .Include(x => x.Column)
                .Include(x => x.Line)
                .ToListAsync();

            calculateVM.LineSums = await _appDbContext.LinesSum.ToListAsync();

            calculateVM.WLines = await _appDbContext.WLines.ToListAsync();

            return calculateVM;
        }

        public async Task UpdateColumnName(int columnNum, string columnName)
        {
            Column column = await _appDbContext.Columns.Where(x => x.Row == columnNum).FirstOrDefaultAsync();
            Line line = await _appDbContext.Lines.Where(x => x.Row == columnNum).FirstOrDefaultAsync();

            column.Name = columnName;
            line.Name = columnName;

            await _appDbContext.SaveChangesAsync();
        }

        public async Task CreateAsync(int columnNum)
        {
            var columnFromDb = await _appDbContext.Columns.Where(x => x.Row == columnNum).FirstOrDefaultAsync();
            var lineFromDb = await _appDbContext.Lines.Where(x => x.Row == columnNum).FirstOrDefaultAsync();

            List<Column> columnListFromDb = await _appDbContext.Columns.Where(x => x.Row <= columnNum).ToListAsync();
            List<Line> lineListFromDb = await _appDbContext.Lines.Where(x => x.Row <= columnNum).ToListAsync();


            List<ColumnLine> columnDataFromDb = await _appDbContext.ColumnsLines.Where(x => x.ColumnNum == columnNum).ToListAsync();
            List<ColumnLine> lineDataFromDb = await _appDbContext.ColumnsLines.Where(x => x.LineNum == columnNum).ToListAsync();


            if (columnFromDb == null || columnFromDb == default)
            {
                columnFromDb = new Column
                {
                    Name = "Z",
                    Row = columnNum
                };

                await _appDbContext.AddAsync(columnFromDb);
                await _appDbContext.SaveChangesAsync();
            }


            if (lineFromDb == null || lineFromDb == default)
            {
                lineFromDb = new Line
                {
                    Name = "Z",
                    Row = columnNum
                };

                await _appDbContext.Lines.AddAsync(lineFromDb);
                await _appDbContext.SaveChangesAsync();
            }

            for (int i = 1; i <= columnNum; i++)
            {
                ColumnLine columnData = columnDataFromDb.Where(x => x.LineNum == i).FirstOrDefault();
                ColumnLine lineData = columnDataFromDb.Where(x => x.ColumnNum == i).FirstOrDefault();


                if (i != columnNum)
                {

                    if (columnData == null || columnData == default)
                    {
                        columnData = new ColumnLine
                        {
                            ColumnId = columnFromDb.ID,
                            ColumnNum = columnNum,
                            LineId = lineListFromDb.Where(x => x.Row == i).FirstOrDefault().ID,
                            LineNum = i,
                            Value = 0
                        };

                        await _appDbContext.ColumnsLines.AddAsync(columnData);
                        await _appDbContext.SaveChangesAsync();
                    }

                    if (lineData == null || lineData == default)
                    {
                        lineData = new ColumnLine
                        {
                            ColumnId = columnListFromDb.Where(x => x.Row == i).FirstOrDefault().ID,
                            ColumnNum = i,
                            LineId = lineFromDb.ID,
                            LineNum = columnNum,
                            Value = 0
                        };

                        await _appDbContext.ColumnsLines.AddAsync(lineData);
                        await _appDbContext.SaveChangesAsync();
                    }
                }

                else
                {
                    ColumnLine columnLine = new ColumnLine
                    {
                        ColumnId = columnFromDb.ID,
                        ColumnNum = columnNum,
                        LineId = lineFromDb.ID,
                        LineNum = columnNum,
                        Value = 1
                    };

                    await _appDbContext.ColumnsLines.AddAsync(columnLine);
                    await _appDbContext.SaveChangesAsync();
                }
            }
        }

        public async Task UpdateAsync(int columnNo, int lineNo, double val)

        {
            Column column = await _appDbContext.Columns.Where(x => x.Row == columnNo).FirstOrDefaultAsync();
            Line line = await _appDbContext.Lines.Where(x => x.Row == lineNo).FirstOrDefaultAsync();

            await UpdateColumnDataAsync(column, columnNo, line.ID, lineNo, val);
            await UpdateLineDataAsync(line, columnNo, column.ID, lineNo, val);

            await UpdateLineSumAsync(lineNo);

            await UpdateWLineAsync();

        }

        private async Task UpdateColumnDataAsync(Column column, int columnNo, int lineId, int lineNo, double val)
        {

            ColumnLine columnDataFromDb = await _appDbContext.ColumnsLines
                .Where(x => x.ColumnNum == columnNo && x.LineNum == lineNo)
                .FirstOrDefaultAsync();



            if (columnDataFromDb == null || columnDataFromDb == default)
            {
                columnDataFromDb = new ColumnLine
                {
                    ColumnId = column.ID,
                    LineId = lineId,
                    ColumnNum = columnNo,
                    LineNum = lineNo,
                    Value = val
                };

                await _appDbContext.ColumnsLines.AddAsync(columnDataFromDb);
                await _appDbContext.SaveChangesAsync();
            }

            else
            {
                columnDataFromDb.Value = val;
                await _appDbContext.SaveChangesAsync();
            }
        }

        private async Task UpdateLineDataAsync(Line line, int columnNo, int columnId, int lineNo, double val)
        {

            ColumnLine lineDataFromDb = await _appDbContext.ColumnsLines
               .Where(x => x.LineNum == columnNo && x.ColumnNum == lineNo)
               .FirstOrDefaultAsync();


            if (lineDataFromDb == null || lineDataFromDb == default)
            {
                lineDataFromDb = new ColumnLine
                {
                    ColumnId = columnId,
                    LineId = line.ID,
                    ColumnNum = columnNo,
                    LineNum = lineNo,
                    Value = 1 / val
                };

                await _appDbContext.ColumnsLines.AddAsync(lineDataFromDb);
                await _appDbContext.SaveChangesAsync();
            }

            else
            {
                lineDataFromDb.Value = 1 / val;
                await _appDbContext.SaveChangesAsync();
            }
        }

        private async Task UpdateLineSumAsync(int lineNo)
        {
            double sum = await _appDbContext.ColumnsLines.Where(x => x.LineNum == lineNo).SumAsync(x => x.Value);

            LineSum lineSum = await _appDbContext.LinesSum.FirstOrDefaultAsync(x => x.LineNum == lineNo);

            if(lineSum==null || lineSum == default)
            {
                lineSum = new LineSum
                {
                    LineNum = lineNo,
                    TotalSum = (decimal)sum
                };
                await _appDbContext.AddAsync(lineSum);
            }

            else
            {
                lineSum.LineNum = lineNo;
                lineSum.TotalSum = (decimal)sum;
            }

            await _appDbContext.SaveChangesAsync();
        }

        private async Task UpdateWLineAsync()
        {
            var linesSum = await _appDbContext.LinesSum.ToListAsync();
            var total = linesSum.Sum(x => x.TotalSum);


            foreach (var lineSum in linesSum)
            {
                WLine wLine= await _appDbContext.WLines.FirstOrDefaultAsync(x=>x.LineNum==lineSum.LineNum);

                if(wLine==null || wLine == default)
                {
                    wLine = new WLine
                    {
                        LineNum = lineSum.LineNum,
                        Value = lineSum.TotalSum / total
                    };

                    await _appDbContext.AddAsync(wLine);
                }

                else
                {
                    wLine.LineNum = lineSum.LineNum;
                    wLine.Value = lineSum.TotalSum / total;
                }
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
