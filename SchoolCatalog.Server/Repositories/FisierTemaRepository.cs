using Microsoft.EntityFrameworkCore;
using SchoolCatalog.Server.Data;
using SchoolCatalog.Server.Model;

namespace SchoolCatalog.Server.Repositories
{
    public class FisierTemaRepository : IFisierTemaRepository
    {
        private readonly SchoolCatalogContext _context;

        public FisierTemaRepository(SchoolCatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FisierTema>> GetAllAsync()
        {
            return await _context.FisiereTema
                .Include(f => f.Elev)
                .Include(f => f.Tema)
                .ToListAsync();
        }

        public async Task<FisierTema?> GetByIdAsync(int id)
        {
            return await _context.FisiereTema
                .Include(f => f.Elev)
                .Include(f => f.Tema)
                .FirstOrDefaultAsync(f => f.IdFisier == id);
        }

        public async Task<IEnumerable<FisierTema>> GetByTemaAsync(int temaId)
        {
            return await _context.FisiereTema
                .Where(f => f.TemaId == temaId)
                .Include(f => f.Elev)
                .Include(f => f.Tema)
                .ToListAsync();
        }

        public async Task<IEnumerable<FisierTema>> GetByElevAsync(int idElev)
        {
            return await _context.FisiereTema
                .Where(f => f.IdElev == idElev)
                .Include(f => f.Elev)
                .Include(f => f.Tema)
                .ToListAsync();
        }

        public async Task<FisierTema> AddAsync(FisierTema fisierTema)
        {
            _context.FisiereTema.Add(fisierTema);
            await _context.SaveChangesAsync();
            return fisierTema;
        }

        public async Task<FisierTema?> UpdateAsync(FisierTema fisierTema)
        {
            var existingFisierTema = await _context.FisiereTema.FindAsync(fisierTema.IdFisier);
            if (existingFisierTema == null)
            {
                return null;
            }

            existingFisierTema.NumeFisier = fisierTema.NumeFisier;
            existingFisierTema.UrlFisier = fisierTema.UrlFisier;
            existingFisierTema.IdElev = fisierTema.IdElev;
            existingFisierTema.TemaId = fisierTema.TemaId;

            _context.FisiereTema.Update(existingFisierTema);
            await _context.SaveChangesAsync();
            return existingFisierTema;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var fisierTema = await _context.FisiereTema.FindAsync(id);
            if (fisierTema == null)
            {
                return false;
            }

            _context.FisiereTema.Remove(fisierTema);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.FisiereTema.AnyAsync(f => f.IdFisier == id);
        }
    }
}
