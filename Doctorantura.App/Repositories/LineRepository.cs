using AutoMapper;
using Doctorantura.App.Context;
using Doctorantura.App.Dtos.LineNumDtos;
using System.Threading.Tasks;
using Doctorantura.App.Models;
using Doctorantura.App.Interfaces.Repositories;

namespace Doctorantura.App.Repositories
{
    public class LineRepository : ILineNumRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public LineRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task AddLineNumAsync(LineNumCreateDto lineNumDto)
        {
            Line lineNum = _mapper.Map<Line>(lineNumDto);
            await _appDbContext.Lines.AddAsync(lineNum);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateLineNumAsync(LineNumUpdateDto lineNumDto)
        {
            Line lineNum = await _appDbContext.Lines.FindAsync(lineNumDto.ID);
            lineNum = _mapper.Map<Line>(lineNumDto);

            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteLineNumAsync(int id)
        {
            Line lineNum = await _appDbContext.Lines.FindAsync(id);
            _appDbContext.Lines.Remove(lineNum);

            await _appDbContext.SaveChangesAsync();
        }
    }
}
