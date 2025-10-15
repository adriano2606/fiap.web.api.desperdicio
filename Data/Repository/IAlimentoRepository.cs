using Fiap.Web.Api.Desperdicio.Models;

namespace Fiap.Web.Api.Desperdicio.Data.Repository
{
    public interface IAlimentoRepository
    {
        Task<Alimento?> GetByIdAsync(int id);
        Task<IEnumerable<Alimento>> GetAllAsync(int pageNumber, int pageSize);
        Task AddAsync(Alimento alimento);
        Task UpdateAsync(Alimento alimento);
        Task DeleteAsync(int id);
    }
}
