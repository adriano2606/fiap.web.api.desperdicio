using Fiap.Web.Api.Desperdicio.Data.Repository;
using Fiap.Web.Api.Desperdicio.Models;

namespace Fiap.Web.Api.Desperdicio.Services
{
    public class ImpactoService : IImpactoService
    {
        private readonly IImpactoRepository _repository;

        public ImpactoService(IImpactoRepository repository)
        {
            _repository = repository;
        }

        public Task<Impacto?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

        public Task<IEnumerable<Impacto>> GetAllAsync(int pageNumber, int pageSize) =>
            _repository.GetAllAsync(pageNumber, pageSize);

        public Task AddAsync(Impacto impacto) => _repository.AddAsync(impacto);

        public Task UpdateAsync(Impacto impacto) => _repository.UpdateAsync(impacto);

        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
