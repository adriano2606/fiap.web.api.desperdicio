using Fiap.Web.Api.Desperdicio.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.Api.Desperdicio.Data.Repository
{
    public class ImpactoRepository : IImpactoRepository
    {
        private readonly AppDbContext _context;
        public ImpactoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Impacto?> GetByIdAsync(int id) =>
            await _context.Impactos.FirstOrDefaultAsync(e => e.Id == id);

        public async Task<IEnumerable<Impacto>> GetAllAsync(int pageNumber, int pageSize) =>
            await _context.Impactos
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

        public async Task AddAsync(Impacto impacto)
        {
            await _context.Impactos.AddAsync(impacto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Impacto impacto)
        {
            _context.Impactos.Update(impacto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return;
            _context.Impactos.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
