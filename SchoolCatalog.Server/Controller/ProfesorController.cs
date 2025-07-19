using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolCatalog.Server.Data;
using SchoolCatalog.Server.Model;
using Microsoft.AspNetCore.JsonPatch;

namespace SchoolCatalog.Server.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesorController : ControllerBase
    {
        private readonly SchoolCatalogContext _context;
        public ProfesorController(SchoolCatalogContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesor>>> GetProfesori()
        {
            var profesori = await _context.Profesori
                .Include(p => p.Clase)
                .ToListAsync();
            if (!profesori.Any())
            {
                return NotFound(new { message = "Nu s-au găsit profesori." });
            }
            return Ok(profesori);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Profesor>> GetProfesorById(int id)
        {
            var profesor = await _context.Profesori
                .Include(p => p.Clase)
                .FirstOrDefaultAsync(p => p.IdProfesor == id);
            if (profesor == null)
            {
                return NotFound(new { message = $"Profesorul cu ID-ul {id} nu a fost găsit." });
            }
            return Ok(profesor);
        }
        [HttpPost]
        public async Task<ActionResult<Profesor>> CreateProfesor([FromBody] Profesor profesor)
        {
            if (profesor == null)
            {
                return BadRequest(new { message = "Profesorul nu poate fi null." });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Profesori.Add(profesor);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProfesorById), new { id = profesor.IdProfesor }, profesor);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProfesor(int id, [FromBody] Profesor profesor)
        {
            if (profesor == null || profesor.IdProfesor != id)
            {
                return BadRequest(new { message = "Profesorul nu este valid." });
            }
            var existingProfesor = await _context.Profesori.FindAsync(id);
            if (existingProfesor == null)
            {
                return NotFound(new { message = $"Profesorul cu ID-ul {id} nu a fost găsit." });
            }
            existingProfesor.NumeProfesor = profesor.NumeProfesor;
            existingProfesor.PrenumeProfesor = profesor.PrenumeProfesor;
            existingProfesor.EmailProfesor = profesor.EmailProfesor;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProfesor(int id)
        {
            var profesor = await _context.Profesori.FindAsync(id);
            if (profesor == null)
            {
                return NotFound(new { message = $"Profesorul cu ID-ul {id} nu a fost găsit." });
            }
            _context.Profesori.Remove(profesor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PatchProfesor(int id, [FromBody] JsonPatchDocument<Profesor> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest(new { message = "Patch document nu poate fi null." });
            }
            var profesor = await _context.Profesori.FindAsync(id);
            if (profesor == null)
            {
                return NotFound(new { message = $"Profesorul cu ID-ul {id} nu a fost găsit." });
            }
            patchDoc.ApplyTo(profesor, error =>
            {
                ModelState.AddModelError(string.Empty, error.ErrorMessage);
            });
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Profesor>>> SearchProfesori([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest(new { message = "Căutarea nu poate fi goală." });
            }
            var profesori = await _context.Profesori
                .Where(p => p.NumeProfesor.Contains(query) || p.PrenumeProfesor.Contains(query))
                .Include(p => p.Clase)
                .ToListAsync();
            if (!profesori.Any())
            {
                return NotFound(new { message = "Nu s-au găsit profesori care să corespundă criteriilor de căutare." });
            }
            return Ok(profesori);
        }
        [HttpGet("clasa/{clasaId:int}")]
        public ActionResult<IEnumerable<Profesor>> GetProfesoriByClasa(int clasaId)
        {
            var profesori = _context.Profesori
                .Where(p => p.Clase.Any(c => c.IdClasa == clasaId))
                .Include(p => p.Clase)
                .ToList();
            if (!profesori.Any())
            {
                return NotFound(new { message = $"Nu s-au găsit profesori pentru clasa cu ID-ul {clasaId}." });
            }
            return Ok(profesori);

        }
    }
}
