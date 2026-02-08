using SchoolCatalog.Server.Model;

namespace SchoolCatalog.Server.Repositories
{
    public interface IElevRepository
    {
        Task<IEnumerable<Elev>> GetAllAsync();
        Task<Elev?> GetByIdAsync(int id);
        Task<IEnumerable<Elev>> GetByClasaAsync(int idClasa);
        Task<Elev> AddAsync(Elev elev);
        Task<Elev?> UpdateAsync(Elev elev);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
