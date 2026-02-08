using SchoolCatalog.Server.Dtos;

namespace SchoolCatalog.Server.Services
{
    public interface IElevService
    {
        Task<IEnumerable<ElevDto>> GetAllEleviAsync();
        Task<ElevDto?> GetElevByIdAsync(int id);
        Task<IEnumerable<ElevDto>> GetEleviByClasaAsync(int idClasa);
        Task<ElevDto> CreateElevAsync(CreateElevDto createElevDto);
        Task<ElevDto?> UpdateElevAsync(int id, UpdateElevDto updateElevDto);
        Task<bool> DeleteElevAsync(int id);
    }
}
