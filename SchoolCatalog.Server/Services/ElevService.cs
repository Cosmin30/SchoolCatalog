using SchoolCatalog.Server.Dtos;
using SchoolCatalog.Server.Model;
using SchoolCatalog.Server.Repositories;

namespace SchoolCatalog.Server.Services
{
    public class ElevService : IElevService
    {
        private readonly IElevRepository _elevRepository;

        public ElevService(IElevRepository elevRepository)
        {
            _elevRepository = elevRepository;
        }

        public async Task<IEnumerable<ElevDto>> GetAllEleviAsync()
        {
            var elevi = await _elevRepository.GetAllAsync();
            return elevi.Select(MapToDto);
        }

        public async Task<ElevDto?> GetElevByIdAsync(int id)
        {
            var elev = await _elevRepository.GetByIdAsync(id);
            return elev != null ? MapToDto(elev) : null;
        }

        public async Task<IEnumerable<ElevDto>> GetEleviByClasaAsync(int idClasa)
        {
            var elevi = await _elevRepository.GetByClasaAsync(idClasa);
            return elevi.Select(MapToDto);
        }

        public async Task<ElevDto> CreateElevAsync(CreateElevDto createElevDto)
        {
            var elev = new Elev
            {
                NumeElev = createElevDto.NumeElev,
                PrenumeElev = createElevDto.PrenumeElev,
                DataNasterii = createElevDto.DataNasterii,
                ClasaId = createElevDto.ClasaId
            };

            var createdElev = await _elevRepository.AddAsync(elev);
            var result = await _elevRepository.GetByIdAsync(createdElev.IdElev);
            return MapToDto(result!);
        }

        public async Task<ElevDto?> UpdateElevAsync(int id, UpdateElevDto updateElevDto)
        {
            var elev = new Elev
            {
                IdElev = id,
                NumeElev = updateElevDto.NumeElev,
                PrenumeElev = updateElevDto.PrenumeElev,
                DataNasterii = updateElevDto.DataNasterii,
                ClasaId = updateElevDto.ClasaId
            };

            var updatedElev = await _elevRepository.UpdateAsync(elev);
            if (updatedElev == null)
            {
                return null;
            }

            var result = await _elevRepository.GetByIdAsync(updatedElev.IdElev);
            return result != null ? MapToDto(result) : null;
        }

        public async Task<bool> DeleteElevAsync(int id)
        {
            return await _elevRepository.DeleteAsync(id);
        }

        private static ElevDto MapToDto(Elev elev)
        {
            return new ElevDto
            {
                IdElev = elev.IdElev,
                NumeElev = elev.NumeElev,
                PrenumeElev = elev.PrenumeElev,
                DataNasterii = elev.DataNasterii,
                ClasaId = elev.ClasaId,
                NumeClasa = elev.Clasa?.NumeClasa,
                ProfilClasa = elev.Clasa?.ProfilClasa
            };
        }
    }
}
