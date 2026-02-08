using Microsoft.EntityFrameworkCore;
using SchoolCatalog.Server.Data;
using SchoolCatalog.Server.Model;

namespace SchoolCatalog.Server.Repositories
{
    public class MaterieRepository : IMaterieRepository
    {
        private readonly SchoolCatalogContext _context;

        public MaterieRepository(SchoolCatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Materie>> GetAllAsync()
        {
            return await _context.Materii
                .Include(m => m.Profesor)
                .ToListAsync();
        }

        public async Task<Materie?> GetByIdAsync(int id)
        {
            return await _context.Materii
                .Include(m => m.Profesor)
                .FirstOrDefaultAsync(m => m.IdMaterie == id);
        }

        public async Task<IEnumerable<Materie>> GetByProfesorAsync(int idProfesor)
        {
            return await _context.Materii
                .Where(m => m.ProfesorId == idProfesor)
                .Include(m => m.Profesor)
                .ToListAsync();
        }

        public async Task<Materie> AddAsync(Materie materie)
        {
            _context.Materii.Add(materie);
            await _context.SaveChangesAsync();
            return materie;
        }

        public async Task<Materie?> UpdateAsync(Materie materie)
        {
            var existingMaterie = await _context.Materii.FindAsync(materie.IdMaterie);
            if (existingMaterie == null)
            {
                return null;
            }

            existingMaterie.NumeMaterie = materie.NumeMaterie;
            existingMaterie.ProfesorId = materie.ProfesorId;

            _context.Materii.Update(existingMaterie);
            await _context.SaveChangesAsync();
            return existingMaterie;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var materie = await _context.Materii.FindAsync(id);
            if (materie == null)
            {
                return false;
            }

            _context.Materii.Remove(materie);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Materii.AnyAsync(m => m.IdMaterie == id);
        }
    }
}
