using SchoolCatalog.Server.Dtos;
using SchoolCatalog.Server.Model;
using SchoolCatalog.Server.Repositories;

namespace SchoolCatalog.Server.Services
{
    public class NotaService : INotaService
    {
        private readonly INotaRepository _notaRepository;

        public NotaService(INotaRepository notaRepository)
        {
            _notaRepository = notaRepository;
        }

        public async Task<IEnumerable<NotaDto>> GetAllNoteAsync()
        {
            var note = await _notaRepository.GetAllAsync();
            return note.Select(MapToDto);
        }

        public async Task<NotaDto?> GetNotaByIdAsync(int id)
        {
            var nota = await _notaRepository.GetByIdAsync(id);
            return nota != null ? MapToDto(nota) : null;
        }

        public async Task<IEnumerable<NotaDto>> GetNoteByElevAsync(int idElev)
        {
            var note = await _notaRepository.GetByElevAsync(idElev);
            return note.Select(MapToDto);
        }

        public async Task<IEnumerable<NotaDto>> GetNoteByMaterieAsync(int idMaterie)
        {
            var note = await _notaRepository.GetByMaterieAsync(idMaterie);
            return note.Select(MapToDto);
        }

        public async Task<IEnumerable<NotaDto>> GetNoteByElevAndMaterieAsync(int idElev, int idMaterie)
        {
            var note = await _notaRepository.GetByElevAndMaterieAsync(idElev, idMaterie);
            return note.Select(MapToDto);
        }

        public async Task<NotaDto> CreateNotaAsync(CreateNotaDto createNotaDto)
        {
            var nota = new Nota
            {
                Valoare = createNotaDto.Valoare,
                DataNotei = createNotaDto.DataNotei,
                IdElev = createNotaDto.IdElev,
                IdMaterie = createNotaDto.IdMaterie,
                EsteAnulata = false
            };

            var createdNota = await _notaRepository.AddAsync(nota);
            var result = await _notaRepository.GetByIdAsync(createdNota.IdNota);
            return MapToDto(result!);
        }

        public async Task<NotaDto?> UpdateNotaAsync(int id, UpdateNotaDto updateNotaDto)
        {
            var nota = new Nota
            {
                IdNota = id,
                Valoare = updateNotaDto.Valoare,
                DataNotei = updateNotaDto.DataNotei,
                EsteAnulata = updateNotaDto.EsteAnulata,
                IdElev = updateNotaDto.IdElev,
                IdMaterie = updateNotaDto.IdMaterie
            };

            var updatedNota = await _notaRepository.UpdateAsync(nota);
            if (updatedNota == null)
            {
                return null;
            }

            var result = await _notaRepository.GetByIdAsync(updatedNota.IdNota);
            return result != null ? MapToDto(result) : null;
        }

        public async Task<bool> DeleteNotaAsync(int id)
        {
            return await _notaRepository.DeleteAsync(id);
        }

        public async Task<bool> ToggleAnulataAsync(int id)
        {
            var nota = await _notaRepository.GetByIdAsync(id);
            if (nota == null)
            {
                return false;
            }

            nota.EsteAnulata = !nota.EsteAnulata;
            var updated = await _notaRepository.UpdateAsync(nota);
            return updated != null;
        }

        private static NotaDto MapToDto(Nota nota)
        {
            return new NotaDto
            {
                IdNota = nota.IdNota,
                Valoare = nota.Valoare,
                DataNotei = nota.DataNotei,
                EsteAnulata = nota.EsteAnulata,
                IdElev = nota.IdElev,
                NumeElev = nota.Elev?.NumeElev,
                PrenumeElev = nota.Elev?.PrenumeElev,
                IdMaterie = nota.IdMaterie,
                NumeMaterie = nota.Materie?.NumeMaterie
            };
        }
    }
}
