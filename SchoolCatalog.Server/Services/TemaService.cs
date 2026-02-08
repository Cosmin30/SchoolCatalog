using SchoolCatalog.Server.Dtos;
using SchoolCatalog.Server.Model;
using SchoolCatalog.Server.Repositories;

namespace SchoolCatalog.Server.Services
{
    public class TemaService : ITemaService
    {
        private readonly ITemaRepository _temaRepository;

        public TemaService(ITemaRepository temaRepository)
        {
            _temaRepository = temaRepository;
        }

        public async Task<IEnumerable<TemaDto>> GetAllTemeAsync()
        {
            var teme = await _temaRepository.GetAllAsync();
            return teme.Select(MapToDto);
        }

        public async Task<TemaDto?> GetTemaByIdAsync(int id)
        {
            var tema = await _temaRepository.GetByIdAsync(id);
            return tema != null ? MapToDto(tema) : null;
        }

        public async Task<IEnumerable<TemaDto>> GetTemeByClasaAsync(int idClasa)
        {
            var teme = await _temaRepository.GetByClasaAsync(idClasa);
            return teme.Select(MapToDto);
        }

        public async Task<IEnumerable<TemaDto>> GetTemeByMaterieAsync(int idMaterie)
        {
            var teme = await _temaRepository.GetByMaterieAsync(idMaterie);
            return teme.Select(MapToDto);
        }

        public async Task<IEnumerable<TemaDto>> GetTemeByClasaAndMaterieAsync(int idClasa, int idMaterie)
        {
            var teme = await _temaRepository.GetByClasaAndMaterieAsync(idClasa, idMaterie);
            return teme.Select(MapToDto);
        }

        public async Task<IEnumerable<TemaDto>> GetTemeByClasaAndMaterieAndTermenLimitaAsync(int idClasa, int idMaterie, DateTime termenLimita)
        {
            var teme = await _temaRepository.GetByClasaAndMaterieAndTermenLimitaAsync(idClasa, idMaterie, termenLimita);
            return teme.Select(MapToDto);
        }

        public async Task<TemaDto> CreateTemaAsync(CreateTemaDto createTemaDto)
        {
            var tema = new Tema
            {
                Descriere = createTemaDto.Descriere,
                TermenLimita = createTemaDto.TermenLimita,
                IdMaterie = createTemaDto.IdMaterie,
                IdClasa = createTemaDto.IdClasa
            };

            var createdTema = await _temaRepository.AddAsync(tema);
            var result = await _temaRepository.GetByIdAsync(createdTema.IdTema);
            return MapToDto(result!);
        }

        public async Task<IEnumerable<TemaDto>> CreateTemeAsync(IEnumerable<CreateTemaDto> createTemeDtos)
        {
            var teme = createTemeDtos.Select(dto => new Tema
            {
                Descriere = dto.Descriere,
                TermenLimita = dto.TermenLimita,
                IdMaterie = dto.IdMaterie,
                IdClasa = dto.IdClasa
            });

            await _temaRepository.AddRangeAsync(teme);
            return await GetAllTemeAsync();
        }

        public async Task<TemaDto?> UpdateTemaAsync(int id, UpdateTemaDto updateTemaDto)
        {
            var tema = new Tema
            {
                IdTema = id,
                Descriere = updateTemaDto.Descriere,
                TermenLimita = updateTemaDto.TermenLimita,
                IdMaterie = updateTemaDto.IdMaterie,
                IdClasa = updateTemaDto.IdClasa
            };

            var updatedTema = await _temaRepository.UpdateAsync(tema);
            if (updatedTema == null)
            {
                return null;
            }

            var result = await _temaRepository.GetByIdAsync(updatedTema.IdTema);
            return result != null ? MapToDto(result) : null;
        }

        public async Task<bool> DeleteTemaAsync(int id)
        {
            return await _temaRepository.DeleteAsync(id);
        }

        public async Task<TemaDto?> UpdateTermenLimitaAsync(int id, UpdateTermenLimitaDto updateTermenLimitaDto)
        {
            var updatedTema = await _temaRepository.UpdateTermenLimitaAsync(id, updateTermenLimitaDto.TermenLimita);
            if (updatedTema == null)
            {
                return null;
            }

            var result = await _temaRepository.GetByIdAsync(updatedTema.IdTema);
            return result != null ? MapToDto(result) : null;
        }

        private static TemaDto MapToDto(Tema tema)
        {
            return new TemaDto
            {
                IdTema = tema.IdTema,
                Descriere = tema.Descriere,
                TermenLimita = tema.TermenLimita,
                IdMaterie = tema.IdMaterie,
                NumeMaterie = tema.Materie?.NumeMaterie,
                IdClasa = tema.IdClasa,
                NumeClasa = tema.Clasa?.NumeClasa
            };
        }
    }
}
