using SchoolCatalog.Server.Dtos;

namespace SchoolCatalog.Server.Services
{
    public interface ITemaService
    {
        Task<IEnumerable<TemaDto>> GetAllTemeAsync();
        Task<TemaDto?> GetTemaByIdAsync(int id);
        Task<IEnumerable<TemaDto>> GetTemeByClasaAsync(int idClasa);
        Task<IEnumerable<TemaDto>> GetTemeByMaterieAsync(int idMaterie);
        Task<IEnumerable<TemaDto>> GetTemeByClasaAndMaterieAsync(int idClasa, int idMaterie);
        Task<IEnumerable<TemaDto>> GetTemeByClasaAndMaterieAndTermenLimitaAsync(int idClasa, int idMaterie, DateTime termenLimita);
        Task<TemaDto> CreateTemaAsync(CreateTemaDto createTemaDto);
        Task<IEnumerable<TemaDto>> CreateTemeAsync(IEnumerable<CreateTemaDto> createTemeDtos);
        Task<TemaDto?> UpdateTemaAsync(int id, UpdateTemaDto updateTemaDto);
        Task<bool> DeleteTemaAsync(int id);
        Task<TemaDto?> UpdateTermenLimitaAsync(int id, UpdateTermenLimitaDto updateTermenLimitaDto);
    }
}
