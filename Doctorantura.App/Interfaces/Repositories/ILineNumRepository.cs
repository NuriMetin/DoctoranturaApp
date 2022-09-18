using Doctorantura.App.Dtos.LineNumDtos;
using System.Threading.Tasks;

namespace Doctorantura.App.Interfaces.Repositories
{
    public interface ILineNumRepository
    {
        public Task AddLineNumAsync(LineNumCreateDto lineNumDto);
        public Task UpdateLineNumAsync(LineNumUpdateDto lineNumDto);
        public Task DeleteLineNumAsync(int id);
    }
}
