using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolCatalog.Server.Dtos;
using SchoolCatalog.Server.Services;

namespace SchoolCatalog.Server.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotaController : ControllerBase
    {
        private readonly INotaService _notaService;
        private readonly ILogger<NotaController> _logger;

        public NotaController(INotaService notaService, ILogger<NotaController> logger)
        {
            _notaService = notaService;
            _logger = logger;
        }

        // GET: api/nota - Accesibil pentru toți (fără autentificare pentru testare)
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<NotaDto>>> GetNote()
        {
            try
            {
                var note = await _notaService.GetAllNoteAsync();
                if (note == null || !note.Any())
                {
                    return NotFound("Nu s-au găsit note.");
                }
                return Ok(note);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving note");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/nota/{id} - Accesibil pentru toți utilizatorii autentificați
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<ActionResult<NotaDto>> GetNotaById(int id)
        {
            var nota = await _notaService.GetNotaByIdAsync(id);
            if (nota == null)
            {
                return NotFound($"Nota cu ID-ul {id} nu a fost găsită.");
            }
            return Ok(nota);
        }

        // GET: api/nota/elev/{idElev} - Note pentru un elev specific
        [HttpGet("elev/{idElev:int}")]
        public async Task<ActionResult<IEnumerable<NotaDto>>> GetNoteByElev(int idElev)
        {
            var note = await _notaService.GetNoteByElevAsync(idElev);
            if (note == null || !note.Any())
            {
                return NotFound($"Nu s-au găsit note pentru elevul cu ID-ul {idElev}.");
            }
            return Ok(note);
        }

        // GET: api/nota/materie/{idMaterie} - Note pentru o materie specifică
        [HttpGet("materie/{idMaterie:int}")]
        [Authorize(Roles = "profesor")]
        public async Task<ActionResult<IEnumerable<NotaDto>>> GetNoteByMaterie(int idMaterie)
        {
            var note = await _notaService.GetNoteByMaterieAsync(idMaterie);
            if (note == null || !note.Any())
            {
                return NotFound($"Nu s-au găsit note pentru materia cu ID-ul {idMaterie}.");
            }
            return Ok(note);
        }

        // GET: api/nota/elev/{idElev}/materie/{idMaterie}
        [HttpGet("elev/{idElev:int}/materie/{idMaterie:int}")]
        public async Task<ActionResult<IEnumerable<NotaDto>>> GetNoteByElevAndMaterie(int idElev, int idMaterie)
        {
            var note = await _notaService.GetNoteByElevAndMaterieAsync(idElev, idMaterie);
            if (note == null || !note.Any())
            {
                return NotFound($"Nu s-au găsit note pentru elevul și materia specificate.");
            }
            return Ok(note);
        }

        // POST: api/nota - Doar profesorii pot adăuga note
        [HttpPost]
        [Authorize(Roles = "profesor")]
        public async Task<ActionResult<NotaDto>> CreateNota([FromBody] CreateNotaDto createNotaDto)
        {
            if (createNotaDto == null)
            {
                return BadRequest("Nota nu poate fi null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nota = await _notaService.CreateNotaAsync(createNotaDto);
            return CreatedAtAction(nameof(GetNotaById), new { id = nota.IdNota }, nota);
        }

        // PUT: api/nota/{id} - Doar profesorii pot modifica note
        [HttpPut("{id:int}")]
        [Authorize(Roles = "profesor")]
        public async Task<ActionResult<NotaDto>> UpdateNota(int id, [FromBody] UpdateNotaDto updateNotaDto)
        {
            if (updateNotaDto == null)
            {
                return BadRequest("Nota nu poate fi null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nota = await _notaService.UpdateNotaAsync(id, updateNotaDto);
            if (nota == null)
            {
                return NotFound($"Nota cu ID-ul {id} nu a fost găsită.");
            }

            return Ok(nota);
        }

        // DELETE: api/nota/{id} - Doar profesorii pot șterge note
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "profesor")]
        public async Task<IActionResult> DeleteNota(int id)
        {
            var deleted = await _notaService.DeleteNotaAsync(id);
            if (!deleted)
            {
                return NotFound($"Nota cu ID-ul {id} nu a fost găsită.");
            }
            return NoContent();
        }

        // PATCH: api/nota/{id}/toggle-anulata - Doar profesorii pot anula/reactiva note
        [HttpPatch("{id:int}/toggle-anulata")]
        [Authorize(Roles = "profesor")]
        public async Task<IActionResult> ToggleAnulata(int id)
        {
            var success = await _notaService.ToggleAnulataAsync(id);
            if (!success)
            {
                return NotFound($"Nota cu ID-ul {id} nu a fost găsită.");
            }
            return Ok(new { message = "Starea notei a fost schimbată." });
        }
    }
}
