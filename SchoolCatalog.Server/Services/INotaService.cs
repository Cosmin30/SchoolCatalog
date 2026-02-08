using SchoolCatalog.Server.Dtos;

namespace SchoolCatalog.Server.Services
{
    public interface INotaService
    {
        Task<IEnumerable<NotaDto>> GetAllNoteAsync();
        Task<NotaDto?> GetNotaByIdAsync(int id);
        Task<IEnumerable<NotaDto>> GetNoteByElevAsync(int idElev);
        Task<IEnumerable<NotaDto>> GetNoteByMaterieAsync(int idMaterie);
        Task<IEnumerable<NotaDto>> GetNoteByElevAndMaterieAsync(int idElev, int idMaterie);
        Task<NotaDto> CreateNotaAsync(CreateNotaDto createNotaDto);
        Task<NotaDto?> UpdateNotaAsync(int id, UpdateNotaDto updateNotaDto);
        Task<bool> DeleteNotaAsync(int id);
        Task<bool> ToggleAnulataAsync(int id);
    }
}
