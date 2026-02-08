using SchoolCatalog.Server.Model;

namespace SchoolCatalog.Server.Repositories
{
    public interface ITemaRepository
    {
        Task<IEnumerable<Tema>> GetAllAsync();
        Task<Tema?> GetByIdAsync(int id);
        Task<IEnumerable<Tema>> GetByClasaAsync(int idClasa);
        Task<IEnumerable<Tema>> GetByMaterieAsync(int idMaterie);
        Task<IEnumerable<Tema>> GetByClasaAndMaterieAsync(int idClasa, int idMaterie);
        Task<IEnumerable<Tema>> GetByClasaAndMaterieAndTermenLimitaAsync(int idClasa, int idMaterie, DateTime termenLimita);
        Task<Tema> AddAsync(Tema tema);
        Task<IEnumerable<Tema>> AddRangeAsync(IEnumerable<Tema> teme);
        Task<Tema?> UpdateAsync(Tema tema);
        Task<bool> DeleteAsync(int id);
        Task<Tema?> UpdateTermenLimitaAsync(int id, DateTime termenLimita);
        Task<bool> ExistsAsync(int id);
    }
}
