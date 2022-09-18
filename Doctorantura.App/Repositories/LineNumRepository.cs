using AutoMapper;
using Doctorantura.App.Context;
using Doctorantura.App.Dtos.LineNumDtos;
using System.Threading.Tasks;
using Doctorantura.App.Models;
using Doctorantura.App.Interfaces.Repositories;

namespace Doctorantura.App.Repositories
{
    public class LineNumRepository : ILineNumRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public LineNumRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task AddLineNumAsync(LineNumCreateDto lineNumDto)
        {
            LineNum lineNum = _mapper.Map<LineNum>(lineNumDto);
            await _appDbContext.LineNums.AddAsync(lineNum);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateLineNumAsync(LineNumUpdateDto lineNumDto)
        {
            LineNum lineNum = await _appDbContext.LineNums.FindAsync(lineNumDto.ID);
            lineNum = _mapper.Map<LineNum>(lineNumDto);

            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteLineNumAsync(int id)
        {
            LineNum lineNum = await _appDbContext.LineNums.FindAsync(id);
            _appDbContext.LineNums.Remove(lineNum);

            await _appDbContext.SaveChangesAsync();
        }
    }
}
