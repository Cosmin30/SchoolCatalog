using SchoolCatalog.Server.Dtos;

namespace SchoolCatalog.Server.Services
{
    public interface IProfesorService
    {
        Task<IEnumerable<ProfesorDto>> GetAllProfesoriAsync();
        Task<ProfesorDto?> GetProfesorByIdAsync(int id);
        Task<ProfesorDto> CreateProfesorAsync(CreateProfesorDto createProfesorDto);
        Task<ProfesorDto?> UpdateProfesorAsync(int id, UpdateProfesorDto updateProfesorDto);
        Task<bool> DeleteProfesorAsync(int id);
    }
}
