using Microsoft.EntityFrameworkCore;
using SchoolCatalog.Server.Data;
using SchoolCatalog.Server.Model;

namespace SchoolCatalog.Server.Repositories
{
    public class OrarRepository : IOrarRepository
    {
        private readonly SchoolCatalogContext _context;

        public OrarRepository(SchoolCatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Orar>> GetAllAsync()
        {
            return await _context.Orare
                .Include(o => o.Clasa)
                .Include(o => o.OrarItems)
                .ToListAsync();
        }

        public async Task<Orar?> GetByIdAsync(int id)
        {
            return await _context.Orare
                .Include(o => o.Clasa)
                .Include(o => o.OrarItems)
                    .ThenInclude(oi => oi.Materie)
                .Include(o => o.OrarItems)
                    .ThenInclude(oi => oi.Profesor)
                .FirstOrDefaultAsync(o => o.IdOrar == id);
        }

        public async Task<Orar?> GetByClasaAsync(int idClasa)
        {
            return await _context.Orare
                .Where(o => o.IdClasa == idClasa)
                .Include(o => o.Clasa)
                .Include(o => o.OrarItems)
                    .ThenInclude(oi => oi.Materie)
                .Include(o => o.OrarItems)
                    .ThenInclude(oi => oi.Profesor)
                .FirstOrDefaultAsync();
        }

        public async Task<Orar> AddAsync(Orar orar)
        {
            _context.Orare.Add(orar);
            await _context.SaveChangesAsync();
            return orar;
        }

        public async Task<Orar?> UpdateAsync(Orar orar)
        {
            var existingOrar = await _context.Orare.FindAsync(orar.IdOrar);
            if (existingOrar == null)
            {
                return null;
            }

            existingOrar.DescriereOrar = orar.DescriereOrar;
            existingOrar.AnScolar = orar.AnScolar;
            existingOrar.IdClasa = orar.IdClasa;

            _context.Orare.Update(existingOrar);
            await _context.SaveChangesAsync();
            return existingOrar;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var orar = await _context.Orare.FindAsync(id);
            if (orar == null)
            {
                return false;
            }

            _context.Orare.Remove(orar);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Orare.AnyAsync(o => o.IdOrar == id);
        }
    }
}
