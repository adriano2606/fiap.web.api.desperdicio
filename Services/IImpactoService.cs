using Fiap.Web.Api.Desperdicio.Models;

namespace Fiap.Web.Api.Desperdicio.Services
{
    public interface IImpactoService
    {
        Task<Impacto?> GetByIdAsync(int id);
        Task<IEnumerable<Impacto>> GetAllAsync(int pageNumber, int pageSize);
        Task AddAsync(Impacto impacto);
        Task UpdateAsync(Impacto impacto);
        Task DeleteAsync(int id);
    }
}
