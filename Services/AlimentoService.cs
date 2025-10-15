using Fiap.Web.Api.Desperdicio.Data.Repository;
using Fiap.Web.Api.Desperdicio.Models;

namespace Fiap.Web.Api.Desperdicio.Services
{
    public class AlimentoService : IAlimentoService
    {
        private readonly IAlimentoRepository _repository;

        public AlimentoService(IAlimentoRepository repository)
        {
            _repository = repository;
        }

        public Task<Alimento?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

        public Task<IEnumerable<Alimento>> GetAllAsync(int pageNumber, int pageSize) =>
            _repository.GetAllAsync(pageNumber, pageSize);

        public Task AddAsync(Alimento alimento) => _repository.AddAsync(alimento);

        public Task UpdateAsync(Alimento alimento) => _repository.UpdateAsync(alimento);

        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
