using Microsoft.EntityFrameworkCore;
using SchoolCatalog.Server.Data;
using SchoolCatalog.Server.Model;

namespace SchoolCatalog.Server.Repositories
{
    public class MediaRepository : IMediaRepository
    {
        private readonly SchoolCatalogContext _context;

        public MediaRepository(SchoolCatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Media>> GetAllAsync()
        {
            return await _context.Medii
                .Include(m => m.Elev)
                .Include(m => m.Materie)
                .ToListAsync();
        }

        public async Task<Media?> GetByIdAsync(int id)
        {
            return await _context.Medii
                .Include(m => m.Elev)
                .Include(m => m.Materie)
                .FirstOrDefaultAsync(m => m.IdMedie == id);
        }

        public async Task<IEnumerable<Media>> GetByElevAsync(int idElev)
        {
            return await _context.Medii
                .Where(m => m.IdElev == idElev)
                .Include(m => m.Elev)
                .Include(m => m.Materie)
                .ToListAsync();
        }

        public async Task<IEnumerable<Media>> GetByMaterieAsync(int idMaterie)
        {
            return await _context.Medii
                .Where(m => m.IdMaterie == idMaterie)
                .Include(m => m.Elev)
                .Include(m => m.Materie)
                .ToListAsync();
        }

        public async Task<Media?> GetByElevAndMaterieAsync(int idElev, int idMaterie)
        {
            return await _context.Medii
                .Where(m => m.IdElev == idElev && m.IdMaterie == idMaterie)
                .Include(m => m.Elev)
                .Include(m => m.Materie)
                .FirstOrDefaultAsync();
        }

        public async Task<Media> AddAsync(Media media)
        {
            _context.Medii.Add(media);
            await _context.SaveChangesAsync();
            return media;
        }

        public async Task<Media?> UpdateAsync(Media media)
        {
            var existingMedia = await _context.Medii.FindAsync(media.IdMedie);
            if (existingMedia == null)
            {
                return null;
            }

            existingMedia.IdElev = media.IdElev;
            existingMedia.IdMaterie = media.IdMaterie;
            existingMedia.Valoare = media.Valoare;

            _context.Medii.Update(existingMedia);
            await _context.SaveChangesAsync();
            return existingMedia;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var media = await _context.Medii.FindAsync(id);
            if (media == null)
            {
                return false;
            }

            _context.Medii.Remove(media);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Medii.AnyAsync(m => m.IdMedie == id);
        }
    }
}
