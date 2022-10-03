using Doctorantura.App.Context;
using Doctorantura.App.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<ColumnLine>> GetAllAsync()
        {
            var calculateVm = await _appDbContext.ColumnsLines
                .Include(x => x.Column)
                .Include(x => x.Line)
                .ToListAsync();

            return calculateVm;
        }

        public async Task Insert(int columnNo, int lineNo, double val, string columnName)
        {
            try
            {
                Column column = new Column
                {
                    Name = columnName,
                    Row = columnNo
                };

                await _appDbContext.Columns.AddAsync(column);
                await _appDbContext.SaveChangesAsync();

                Line line = new Line
                {
                    Name = columnName,
                    Row = lineNo
                };

                await _appDbContext.Lines.AddAsync(line);
                await _appDbContext.SaveChangesAsync();

                ColumnLine columnLineCreated = new ColumnLine
                {
                    ColumnId = column.ID,
                    LineId = line.ID,
                    Value = val
                };

                await _appDbContext.ColumnsLines.AddAsync(columnLineCreated);
                await _appDbContext.SaveChangesAsync();


                ColumnLine columnLineCalculated = new ColumnLine
                {
                    LineId = column.ID,
                    ColumnId = line.ID,
                    Value = 1 / val
                };

                await _appDbContext.ColumnsLines.AddAsync(columnLineCalculated);
                await _appDbContext.SaveChangesAsync();
            }

            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public async Task InsertAsync(int columnNo, int lineNo, double val, string columnName)
        
        {
            Column column = await _appDbContext.Columns.Where(x => x.Row == columnNo).FirstOrDefaultAsync();
            Line line = await _appDbContext.Lines.Where(x => x.Row == lineNo).FirstOrDefaultAsync();
            ColumnLine columnLine = await _appDbContext.ColumnsLines
                .Include(x => x.Column)
                .Include(x => x.Line)
                .Where(x => x.Column.Row == columnNo && x.Line.Row == lineNo)
                .FirstOrDefaultAsync();

            if (column == null)
            {
                column = new Column
                {
                    Name = columnName,
                    Row = columnNo
                };

                await _appDbContext.Columns.AddAsync(column);
                await _appDbContext.SaveChangesAsync();
            }


            if (line == null)
            {
                line = new Line
                {
                    Name = columnName,
                    Row = lineNo
                };

                await _appDbContext.Lines.AddAsync(line);
                await _appDbContext.SaveChangesAsync();
            }

            if (columnLine == null)
            {
                columnLine = new ColumnLine
                {
                    ColumnId = column.ID,
                    LineId = line.ID
                };

                await _appDbContext.ColumnsLines.AddAsync(columnLine);
                await _appDbContext.SaveChangesAsync();
            }

            else
            {
                columnLine.Value = val;
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
