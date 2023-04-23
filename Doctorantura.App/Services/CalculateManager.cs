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
        private readonly CalcTaskManager _calcTaskManager;

        public CalculateManager(AppDbContext appDbContext, CalcTaskManager calcTaskManager)
        {
            _appDbContext = appDbContext;
            _calcTaskManager = calcTaskManager;
        }

        public async Task<CalculateVM> GetAllByTaskIdAsync(int taskId)
        {
            CalculateVM calculateVM = new CalculateVM();

            calculateVM.ColumnLines = await _appDbContext.ColumnsLines.Where(x => x.CalcTaskId == taskId)
                .Include(x => x.Column)
                .Include(x => x.Line)
                .ToListAsync();

            calculateVM.LineSums = await _appDbContext.LinesSum.Where(x => x.CalcTaskId == taskId).ToListAsync();

            calculateVM.WLines = await _appDbContext.WLines.Where(x => x.CalcTaskId == taskId).ToListAsync();

            calculateVM.AlfLines = await _appDbContext.AlfLines.Where(x => x.CalcTaskId == taskId).ToListAsync();

            calculateVM.QamLines = await _appDbContext.QamLines.Where(x => x.CalcTaskId == taskId).ToListAsync();

            calculateVM.XLines = await _appDbContext.XLines.Where(x => x.CalcTaskId == taskId).ToListAsync();

            return calculateVM;
        }

        public async Task UpdateColumnName(int columnNum, string columnName, int taskId)
        {
            Column column = await _appDbContext.Columns.Where(x => x.Row == columnNum && x.CalcTaskId == taskId).FirstOrDefaultAsync();
            Line line = await _appDbContext.Lines.Where(x => x.Row == columnNum && x.CalcTaskId == taskId).FirstOrDefaultAsync();

            column.Name = columnName;
            line.Name = columnName;

            await _appDbContext.SaveChangesAsync();
        }

        public async Task CreateAsync(int columnNum, int taskId)
        {
            string name = $"K{columnNum}";

            await CreateColumnAsync(name, columnNum, taskId);

            await CreateLineAsync(name, columnNum, taskId);

            await AddLineAsync(columnNum, taskId);

            await UpdateLineSumAsync(columnNum, taskId);

            await UpdateWLineAsync(taskId);

            List<int> columnRows = await _appDbContext.Columns.Where(x => x.CalcTaskId == taskId).Select(x => x.Row).ToListAsync();

            foreach (var line in columnRows)
            {
                await UpdateQamLineAsync(line, taskId);
            }

            await UpdateXLineAsync(taskId);
        }

        public async Task AddLineAsync(int columnNum, int taskId)
        {
            var columnFromDb = await _appDbContext.Columns.Where(x => x.Row == columnNum && x.CalcTaskId == taskId).FirstOrDefaultAsync();
            var lineFromDb = await _appDbContext.Lines.Where(x => x.Row == columnNum && x.CalcTaskId == taskId).FirstOrDefaultAsync();

            List<Column> columnListFromDb = await _appDbContext.Columns.Where(x => x.Row <= columnNum && x.CalcTaskId == taskId).ToListAsync();
            List<Line> lineListFromDb = await _appDbContext.Lines.Where(x => x.Row <= columnNum && x.CalcTaskId == taskId).ToListAsync();


            List<ColumnLine> columnDataFromDb = await _appDbContext.ColumnsLines.Where(x => x.ColumnNum == columnNum && x.CalcTaskId == taskId).ToListAsync();
            List<ColumnLine> lineDataFromDb = await _appDbContext.ColumnsLines.Where(x => x.LineNum == columnNum && x.CalcTaskId == taskId).ToListAsync();

            for (int i = 1; i <= columnNum; i++)
            {
                ColumnLine columnData = columnDataFromDb.Where(x => x.LineNum == i && x.CalcTaskId == taskId).FirstOrDefault();
                ColumnLine lineData = columnDataFromDb.Where(x => x.ColumnNum == i && x.CalcTaskId == taskId).FirstOrDefault();


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
                            Value = 0,
                            CalcTaskId = taskId,
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
                            Value = 0,
                            CalcTaskId = taskId,
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
                        Value = 1,
                        CalcTaskId = taskId
                    };

                    await _appDbContext.ColumnsLines.AddAsync(columnLine);
                    await _appDbContext.SaveChangesAsync();
                }
            }
        }

        private async Task CreateColumnAsync(string name, int columnNum, int taskId)
        {
            Column column = await _appDbContext.Columns.FirstOrDefaultAsync(x => x.Row == columnNum && x.CalcTaskId == taskId);

            if (column == null || column == default)
            {
                column = new Column
                {
                    Name = name,
                    Row = columnNum,
                    CalcTaskId = taskId
                };

                await _appDbContext.Columns.AddAsync(column);
            }

            else
            {
                column.Name = name;
                column.Row = columnNum;
                column.CalcTaskId = taskId;
            }

            await _appDbContext.SaveChangesAsync();
        }

        private async Task CreateLineAsync(string name, int lineNum, int taskId)
        {
            Line line = await _appDbContext.Lines.FirstOrDefaultAsync(x => x.Row == lineNum && x.CalcTaskId == taskId);

            if (line == null || line == default)
            {
                line = new Line
                {
                    Name = name,
                    Row = lineNum,
                    CalcTaskId = taskId
                };

                await _appDbContext.Lines.AddAsync(line);
            }

            else
            {
                line.Name = name;
                line.Row = lineNum;
                line.CalcTaskId = taskId;
            }

            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(int columnNo, int lineNo, double val, int taskId)
        {
            Column column = await _appDbContext.Columns.Where(x => x.Row == columnNo && x.CalcTaskId == taskId).FirstOrDefaultAsync();
            Line line = await _appDbContext.Lines.Where(x => x.Row == lineNo && x.CalcTaskId == taskId).FirstOrDefaultAsync();

            List<int> columnRows = await _appDbContext.Columns.Where(x => x.CalcTaskId == taskId).Select(x => x.Row).ToListAsync();

            await UpdateColumnDataAsync(column, columnNo, line.ID, lineNo, val, taskId);
            await UpdateLineDataAsync(line, columnNo, column.ID, lineNo, val, taskId);

            await UpdateLineSumAsync(lineNo, taskId);

            await UpdateWLineAsync(taskId);

            foreach (var columnRow in columnRows)
            {
                await UpdateQamLineAsync(columnRow, taskId);
            }

            await UpdateXLineAsync(taskId);
        }

        private async Task UpdateColumnDataAsync(Column column, int columnNo, int lineId, int lineNo, double val, int taskId)
        {
            ColumnLine columnDataFromDb = await _appDbContext.ColumnsLines
                .Where(x => x.ColumnNum == columnNo && x.LineNum == lineNo && x.CalcTaskId == taskId)
                .FirstOrDefaultAsync();

            if (columnDataFromDb == null || columnDataFromDb == default)
            {
                columnDataFromDb = new ColumnLine
                {
                    ColumnId = column.ID,
                    LineId = lineId,
                    ColumnNum = columnNo,
                    LineNum = lineNo,
                    Value = val,
                    CalcTaskId = taskId
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

        private async Task UpdateLineDataAsync(Line line, int columnNo, int columnId, int lineNo, double val, int taskId)
        {
            ColumnLine lineDataFromDb = await _appDbContext.ColumnsLines
               .Where(x => x.LineNum == columnNo && x.ColumnNum == lineNo && x.CalcTaskId == taskId)
               .FirstOrDefaultAsync();

            if (lineDataFromDb == null || lineDataFromDb == default)
            {
                lineDataFromDb = new ColumnLine
                {
                    ColumnId = columnId,
                    LineId = line.ID,
                    ColumnNum = columnNo,
                    LineNum = lineNo,
                    Value = 1 / val,
                    CalcTaskId = taskId
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

        private async Task UpdateLineSumAsync(int lineNo, int taskId)
        {
            double sum = await _appDbContext.ColumnsLines.Where(x => x.LineNum == lineNo && x.CalcTaskId == taskId).SumAsync(x => x.Value);

            LineSum lineSum = await _appDbContext.LinesSum.FirstOrDefaultAsync(x => x.LineNum == lineNo && x.CalcTaskId == taskId);

            if (lineSum == null || lineSum == default)
            {
                lineSum = new LineSum
                {
                    LineNum = lineNo,
                    TotalSum = sum,
                    CalcTaskId = taskId
                };
                await _appDbContext.LinesSum.AddAsync(lineSum);
            }

            else
            {
                lineSum.LineNum = lineNo;
                lineSum.TotalSum = sum;
                lineSum.CalcTaskId = taskId;
            }

            await _appDbContext.SaveChangesAsync();
        }

        private async Task UpdateWLineAsync(int taskId)
        {
            var linesSum = await _appDbContext.LinesSum.Where(x => x.CalcTaskId == taskId).ToListAsync();
            var total = linesSum.Sum(x => x.TotalSum);


            foreach (var lineSum in linesSum)
            {
                WLine wLine = await _appDbContext.WLines.FirstOrDefaultAsync(x => x.LineNum == lineSum.LineNum && x.CalcTaskId == taskId);

                if (wLine == null || wLine == default)
                {
                    wLine = new WLine
                    {
                        LineNum = lineSum.LineNum,
                        Value = lineSum.TotalSum / total,
                        CalcTaskId = taskId
                    };

                    await _appDbContext.WLines.AddAsync(wLine);
                }

                else
                {
                    wLine.LineNum = lineSum.LineNum;
                    wLine.Value = lineSum.TotalSum / total;
                    wLine.CalcTaskId = taskId;
                }
                await _appDbContext.SaveChangesAsync();

                await UpdateAlfLineAsync(wLine, taskId);
            }
        }

        private async Task UpdateXLineAsync(int taskId)
        {
            var lines = await _appDbContext.Lines.Where(x => x.CalcTaskId == taskId).Select(x => x.Row).ToListAsync();

            foreach (var lineNo in lines)
            {
                var qamLine = await _appDbContext.QamLines.Where(x => x.LineNum == lineNo && x.CalcTaskId == taskId).FirstOrDefaultAsync();
                var wLine = await _appDbContext.WLines.Where(x => x.LineNum == lineNo && x.CalcTaskId == taskId).FirstOrDefaultAsync();

                XLine xLine = await _appDbContext.XLines.FirstOrDefaultAsync(x => x.LineNum == lineNo && x.CalcTaskId == taskId);

                if (xLine == null || xLine == default)
                {
                    xLine = new XLine
                    {
                        LineNum = lineNo,
                        Value = qamLine.Value / wLine.Value,
                        CalcTaskId = taskId
                    };

                    await _appDbContext.XLines.AddAsync(xLine);
                }

                else
                {
                    xLine.LineNum = lineNo;
                    xLine.Value = qamLine.Value / wLine.Value;
                    xLine.CalcTaskId = taskId;
                }
                await _appDbContext.SaveChangesAsync();
            }
        }

        private async Task UpdateAlfLineAsync(WLine wLine, int taskId)
        {
            AlfLine alfLine = await _appDbContext.AlfLines.FirstOrDefaultAsync(x => x.LineNum == wLine.LineNum && x.CalcTaskId == taskId);

            if (alfLine == null || alfLine == default)
            {
                alfLine = new AlfLine
                {
                    LineNum = wLine.LineNum,
                    Value = wLine.Value * 8,
                    CalcTaskId = taskId
                };

                await _appDbContext.AlfLines.AddAsync(alfLine);
            }

            else
            {
                alfLine.LineNum = wLine.LineNum;
                alfLine.Value = wLine.Value * 8;
                alfLine.CalcTaskId = taskId;
            }

            await _appDbContext.SaveChangesAsync();
        }

        private async Task UpdateQamLineAsync(int lineNum, int taskId)
        {
            var lineValues = await _appDbContext.ColumnsLines.Where(x => x.LineNum == lineNum && x.CalcTaskId == taskId).ToListAsync();

            var wLines = await _appDbContext.WLines.Where(x => x.CalcTaskId == taskId).ToListAsync();

            double total = 0;

            foreach (var lineValue in lineValues)
            {
                total += lineValue.Value * wLines.FirstOrDefault(x => x.LineNum == lineValue.ColumnNum && x.CalcTaskId == taskId).Value;
            }

            var qamLine = await _appDbContext.QamLines.FirstOrDefaultAsync(x => x.LineNum == lineNum && x.CalcTaskId == taskId);

            if (qamLine == null || qamLine == default)
            {
                qamLine = new QamLine
                {
                    LineNum = lineNum,
                    Value = total,
                    CalcTaskId = taskId
                };

                await _appDbContext.QamLines.AddAsync(qamLine);
            }

            else
            {
                qamLine.LineNum = lineNum;
                qamLine.Value = total;
                qamLine.CalcTaskId = taskId;
            }

            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteByColumnNoAsync(int columnNo, int taskId)
        {
            await DeleteColumnLinesByColumnNo(columnNo, taskId);
            await DeleteColumnByRow(columnNo, taskId);
            await DeleteLineByRow(columnNo, taskId);

            await DeleteLineSumByLineNo(columnNo, taskId);

            await DeleteAlfLineByLineNo(columnNo, taskId);
            await DeleteWLineByLineNo(columnNo, taskId);

            await DeleteQamLineByLineNo(columnNo, taskId);

            await DeleteXLineByLineNo(columnNo, taskId);
        }

        private async Task DeleteColumnByRow(int row, int taskId)
        {
            var columns = await _appDbContext.Columns.Where(x => x.Row >= row && x.CalcTaskId == taskId).OrderByDescending(x => x.Row).ToListAsync();
            _appDbContext.Columns.Remove(columns.Where(x => x.Row == row && x.CalcTaskId == taskId).FirstOrDefault());

            for (int i = 0; i < columns.Where(x => x.Row >= row).Count() - 1; i++)
            {
                columns[i].Row = columns[i + 1].Row;
                columns[i].Name = columns[i + 1].Name;
            }

            await _appDbContext.SaveChangesAsync();
        }

        private async Task DeleteLineByRow(int row, int taskId)
        {
            var lines = await _appDbContext.Lines.Where(x => x.Row >= row && x.CalcTaskId == taskId).OrderByDescending(x => x.Row).ToListAsync();
            _appDbContext.Lines.Remove(lines.Where(x => x.Row == row && x.CalcTaskId == taskId).FirstOrDefault());

            for (int i = 0; i < lines.Where(x => x.Row >= row).Count() - 1; i++)
            {
                lines[i].Row = lines[i + 1].Row;
                lines[i].Name = lines[i + 1].Name;
            }

            await _appDbContext.SaveChangesAsync();
        }

        private async Task DeleteColumnLinesByColumnNo(int columnNo, int taskId)
        {
            var columnLinesForDelete = await _appDbContext.ColumnsLines.Where(x => x.ColumnNum == columnNo || x.LineNum == columnNo && x.CalcTaskId == taskId).ToListAsync();

            _appDbContext.ColumnsLines.RemoveRange(columnLinesForDelete);
            await _appDbContext.SaveChangesAsync();

            var columnLines = await _appDbContext.ColumnsLines.Where(x => x.ColumnNum >= columnNo || x.LineNum >= columnNo && x.CalcTaskId == taskId).OrderBy(x => x.ColumnNum).OrderBy(x => x.LineNum).ToListAsync();

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

        private async Task DeleteLineSumByLineNo(int lineNo, int taskId)
        {
            var linesSum = await _appDbContext.LinesSum.Where(x => x.LineNum >= lineNo && x.CalcTaskId == taskId).OrderByDescending(x => x.LineNum).ToListAsync();

            _appDbContext.LinesSum.Remove(linesSum.Where(x => x.LineNum == lineNo && x.CalcTaskId == taskId).FirstOrDefault());

            for (int i = 0; i < linesSum.Where(x => x.LineNum >= lineNo).Count() - 1; i++)
            {
                linesSum[i].LineNum = linesSum[i + 1].LineNum;
            }

            await _appDbContext.SaveChangesAsync();


            var lines = await _appDbContext.LinesSum.Select(x => x.LineNum).ToListAsync();

            foreach (var line in lines)
            {
                await UpdateLineSumAsync(line, taskId);
            }
        }

        private async Task DeleteWLineByLineNo(int lineNo, int taskId)
        {
            var wLines = await _appDbContext.WLines.Where(x => x.LineNum >= lineNo && x.CalcTaskId == taskId).OrderByDescending(x => x.LineNum).ToListAsync();

            _appDbContext.WLines.Remove(wLines.Where(x => x.LineNum == lineNo && x.CalcTaskId == taskId).FirstOrDefault());

            for (int i = 0; i < wLines.Where(x => x.LineNum >= lineNo).Count() - 1; i++)
            {
                wLines[i].LineNum = wLines[i + 1].LineNum;
            }

            await _appDbContext.SaveChangesAsync();

            await UpdateWLineAsync(taskId);
        }

        private async Task DeleteAlfLineByLineNo(int lineNo, int taskId)
        {
            var alfLines = await _appDbContext.AlfLines.Where(x => x.LineNum >= lineNo && x.CalcTaskId == taskId).OrderByDescending(x => x.LineNum).ToListAsync();

            _appDbContext.AlfLines.Remove(alfLines.Where(x => x.LineNum == lineNo && x.CalcTaskId == taskId).FirstOrDefault());

            for (int i = 0; i < alfLines.Where(x => x.LineNum >= lineNo).Count() - 1; i++)
            {
                alfLines[i].LineNum = alfLines[i + 1].LineNum;
            }

            await _appDbContext.SaveChangesAsync();
        }

        private async Task DeleteQamLineByLineNo(int lineNo, int taskId)
        {
            var qamLines = await _appDbContext.QamLines.Where(x => x.LineNum >= lineNo && x.CalcTaskId == taskId).OrderByDescending(x => x.LineNum).ToListAsync();

            _appDbContext.QamLines.Remove(qamLines.Where(x => x.LineNum == lineNo && x.CalcTaskId == taskId).FirstOrDefault());

            for (int i = 0; i < qamLines.Where(x => x.LineNum >= lineNo).Count() - 1; i++)
            {
                qamLines[i].LineNum = qamLines[i + 1].LineNum;
            }

            await _appDbContext.SaveChangesAsync();

            List<int> columnRows = await _appDbContext.Columns.Select(x => x.Row).ToListAsync();

            foreach (var line in columnRows)
            {
                await UpdateQamLineAsync(line, taskId);
            }


        }

        private async Task DeleteXLineByLineNo(int lineNo, int taskId)
        {
            var xLines = await _appDbContext.XLines.Where(x => x.LineNum >= lineNo && x.CalcTaskId == taskId).OrderByDescending(x => x.LineNum).ToListAsync();

            _appDbContext.XLines.Remove(xLines.Where(x => x.LineNum == lineNo && x.CalcTaskId == taskId).FirstOrDefault());

            for (int i = 0; i < xLines.Where(x => x.LineNum >= lineNo).Count() - 1; i++)
            {
                xLines[i].LineNum = xLines[i + 1].LineNum;
            }

            await _appDbContext.SaveChangesAsync();

            await UpdateXLineAsync(taskId);
        }

        public async Task DeleteAllByTaskId(int taskId)
        {
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

            await _appDbContext.SaveChangesAsync();
        }
    }
}
