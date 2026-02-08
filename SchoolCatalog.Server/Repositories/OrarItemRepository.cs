using Microsoft.EntityFrameworkCore;
using SchoolCatalog.Server.Data;
using SchoolCatalog.Server.Model;

namespace SchoolCatalog.Server.Repositories
{
    public class OrarItemRepository : IOrarItemRepository
    {
        private readonly SchoolCatalogContext _context;

        public OrarItemRepository(SchoolCatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrarItem>> GetAllAsync()
        {
            return await _context.OrarItems
                .Include(oi => oi.Materie)
                .Include(oi => oi.Profesor)
                .Include(oi => oi.Orar)
                .ToListAsync();
        }

        public async Task<OrarItem?> GetByIdAsync(int id)
        {
            return await _context.OrarItems
                .Include(oi => oi.Materie)
                .Include(oi => oi.Profesor)
                .Include(oi => oi.Orar)
                .FirstOrDefaultAsync(oi => oi.IdOrarItem == id);
        }

        public async Task<IEnumerable<OrarItem>> GetByOrarAsync(int idOrar)
        {
            return await _context.OrarItems
                .Where(oi => oi.IdOrar == idOrar)
                .Include(oi => oi.Materie)
                .Include(oi => oi.Profesor)
                .ToListAsync();
        }

        public async Task<OrarItem> AddAsync(OrarItem orarItem)
        {
            _context.OrarItems.Add(orarItem);
            await _context.SaveChangesAsync();
            return orarItem;
        }

        public async Task<OrarItem?> UpdateAsync(OrarItem orarItem)
        {
            var existingOrarItem = await _context.OrarItems.FindAsync(orarItem.IdOrarItem);
            if (existingOrarItem == null)
            {
                return null;
            }

            existingOrarItem.ZiSaptamana = orarItem.ZiSaptamana;
            existingOrarItem.OraInceput = orarItem.OraInceput;
            existingOrarItem.OraSfarsit = orarItem.OraSfarsit;
            existingOrarItem.IdMaterie = orarItem.IdMaterie;
            existingOrarItem.IdProfesor = orarItem.IdProfesor;
            existingOrarItem.IdOrar = orarItem.IdOrar;

            _context.OrarItems.Update(existingOrarItem);
            await _context.SaveChangesAsync();
            return existingOrarItem;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var orarItem = await _context.OrarItems.FindAsync(id);
            if (orarItem == null)
            {
                return false;
            }

            _context.OrarItems.Remove(orarItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.OrarItems.AnyAsync(oi => oi.IdOrarItem == id);
        }
    }
}
