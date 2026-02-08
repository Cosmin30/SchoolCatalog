using Microsoft.EntityFrameworkCore;
using SchoolCatalog.Server.Data;
using SchoolCatalog.Server.Model;

namespace SchoolCatalog.Server.Repositories
{
    public class ElevRepository : IElevRepository
    {
        private readonly SchoolCatalogContext _context;

        public ElevRepository(SchoolCatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Elev>> GetAllAsync()
        {
            return await _context.Elevi
                .Include(e => e.Clasa)
                .ToListAsync();
        }

        public async Task<Elev?> GetByIdAsync(int id)
        {
            return await _context.Elevi
                .Include(e => e.Clasa)
                .FirstOrDefaultAsync(e => e.IdElev == id);
        }

        public async Task<IEnumerable<Elev>> GetByClasaAsync(int idClasa)
        {
            return await _context.Elevi
                .Where(e => e.ClasaId == idClasa)
                .Include(e => e.Clasa)
                .ToListAsync();
        }

        public async Task<Elev> AddAsync(Elev elev)
        {
            _context.Elevi.Add(elev);
            await _context.SaveChangesAsync();
            return elev;
        }

        public async Task<Elev?> UpdateAsync(Elev elev)
        {
            var existingElev = await _context.Elevi.FindAsync(elev.IdElev);
            if (existingElev == null)
            {
                return null;
            }

            existingElev.NumeElev = elev.NumeElev;
            existingElev.PrenumeElev = elev.PrenumeElev;
            existingElev.DataNasterii = elev.DataNasterii;
            existingElev.ClasaId = elev.ClasaId;

            _context.Elevi.Update(existingElev);
            await _context.SaveChangesAsync();
            return existingElev;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var elev = await _context.Elevi.FindAsync(id);
            if (elev == null)
            {
                return false;
            }

            _context.Elevi.Remove(elev);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Elevi.AnyAsync(e => e.IdElev == id);
        }
    }
}
