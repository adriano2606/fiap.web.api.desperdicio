using Fiap.Web.Api.Desperdicio.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.Api.Desperdicio.Data.Repository
{
    public class AlimentoRepository : IAlimentoRepository
    {
        private readonly AppDbContext _context;
        public AlimentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Alimento?> GetByIdAsync(int id) =>
            await _context.Alimentos.FirstOrDefaultAsync(e => e.Id == id);

        public async Task<IEnumerable<Alimento>> GetAllAsync(int pageNumber, int pageSize) =>
            await _context.Alimentos
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

        public async Task AddAsync(Alimento alimento)
        {
            await _context.Alimentos.AddAsync(alimento);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Alimento alimento)
        {
            _context.Alimentos.Update(alimento);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return;
            _context.Alimentos.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

}
