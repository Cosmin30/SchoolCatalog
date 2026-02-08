using SchoolCatalog.Server.Model;

namespace SchoolCatalog.Server.Repositories
{
    public interface IClasaRepository
    {
        Task<IEnumerable<Clasa>> GetAllAsync();
        Task<Clasa?> GetByIdAsync(int id);
        Task<Clasa> AddAsync(Clasa clasa);
        Task<Clasa?> UpdateAsync(Clasa clasa);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
