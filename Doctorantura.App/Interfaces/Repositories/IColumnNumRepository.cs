using Doctorantura.App.Dtos.ColumnNumDtos;
using System.Threading.Tasks;

namespace Doctorantura.App.Interfaces.Repositories
{
    public interface IColumnNumRepository
    {
        public Task AddColumnNumAsync(ColumnNumCreateDto columnNumDto);
        public Task UpdateColumnNumAsync(ColumnNumUpdateDto columnNumDto);
        public Task DeleteColumnNumAsync(int id);
    }
}
