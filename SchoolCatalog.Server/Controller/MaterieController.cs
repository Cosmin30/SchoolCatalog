using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolCatalog.Server.Data;
using SchoolCatalog.Server.Model;
using Microsoft.AspNetCore.JsonPatch;
namespace SchoolCatalog.Server.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterieController : ControllerBase
    {
        private readonly SchoolCatalogContext _context;
        private readonly ILogger<MaterieController> _logger;
        
        public MaterieController(SchoolCatalogContext context, ILogger<MaterieController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Materie>>> GetMaterii()
        {
            try
            {
                var materii = await _context.Materii.Include(m => m.Profesor).ToListAsync();
                if (!materii.Any())
                    return NotFound("Nu s-au găsit materii.");

                return Ok(materii);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving materii");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Materie>> GetMaterieById(int id)
        {
            var materie = await _context.Materii.Include(m => m.Profesor).FirstOrDefaultAsync(m => m.IdMaterie == id);
            if (materie == null)
                return NotFound($"Materia cu ID-ul {id} nu a fost găsită.");

            return Ok(materie);
        }

        [HttpPost]
        public async Task<ActionResult<Materie>> CreateMaterie([FromBody] Materie materie)
        {
            if (materie == null)
                return BadRequest("Materia nu poate fi null.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Materii.Add(materie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMaterieById), new { id = materie.IdMaterie }, materie);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMaterie(int id, [FromBody] Materie materie)
        {
            if (materie == null || materie.IdMaterie != id)
                return BadRequest("Materia nu este validă.");

            var existingMaterie = await _context.Materii.FindAsync(id);
            if (existingMaterie == null)
                return NotFound($"Materia cu ID-ul {id} nu a fost găsită.");

            existingMaterie.NumeMaterie = materie.NumeMaterie;
            existingMaterie.ProfesorId = materie.ProfesorId;

            _context.Materii.Update(existingMaterie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMaterie(int id)
        {
            var materie = await _context.Materii.FindAsync(id);
            if (materie == null)
                return NotFound($"Materia cu ID-ul {id} nu a fost găsită.");

            _context.Materii.Remove(materie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PatchMaterie(int id, [FromBody] JsonPatchDocument<Materie> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest("Documentul de patch nu poate fi null.");

            var materie = await _context.Materii.FindAsync(id);
            if (materie == null)
                return NotFound($"Materia cu ID-ul {id} nu a fost găsită.");

            patchDoc.ApplyTo(materie, error =>
            {
                ModelState.AddModelError(error.Operation?.path ?? string.Empty, error.ErrorMessage);
            });

            TryValidateModel(materie);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Materii.Update(materie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("search")]
        public async Task<ActionResult<IEnumerable<Materie>>> SearchMaterii([FromBody] string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return BadRequest("Termenul de căutare nu poate fi gol.");

            var materii = await _context.Materii
                .Where(m => m.NumeMaterie.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .Include(m => m.Profesor)
                .ToListAsync();

            if (!materii.Any())
                return NotFound($"Nu s-au găsit materii pentru termenul '{searchTerm}'.");

            return Ok(materii);
        }

        [HttpGet("clasa/{clasaId:int}")]
        public async Task<ActionResult<IEnumerable<Materie>>> GetMateriiByClasa(int clasaId)
        {
            var materii = await _context.Materii
                .Where(m => m.Clase.Any(c => c.IdClasa == clasaId))
                .Include(m => m.Clase)
                .ToListAsync();

            if (!materii.Any())
                return NotFound($"Nu s-au găsit materii pentru clasa cu ID-ul {clasaId}.");

            return Ok(materii);
        }

        [HttpGet("profesori/{clasaId:int}")]
        public async Task<ActionResult<IEnumerable<Profesor>>> GetProfesoriByClasa(int clasaId)
        {
            var profesori = await _context.Profesori
                .Where(p => p.Clase.Any(c => c.IdClasa == clasaId))
                .Include(p => p.Clase)
                .ToListAsync();

            if (!profesori.Any())
                return NotFound($"Nu s-au găsit profesori pentru clasa cu ID-ul {clasaId}.");

            return Ok(profesori);
        }

        [HttpGet("orar/{clasaId:int}")]
        public async Task<ActionResult<Orar>> GetOrarByClasa(int clasaId)
        {
            var orar = await _context.Orare
                .Include(o => o.Clasa)
                .FirstOrDefaultAsync(o => o.IdClasa == clasaId);

            if (orar == null)
                return NotFound($"Nu s-a găsit orarul pentru clasa cu ID-ul {clasaId}.");

            return Ok(orar);
        }
    }

}
    
