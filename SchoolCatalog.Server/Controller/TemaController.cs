using Microsoft.AspNetCore.Mvc;
using SchoolCatalog.Server.Dtos;
using SchoolCatalog.Server.Services;

namespace SchoolCatalog.Server.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemaController : ControllerBase
    {
        private readonly ITemaService _temaService;

        public TemaController(ITemaService temaService)
        {
            _temaService = temaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemaDto>>> GetTeme()
        {
            var teme = await _temaService.GetAllTemeAsync();
            if (teme == null || !teme.Any())
            {
                return NotFound("Nu s-au găsit teme.");
            }
            return Ok(teme);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TemaDto>> GetTemaById(int id)
        {
            var tema = await _temaService.GetTemaByIdAsync(id);
            if (tema == null)
            {
                return NotFound($"Tema cu ID-ul {id} nu a fost găsită.");
            }
            return Ok(tema);
        }

        [HttpPost]
        public async Task<ActionResult<TemaDto>> CreateTema([FromBody] CreateTemaDto createTemaDto)
        {
            if (createTemaDto == null)
            {
                return BadRequest("Tema nu poate fi null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tema = await _temaService.CreateTemaAsync(createTemaDto);
            return CreatedAtAction(nameof(GetTemaById), new { id = tema.IdTema }, tema);
        }

        [HttpPost]
        [Route("CreateTeme")]
        public async Task<ActionResult<IEnumerable<TemaDto>>> CreateTeme([FromBody] IEnumerable<CreateTemaDto> createTemeDtos)
        {
            if (createTemeDtos == null || !createTemeDtos.Any())
            {
                return BadRequest("Lista de teme nu poate fi null sau goală.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teme = await _temaService.CreateTemeAsync(createTemeDtos);
            return CreatedAtAction(nameof(GetTeme), teme);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TemaDto>> UpdateTema(int id, [FromBody] UpdateTemaDto updateTemaDto)
        {
            if (updateTemaDto == null)
            {
                return BadRequest("Tema nu poate fi null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tema = await _temaService.UpdateTemaAsync(id, updateTemaDto);
            if (tema == null)
            {
                return NotFound($"Tema cu ID-ul {id} nu a fost găsită.");
            }

            return Ok(tema);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteTema(int id)
        {
            var deleted = await _temaService.DeleteTemaAsync(id);
            if (!deleted)
            {
                return NotFound($"Tema cu ID-ul {id} nu a fost găsită.");
            }
            return NoContent();
        }

        [HttpGet("Clasa/{idClasa:int}")]
        public async Task<ActionResult<IEnumerable<TemaDto>>> GetTemeByClasa(int idClasa)
        {
            var teme = await _temaService.GetTemeByClasaAsync(idClasa);
            if (teme == null || !teme.Any())
            {
                return NotFound($"Nu s-au găsit teme pentru clasa cu ID-ul {idClasa}.");
            }
            return Ok(teme);
        }

        [HttpGet("Materie/{idMaterie:int}")]
        public async Task<ActionResult<IEnumerable<TemaDto>>> GetTemeByMaterie(int idMaterie)
        {
            var teme = await _temaService.GetTemeByMaterieAsync(idMaterie);
            if (teme == null || !teme.Any())
            {
                return NotFound($"Nu s-au găsit teme pentru materia cu ID-ul {idMaterie}.");
            }
            return Ok(teme);
        }

        [HttpGet("Clasa/{idClasa:int}/Materie/{idMaterie:int}")]
        public async Task<ActionResult<IEnumerable<TemaDto>>> GetTemeByClasaAndMaterie(int idClasa, int idMaterie)
        {
            var teme = await _temaService.GetTemeByClasaAndMaterieAsync(idClasa, idMaterie);
            if (teme == null || !teme.Any())
            {
                return NotFound($"Nu s-au găsit teme pentru clasa cu ID-ul {idClasa} și materia cu ID-ul {idMaterie}.");
            }
            return Ok(teme);
        }

        [HttpGet("Clasa/{idClasa:int}/Materie/{idMaterie:int}/TermenLimita")]
        public async Task<ActionResult<IEnumerable<TemaDto>>> GetTemeByClasaAndMaterieAndTermenLimita(int idClasa, int idMaterie, DateTime termenLimita)
        {
            var teme = await _temaService.GetTemeByClasaAndMaterieAndTermenLimitaAsync(idClasa, idMaterie, termenLimita);
            if (teme == null || !teme.Any())
            {
                return NotFound($"Nu s-au găsit teme pentru clasa cu ID-ul {idClasa}, materia cu ID-ul {idMaterie} și termenul limită {termenLimita.ToShortDateString()}.");
            }
            return Ok(teme);
        }

        [HttpPatch]
        [Route("{id:int}/TermenLimita")]
        public async Task<ActionResult<TemaDto>> UpdateTermenLimita(int id, [FromBody] UpdateTermenLimitaDto updateTermenLimitaDto)
        {
            if (updateTermenLimitaDto == null)
            {
                return BadRequest("Termenul limită nu poate fi null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tema = await _temaService.UpdateTermenLimitaAsync(id, updateTermenLimitaDto);
            if (tema == null)
            {
                return NotFound($"Tema cu ID-ul {id} nu a fost găsită.");
            }

            return Ok(tema);
        }
    }
}
