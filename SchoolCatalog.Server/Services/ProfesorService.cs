using SchoolCatalog.Server.Dtos;
using SchoolCatalog.Server.Model;
using SchoolCatalog.Server.Repositories;

namespace SchoolCatalog.Server.Services
{
    public class ProfesorService : IProfesorService
    {
        private readonly IProfesorRepository _profesorRepository;

        public ProfesorService(IProfesorRepository profesorRepository)
        {
            _profesorRepository = profesorRepository;
        }

        public async Task<IEnumerable<ProfesorDto>> GetAllProfesoriAsync()
        {
            var profesori = await _profesorRepository.GetAllAsync();
            return profesori.Select(MapToDto);
        }

        public async Task<ProfesorDto?> GetProfesorByIdAsync(int id)
        {
            var profesor = await _profesorRepository.GetByIdAsync(id);
            return profesor != null ? MapToDto(profesor) : null;
        }

        public async Task<ProfesorDto> CreateProfesorAsync(CreateProfesorDto createProfesorDto)
        {
            var profesor = new Profesor
            {
                NumeProfesor = createProfesorDto.NumeProfesor,
                PrenumeProfesor = createProfesorDto.PrenumeProfesor,
                EmailProfesor = createProfesorDto.EmailProfesor,
                DataNasterii = createProfesorDto.DataNasterii
            };

            var createdProfesor = await _profesorRepository.AddAsync(profesor);
            var result = await _profesorRepository.GetByIdAsync(createdProfesor.IdProfesor);
            return MapToDto(result!);
        }

        public async Task<ProfesorDto?> UpdateProfesorAsync(int id, UpdateProfesorDto updateProfesorDto)
        {
            var profesor = new Profesor
            {
                IdProfesor = id,
                NumeProfesor = updateProfesorDto.NumeProfesor,
                PrenumeProfesor = updateProfesorDto.PrenumeProfesor,
                EmailProfesor = updateProfesorDto.EmailProfesor,
                DataNasterii = updateProfesorDto.DataNasterii
            };

            var updatedProfesor = await _profesorRepository.UpdateAsync(profesor);
            if (updatedProfesor == null)
            {
                return null;
            }

            var result = await _profesorRepository.GetByIdAsync(updatedProfesor.IdProfesor);
            return result != null ? MapToDto(result) : null;
        }

        public async Task<bool> DeleteProfesorAsync(int id)
        {
            return await _profesorRepository.DeleteAsync(id);
        }

        private static ProfesorDto MapToDto(Profesor profesor)
        {
            return new ProfesorDto
            {
                IdProfesor = profesor.IdProfesor,
                NumeProfesor = profesor.NumeProfesor,
                PrenumeProfesor = profesor.PrenumeProfesor,
                EmailProfesor = profesor.EmailProfesor,
                DataNasterii = profesor.DataNasterii
            };
        }
    }
}

