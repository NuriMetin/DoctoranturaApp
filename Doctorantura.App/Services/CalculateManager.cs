using Doctorantura.App.Context;
using Doctorantura.App.Models;
using Doctorantura.App.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

            calculateVM.AlfLines = await _appDbContext.AlfLines.ToListAsync();

            calculateVM.QamLines = await _appDbContext.QamLines.ToListAsync();

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
            string name = $"K{columnNum}";

            await CreateColumnAsync(name, columnNum);

            await CreateLineAsync(name, columnNum);

            await AddLineAsync(columnNum);

            await UpdateLineSumAsync(columnNum);

            await UpdateWLineAsync();

            await UpdateQamLineAsync(columnNum);
        }

        public async Task AddLineAsync(int columnNum)
        {
            var columnFromDb = await _appDbContext.Columns.Where(x => x.Row == columnNum).FirstOrDefaultAsync();
            var lineFromDb = await _appDbContext.Lines.Where(x => x.Row == columnNum).FirstOrDefaultAsync();

            List<Column> columnListFromDb = await _appDbContext.Columns.Where(x => x.Row <= columnNum).ToListAsync();
            List<Line> lineListFromDb = await _appDbContext.Lines.Where(x => x.Row <= columnNum).ToListAsync();


            List<ColumnLine> columnDataFromDb = await _appDbContext.ColumnsLines.Where(x => x.ColumnNum == columnNum).ToListAsync();
            List<ColumnLine> lineDataFromDb = await _appDbContext.ColumnsLines.Where(x => x.LineNum == columnNum).ToListAsync();

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

        private async Task CreateColumnAsync(string name, int columnNum)
        {
            Column column = await _appDbContext.Columns.FirstOrDefaultAsync(x => x.Row == columnNum);

            if (column == null || column == default)
            {
                column = new Column
                {
                    Name = name,
                    Row = columnNum
                };

                await _appDbContext.Columns.AddAsync(column);
            }

            else
            {
                column.Name = name;
                column.Row = columnNum;
            }

            await _appDbContext.SaveChangesAsync();
        }

        private async Task CreateLineAsync(string name, int lineNum)
        {
            Line line = await _appDbContext.Lines.FirstOrDefaultAsync(x => x.Row == lineNum);

            if (line == null || line == default)
            {
                line = new Line
                {
                    Name = name,
                    Row = lineNum
                };

                await _appDbContext.Lines.AddAsync(line);
            }

            else
            {
                line.Name = name;
                line.Row = lineNum;
            }

            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(int columnNo, int lineNo, double val)
        {
            Column column = await _appDbContext.Columns.Where(x => x.Row == columnNo).FirstOrDefaultAsync();
            Line line = await _appDbContext.Lines.Where(x => x.Row == lineNo).FirstOrDefaultAsync();

            await UpdateColumnDataAsync(column, columnNo, line.ID, lineNo, val);
            await UpdateLineDataAsync(line, columnNo, column.ID, lineNo, val);


            await UpdateLineSumAsync(lineNo);

            await UpdateWLineAsync();
            await UpdateQamLineAsync(lineNo);
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

            if (lineSum == null || lineSum == default)
            {
                lineSum = new LineSum
                {
                    LineNum = lineNo,
                    TotalSum = sum
                };
                await _appDbContext.AddAsync(lineSum);
            }

            else
            {
                lineSum.LineNum = lineNo;
                lineSum.TotalSum = sum;
            }

            await _appDbContext.SaveChangesAsync();
        }

        private async Task UpdateWLineAsync()
        {
            var linesSum = await _appDbContext.LinesSum.ToListAsync();
            var total = linesSum.Sum(x => x.TotalSum);


            foreach (var lineSum in linesSum)
            {
                WLine wLine = await _appDbContext.WLines.FirstOrDefaultAsync(x => x.LineNum == lineSum.LineNum);

                if (wLine == null || wLine == default)
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

                await UpdateAlfLineAsync(wLine);
            }
        }

        private async Task UpdateAlfLineAsync(WLine wLine)
        {
            AlfLine alfLine = await _appDbContext.AlfLines.FirstOrDefaultAsync(x => x.LineNum == wLine.LineNum);

            if (alfLine == null || alfLine == default)
            {
                alfLine = new AlfLine
                {
                    LineNum = wLine.LineNum,
                    Value = wLine.Value * 8
                };

                await _appDbContext.AlfLines.AddAsync(alfLine);
            }

            else
            {
                alfLine.LineNum = wLine.LineNum;
                alfLine.Value = wLine.Value * 8;
            }

            await _appDbContext.SaveChangesAsync();
        }

        private async Task UpdateQamLineAsync(int lineNum)
        {
            var lineValues = await _appDbContext.ColumnsLines.Where(x => x.LineNum == lineNum).ToListAsync();

            var wLines = await _appDbContext.WLines.ToListAsync();

            double total = 0;

            foreach (var lineValue in lineValues)
            {
                total += lineValue.Value * wLines.FirstOrDefault(x => x.LineNum == lineValue.ColumnNum).Value;
            }

            var qamLine = await _appDbContext.QamLines.FirstOrDefaultAsync(x => x.LineNum == lineNum);

            if (qamLine == null || qamLine == default)
            {
                qamLine = new QamLine
                {
                    LineNum = lineNum,
                    Value = total
                };

                await _appDbContext.QamLines.AddAsync(qamLine);
            }

            else
            {
                qamLine.LineNum = lineNum;
                qamLine.Value = total;
            }

            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteByColumnNoAsync(int columnNo)
        {
            //await DeleteColumnLinesByColumnNo(columnNo);
            await DeleteColumnByRow(columnNo);
            //await DeleteLineByRow(columnNo);

            //await DeleteLineSumByLineNo(columnNo);

            //await DeleteWLineByLineNo(columnNo);

            //await UpdateWLineAsync();

            //await DeleteQamLineByLineNo(columnNo);

        }

        private async Task DeleteColumnByRow(int row)
        {
            var columnForDelete = await _appDbContext.Columns.Where(x => x.Row == row).FirstOrDefaultAsync();
            _appDbContext.Columns.Remove(columnForDelete);

            var columns = await _appDbContext.Columns.Where(x => x.Row >= row).OrderByDescending(x => x.Row).ToListAsync();
            int ff = columns.Where(x => x.Row > row).Count();


            for (int i =1 ; i == ff; i++)
            {
                columns[i].Name = columns[i - 1].Name;
                columns[i].Row = columns[i - 1].Row;
            }


            await _appDbContext.SaveChangesAsync();
        }

        private async Task DeleteLineByRow(int row)
        {
            var lines = await _appDbContext.Lines.Where(x => x.Row >= row).ToListAsync();

            for (int i = 1; i <= lines.Where(x => x.Row > row).Count(); i++)
            {
                lines[i].Name = lines[i - 1].Name;
                lines[i].Row = lines[i - 1].Row;
            }

            _appDbContext.Lines.RemoveRange(lines.Where(x => x.Row == row).FirstOrDefault());

            await _appDbContext.SaveChangesAsync();
        }

        private async Task DeleteColumnLinesByColumnNo(int columnNo)
        {
            var columnLinesForDelete = await _appDbContext.ColumnsLines.Where(x => x.ColumnNum == columnNo || x.LineNum == columnNo).ToListAsync();

            _appDbContext.ColumnsLines.RemoveRange(columnLinesForDelete);
            await _appDbContext.SaveChangesAsync();

            var columnLines = await _appDbContext.ColumnsLines.Where(x => x.ColumnNum >= columnNo || x.LineNum >= columnNo).OrderBy(x => x.ColumnNum).OrderBy(x => x.LineNum).ToListAsync();

            foreach (var grouppedColumnLine in columnLines.GroupBy(x => x.ColumnNum))
            {
                foreach (var columnLine in grouppedColumnLine.Where(x => x.ColumnNum > columnNo))
                {
                    columnLine.ColumnNum = columnLine.ColumnNum - 1;
                }
            }

            foreach (var grouppedColumnLine in columnLines.GroupBy(x => x.LineNum))
            {
                foreach (var columnLine in grouppedColumnLine.Where(x => x.LineNum > columnNo))
                {
                    columnLine.LineNum = columnLine.LineNum - 1;
                }
            }

            await _appDbContext.SaveChangesAsync();
        }

        private async Task DeleteLineSumByLineNo(int lineNo)
        {
            var linesSum = await _appDbContext.LinesSum.Where(x => x.LineNum >= lineNo).ToListAsync();

            for (int i = 1; i <= linesSum.Where(x => x.LineNum > lineNo).Count(); i++)
            {
                linesSum[i].LineNum = linesSum[i - 1].LineNum;
            }

            _appDbContext.LinesSum.RemoveRange(linesSum.Where(x => x.LineNum == lineNo).FirstOrDefault());
            await _appDbContext.SaveChangesAsync();


            var lines = await _appDbContext.LinesSum.Select(x => x.LineNum).ToListAsync();

            foreach (var line in lines)
            {
                await UpdateLineSumAsync(line);
            }
        }

        private async Task DeleteWLineByLineNo(int lineNo)
        {
            var wLines = await _appDbContext.WLines.Where(x => x.LineNum >= lineNo).ToListAsync();

            for (int i = 1; i <= wLines.Where(x => x.LineNum > lineNo).Count(); i++)
            {
                wLines[i].LineNum = wLines[i - 1].LineNum;
            }

            _appDbContext.WLines.RemoveRange(wLines.Where(x => x.LineNum == lineNo).FirstOrDefault());
            await _appDbContext.SaveChangesAsync();
        }

        private async Task DeleteQamLineByLineNo(int lineNo)
        {
            var qamLines = await _appDbContext.QamLines.Where(x => x.LineNum >= lineNo).ToListAsync();
            var lines = await _appDbContext.QamLines.Select(x => x.LineNum).ToListAsync();

            for (int i = 1; i <= qamLines.Where(x => x.LineNum > lineNo).Count(); i++)
            {
                qamLines[i].LineNum = qamLines[i - 1].LineNum;
            }

            _appDbContext.QamLines.RemoveRange(qamLines.Where(x => x.LineNum == lineNo).FirstOrDefault());
            await _appDbContext.SaveChangesAsync();

            foreach (var line in lines)
            {
                await UpdateQamLineAsync(line);
            }
        }
    }
}
