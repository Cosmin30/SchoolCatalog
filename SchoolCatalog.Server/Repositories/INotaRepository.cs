using SchoolCatalog.Server.Model;

namespace SchoolCatalog.Server.Repositories
{
    public interface INotaRepository
    {
        Task<IEnumerable<Nota>> GetAllAsync();
        Task<Nota?> GetByIdAsync(int id);
        Task<IEnumerable<Nota>> GetByElevAsync(int idElev);
        Task<IEnumerable<Nota>> GetByMaterieAsync(int idMaterie);
        Task<IEnumerable<Nota>> GetByElevAndMaterieAsync(int idElev, int idMaterie);
        Task<Nota> AddAsync(Nota nota);
        Task<Nota?> UpdateAsync(Nota nota);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
