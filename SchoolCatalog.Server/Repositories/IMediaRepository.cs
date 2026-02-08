using SchoolCatalog.Server.Model;

namespace SchoolCatalog.Server.Repositories
{
    public interface IMediaRepository
    {
        Task<IEnumerable<Media>> GetAllAsync();
        Task<Media?> GetByIdAsync(int id);
        Task<IEnumerable<Media>> GetByElevAsync(int idElev);
        Task<IEnumerable<Media>> GetByMaterieAsync(int idMaterie);
        Task<Media?> GetByElevAndMaterieAsync(int idElev, int idMaterie);
        Task<Media> AddAsync(Media media);
        Task<Media?> UpdateAsync(Media media);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
