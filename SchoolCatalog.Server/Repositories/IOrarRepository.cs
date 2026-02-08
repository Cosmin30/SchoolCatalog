using SchoolCatalog.Server.Model;

namespace SchoolCatalog.Server.Repositories
{
    public interface IOrarRepository
    {
        Task<IEnumerable<Orar>> GetAllAsync();
        Task<Orar?> GetByIdAsync(int id);
        Task<Orar?> GetByClasaAsync(int idClasa);
        Task<Orar> AddAsync(Orar orar);
        Task<Orar?> UpdateAsync(Orar orar);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
