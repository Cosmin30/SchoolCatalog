using Microsoft.EntityFrameworkCore;
using SchoolCatalog.Server.Data;
using SchoolCatalog.Server.Model;

namespace SchoolCatalog.Server.Repositories
{
    public class ClasaRepository : IClasaRepository
    {
        private readonly SchoolCatalogContext _context;

        public ClasaRepository(SchoolCatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Clasa>> GetAllAsync()
        {
            return await _context.Clase.ToListAsync();
        }

        public async Task<Clasa?> GetByIdAsync(int id)
        {
            return await _context.Clase
                .FirstOrDefaultAsync(c => c.IdClasa == id);
        }

        public async Task<Clasa> AddAsync(Clasa clasa)
        {
            _context.Clase.Add(clasa);
            await _context.SaveChangesAsync();
            return clasa;
        }

        public async Task<Clasa?> UpdateAsync(Clasa clasa)
        {
            var existingClasa = await _context.Clase.FindAsync(clasa.IdClasa);
            if (existingClasa == null)
            {
                return null;
            }

            existingClasa.NumeClasa = clasa.NumeClasa;
            existingClasa.ProfilClasa = clasa.ProfilClasa;

            _context.Clase.Update(existingClasa);
            await _context.SaveChangesAsync();
            return existingClasa;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var clasa = await _context.Clase.FindAsync(id);
            if (clasa == null)
            {
                return false;
            }

            _context.Clase.Remove(clasa);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Clase.AnyAsync(c => c.IdClasa == id);
        }
    }
}
