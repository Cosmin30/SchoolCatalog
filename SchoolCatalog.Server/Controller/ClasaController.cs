using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolCatalog.Server.Data;
using SchoolCatalog.Server.Model;
using Microsoft.AspNetCore.JsonPatch;

namespace SchoolCatalog.Server.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClasaController : ControllerBase
    {
        private readonly SchoolCatalogContext _context;
        public ClasaController(SchoolCatalogContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clasa>>> GetClase()
        {
            var clase = await _context.Clase
                .Include(c => c.Elevi)
                .Include(c => c.Materii)
                .Include(c => c.Profesori)
                .ToListAsync();
            if (clase == null || !clase.Any())
            {
                return NotFound("Nu s-au găsit clase.");
            }
            return Ok(clase);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Clasa>> GetClasaById(int id)
        {
            var clasa = await _context.Clase
                .Include(c => c.Elevi)
                .Include(c => c.Materii)
                .Include(c => c.Profesori)
                .FirstOrDefaultAsync(c => c.IdClasa == id);
            if (clasa == null)
            {
                return NotFound($"Clasa cu ID-ul {id} nu a fost găsită.");
            }
            return Ok(clasa);
        }
        [HttpPost]
        public async Task<ActionResult<Clasa>> CreateClasa([FromBody] Clasa clasa)
        {
            if (clasa == null)
            {
                return BadRequest("Clasa nu poate fi null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Clase.Add(clasa);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetClasaById), new { id = clasa.IdClasa }, clasa);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Clasa>> UpdateClasa(int id, [FromBody] Clasa clasa)
        {
            if (clasa == null || clasa.IdClasa != id)
            {
                return BadRequest("Clasa nu poate fi null sau ID-ul nu se potrivește.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingClasa = await _context.Clase.FindAsync(id);
            if (existingClasa == null)
            {
                return NotFound($"Clasa cu ID-ul {id} nu a fost găsită.");
            }
            existingClasa.NumeClasa = clasa.NumeClasa;
            existingClasa.ProfilClasa = clasa.ProfilClasa;

            await _context.SaveChangesAsync();
            return Ok(existingClasa);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteClasa(int id)
        {
            var clasa = await _context.Clase.FindAsync(id);
            if (clasa == null)
            {
                return NotFound($"Clasa cu ID-ul {id} nu a fost găsită.");
            }
            _context.Clase.Remove(clasa);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Clasa>>> SearchClase([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Căutarea nu poate fi goală.");
            }
            var clase = await _context.Clase
                .Where(c => c.NumeClasa.Contains(query) || c.ProfilClasa.Contains(query))
                .Include(c => c.Elevi)
                .Include(c => c.Materii)
                .Include(c => c.Profesori)
                .ToListAsync();
            if (clase == null || !clase.Any())
            {
                return NotFound($"Nu s-au găsit clase pentru căutarea '{query}'.");
            }
            return Ok(clase);
        }
        [HttpGet("elevi/{clasaId:int}")]
        public async Task<ActionResult<IEnumerable<Elev>>> GetEleviByClasa(int clasaId)
        {
            var elevi = await _context.Elevi
                .Where(e => e.ClasaId == clasaId)
                .Include(e => e.Clasa)
                .ToListAsync();
            if (elevi == null || !elevi.Any())
            {
                return NotFound($"Nu s-au găsit elevi în clasa cu ID-ul {clasaId}.");
            }
            return Ok(elevi);
        }
        [HttpGet("materii/{clasaId:int}")]
        public async Task<ActionResult<IEnumerable<Materie>>> GetMateriiByClasa(int clasaId)
        {
            var materii = await _context.Materii
                .Where(m => m.Clase.Any(c => c.IdClasa == clasaId))
                .Include(m => m.Clase)
                .ToListAsync();
            if (materii == null || !materii.Any())
            {
                return NotFound($"Nu s-au găsit materii pentru clasa cu ID-ul {clasaId}.");
            }
            return Ok(materii);
        }
        [HttpGet("profesori/{clasaId:int}")]
        public ActionResult<IEnumerable<Profesor>> GetProfesoriByClasa(int clasaId)
        {
            var profesori = _context.Profesori
                .Where(p => p.Clase.Any(c => c.IdClasa == clasaId))
                .Include(p => p.Clase)
                .ToList();
            if (profesori == null || !profesori.Any())
            {
                return NotFound($"Nu s-au găsit profesori pentru clasa cu ID-ul {clasaId}.");
            }
            return Ok(profesori);
        }
        [HttpGet("orar/{clasaId:int}")]
        public async Task<ActionResult<Orar>> GetOrarByClasa(int clasaId)
        {
            var orar = await _context.Orare
                .Include(o => o.Clasa)
                .FirstOrDefaultAsync(o => o.IdClasa == clasaId);
            if (orar == null)
            {
                return NotFound($"Nu s-a găsit orarul pentru clasa cu ID-ul {clasaId}.");
            }
            return Ok(orar);

        }
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PatchClasa(int id, [FromBody] JsonPatchDocument<Clasa> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Documentul de patch nu poate fi null.");
            }
            var clasa = await _context.Clase.FindAsync(id);
            if (clasa == null)
            {
                return NotFound($"Clasa cu ID-ul {id} nu a fost găsită.");
            }
            patchDoc.ApplyTo(clasa, error =>
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
    }
    }
