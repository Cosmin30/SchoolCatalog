using Microsoft.EntityFrameworkCore;
using SchoolCatalog.Server.Data;
using SchoolCatalog.Server.Model;

namespace SchoolCatalog.Server.Repositories
{
    public class ProfesorRepository : IProfesorRepository
    {
        private readonly SchoolCatalogContext _context;

        public ProfesorRepository(SchoolCatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Profesor>> GetAllAsync()
        {
            return await _context.Profesori.ToListAsync();
        }

        public async Task<Profesor?> GetByIdAsync(int id)
        {
            return await _context.Profesori
                .FirstOrDefaultAsync(p => p.IdProfesor == id);
        }

        public async Task<Profesor> AddAsync(Profesor profesor)
        {
            _context.Profesori.Add(profesor);
            await _context.SaveChangesAsync();
            return profesor;
        }

        public async Task<Profesor?> UpdateAsync(Profesor profesor)
        {
            var existingProfesor = await _context.Profesori.FindAsync(profesor.IdProfesor);
            if (existingProfesor == null)
            {
                return null;
            }

            existingProfesor.NumeProfesor = profesor.NumeProfesor;
            existingProfesor.PrenumeProfesor = profesor.PrenumeProfesor;
            existingProfesor.EmailProfesor = profesor.EmailProfesor;
            existingProfesor.DataNasterii = profesor.DataNasterii;

            _context.Profesori.Update(existingProfesor);
            await _context.SaveChangesAsync();
            return existingProfesor;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var profesor = await _context.Profesori.FindAsync(id);
            if (profesor == null)
            {
                return false;
            }

            _context.Profesori.Remove(profesor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Profesori.AnyAsync(p => p.IdProfesor == id);
        }
    }
}
