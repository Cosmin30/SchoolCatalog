using SchoolCatalog.Server.Model;

namespace SchoolCatalog.Server.Repositories
{
    public interface IProfesorRepository
    {
        Task<IEnumerable<Profesor>> GetAllAsync();
        Task<Profesor?> GetByIdAsync(int id);
        Task<Profesor> AddAsync(Profesor profesor);
        Task<Profesor?> UpdateAsync(Profesor profesor);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
