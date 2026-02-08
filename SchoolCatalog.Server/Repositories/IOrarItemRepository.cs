using SchoolCatalog.Server.Model;

namespace SchoolCatalog.Server.Repositories
{
    public interface IOrarItemRepository
    {
        Task<IEnumerable<OrarItem>> GetAllAsync();
        Task<OrarItem?> GetByIdAsync(int id);
        Task<IEnumerable<OrarItem>> GetByOrarAsync(int idOrar);
        Task<OrarItem> AddAsync(OrarItem orarItem);
        Task<OrarItem?> UpdateAsync(OrarItem orarItem);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
