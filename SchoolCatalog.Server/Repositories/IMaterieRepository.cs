using SchoolCatalog.Server.Model;

namespace SchoolCatalog.Server.Repositories
{
    public interface IMaterieRepository
    {
        Task<IEnumerable<Materie>> GetAllAsync();
        Task<Materie?> GetByIdAsync(int id);
        Task<IEnumerable<Materie>> GetByProfesorAsync(int idProfesor);
        Task<Materie> AddAsync(Materie materie);
        Task<Materie?> UpdateAsync(Materie materie);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
