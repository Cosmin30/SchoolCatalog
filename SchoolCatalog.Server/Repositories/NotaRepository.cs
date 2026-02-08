using Microsoft.EntityFrameworkCore;
using SchoolCatalog.Server.Data;
using SchoolCatalog.Server.Model;

namespace SchoolCatalog.Server.Repositories
{
    public class NotaRepository : INotaRepository
    {
        private readonly SchoolCatalogContext _context;

        public NotaRepository(SchoolCatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Nota>> GetAllAsync()
        {
            return await _context.Note
                .Include(n => n.Elev)
                .Include(n => n.Materie)
                .ToListAsync();
        }

        public async Task<Nota?> GetByIdAsync(int id)
        {
            return await _context.Note
                .Include(n => n.Elev)
                .Include(n => n.Materie)
                .FirstOrDefaultAsync(n => n.IdNota == id);
        }

        public async Task<IEnumerable<Nota>> GetByElevAsync(int idElev)
        {
            return await _context.Note
                .Where(n => n.IdElev == idElev)
                .Include(n => n.Elev)
                .Include(n => n.Materie)
                .ToListAsync();
        }

        public async Task<IEnumerable<Nota>> GetByMaterieAsync(int idMaterie)
        {
            return await _context.Note
                .Where(n => n.IdMaterie == idMaterie)
                .Include(n => n.Elev)
                .Include(n => n.Materie)
                .ToListAsync();
        }

        public async Task<IEnumerable<Nota>> GetByElevAndMaterieAsync(int idElev, int idMaterie)
        {
            return await _context.Note
                .Where(n => n.IdElev == idElev && n.IdMaterie == idMaterie)
                .Include(n => n.Elev)
                .Include(n => n.Materie)
                .ToListAsync();
        }

        public async Task<Nota> AddAsync(Nota nota)
        {
            _context.Note.Add(nota);
            await _context.SaveChangesAsync();
            return nota;
        }

        public async Task<Nota?> UpdateAsync(Nota nota)
        {
            var existingNota = await _context.Note.FindAsync(nota.IdNota);
            if (existingNota == null)
            {
                return null;
            }

            existingNota.Valoare = nota.Valoare;
            existingNota.DataNotei = nota.DataNotei;
            existingNota.EsteAnulata = nota.EsteAnulata;
            existingNota.IdElev = nota.IdElev;
            existingNota.IdMaterie = nota.IdMaterie;

            _context.Note.Update(existingNota);
            await _context.SaveChangesAsync();
            return existingNota;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var nota = await _context.Note.FindAsync(id);
            if (nota == null)
            {
                return false;
            }

            _context.Note.Remove(nota);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Note.AnyAsync(n => n.IdNota == id);
        }
    }
}
