using AutoMapper;
using Doctorantura.App.Context;
using Doctorantura.App.Dtos.ColumnNumDtos;
using Doctorantura.App.Interfaces.Repositories;
using Doctorantura.App.Models;
using System.Threading.Tasks;

namespace Doctorantura.App.Repository
{
    public class ColumnRepository: IColumnNumRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public ColumnRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task AddColumnNumAsync(ColumnNumCreateDto columnNumDto)
        {
            Column columnNum = _mapper.Map<Column>(columnNumDto);
            await _appDbContext.Columns.AddAsync(columnNum);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateColumnNumAsync(ColumnNumUpdateDto columnNumDto)
        {
            Column columnNum = await _appDbContext.Columns.FindAsync(columnNumDto.ID);
            columnNum = _mapper.Map<Column>(columnNumDto);
        
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteColumnNumAsync(int id)
        {
            Column columnNum = await _appDbContext.Columns.FindAsync(id);
            _appDbContext.Columns.Remove(columnNum);

            await _appDbContext.SaveChangesAsync();
        }
    }
}