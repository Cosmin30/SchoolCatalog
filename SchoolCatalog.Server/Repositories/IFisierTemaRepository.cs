using SchoolCatalog.Server.Model;

namespace SchoolCatalog.Server.Repositories
{
    public interface IFisierTemaRepository
    {
        Task<IEnumerable<FisierTema>> GetAllAsync();
        Task<FisierTema?> GetByIdAsync(int id);
        Task<IEnumerable<FisierTema>> GetByTemaAsync(int temaId);
        Task<IEnumerable<FisierTema>> GetByElevAsync(int idElev);
        Task<FisierTema> AddAsync(FisierTema fisierTema);
        Task<FisierTema?> UpdateAsync(FisierTema fisierTema);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
