using Microsoft.EntityFrameworkCore;
using SchoolCatalog.Server.Data;
using SchoolCatalog.Server.Model;

namespace SchoolCatalog.Server.Repositories
{
    public class TemaRepository : ITemaRepository
    {
        private readonly SchoolCatalogContext _context;

        public TemaRepository(SchoolCatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tema>> GetAllAsync()
        {
            return await _context.Teme
                .Include(t => t.Materie)
                .Include(t => t.Clasa)
                .ToListAsync();
        }

        public async Task<Tema?> GetByIdAsync(int id)
        {
            return await _context.Teme
                .Include(t => t.Materie)
                .Include(t => t.Clasa)
                .FirstOrDefaultAsync(t => t.IdTema == id);
        }

        public async Task<IEnumerable<Tema>> GetByClasaAsync(int idClasa)
        {
            return await _context.Teme
                .Where(t => t.IdClasa == idClasa)
                .Include(t => t.Materie)
                .Include(t => t.Clasa)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tema>> GetByMaterieAsync(int idMaterie)
        {
            return await _context.Teme
                .Where(t => t.IdMaterie == idMaterie)
                .Include(t => t.Materie)
                .Include(t => t.Clasa)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tema>> GetByClasaAndMaterieAsync(int idClasa, int idMaterie)
        {
            return await _context.Teme
                .Where(t => t.IdClasa == idClasa && t.IdMaterie == idMaterie)
                .Include(t => t.Materie)
                .Include(t => t.Clasa)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tema>> GetByClasaAndMaterieAndTermenLimitaAsync(int idClasa, int idMaterie, DateTime termenLimita)
        {
            return await _context.Teme
                .Where(t => t.IdClasa == idClasa && t.IdMaterie == idMaterie && t.TermenLimita.Date == termenLimita.Date)
                .Include(t => t.Materie)
                .Include(t => t.Clasa)
                .ToListAsync();
        }

        public async Task<Tema> AddAsync(Tema tema)
        {
            _context.Teme.Add(tema);
            await _context.SaveChangesAsync();
            return tema;
        }

        public async Task<IEnumerable<Tema>> AddRangeAsync(IEnumerable<Tema> teme)
        {
            _context.Teme.AddRange(teme);
            await _context.SaveChangesAsync();
            return teme;
        }

        public async Task<Tema?> UpdateAsync(Tema tema)
        {
            var existingTema = await _context.Teme.FindAsync(tema.IdTema);
            if (existingTema == null)
            {
                return null;
            }

            existingTema.Descriere = tema.Descriere;
            existingTema.TermenLimita = tema.TermenLimita;
            existingTema.IdMaterie = tema.IdMaterie;
            existingTema.IdClasa = tema.IdClasa;

            _context.Teme.Update(existingTema);
            await _context.SaveChangesAsync();
            return existingTema;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tema = await _context.Teme.FindAsync(id);
            if (tema == null)
            {
                return false;
            }

            _context.Teme.Remove(tema);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Tema?> UpdateTermenLimitaAsync(int id, DateTime termenLimita)
        {
            var tema = await _context.Teme.FindAsync(id);
            if (tema == null)
            {
                return null;
            }

            tema.TermenLimita = termenLimita;
            _context.Teme.Update(tema);
            await _context.SaveChangesAsync();
            return tema;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Teme.AnyAsync(t => t.IdTema == id);
        }
    }
}
