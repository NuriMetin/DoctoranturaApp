using AutoMapper;
using Doctorantura.App.Context;
using Doctorantura.App.Dtos.ColumnNumDtos;
using Doctorantura.App.Interfaces.Repositories;
using Doctorantura.App.Models;
using System.Threading.Tasks;

namespace Doctorantura.App.Repository
{
    public class ColumnNumRepository: IColumnNumRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public ColumnNumRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task AddColumnNumAsync(ColumnNumCreateDto columnNumDto)
        {
            ColumnNum columnNum = _mapper.Map<ColumnNum>(columnNumDto);
            await _appDbContext.ColumnNums.AddAsync(columnNum);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateColumnNumAsync(ColumnNumUpdateDto columnNumDto)
        {
            ColumnNum columnNum = await _appDbContext.ColumnNums.FindAsync(columnNumDto.ID);
            columnNum = _mapper.Map<ColumnNum>(columnNumDto);
        
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteColumnNumAsync(int id)
        {
            ColumnNum columnNum = await _appDbContext.ColumnNums.FindAsync(id);
            _appDbContext.ColumnNums.Remove(columnNum);

            await _appDbContext.SaveChangesAsync();
        }
    }
}