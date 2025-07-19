using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolCatalog.Server.Data;
using SchoolCatalog.Server.Model;
using System.Security.Claims;
namespace SchoolCatalog.Server.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotaController : ControllerBase
    {
        private readonly SchoolCatalogContext _context;
        public NotaController(SchoolCatalogContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nota>>> GetNote()
        {
            var note = await _context.Note
                .Include(n => n.Elev)
                .Include(n => n.Materie)
                .ToListAsync();
            if (note == null || !note.Any())
            {
                return NotFound("Nu s-au găsit note.");
            }
            return Ok(note);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Nota>> GetNotaById(int id)
        {
            var nota = await _context.Note
                .Include(n => n.Elev)
                .Include(n => n.Materie)
                .FirstOrDefaultAsync(n => n.IdNota == id);
            if (nota == null)
            {
                return NotFound($"Nota cu ID-ul {id} nu a fost găsită.");
            }
            return Ok(nota);
        }
        [HttpPost]
        public async Task<ActionResult<Nota>> CreateNota([FromBody] Nota nota)
        {
            if (nota == null)
            {
                return BadRequest("Nota nu poate fi null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Note.Add(nota);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetNotaById), new { id = nota.IdNota }, nota);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateNota(int id, [FromBody] Nota nota)
        {
            if (nota == null || nota.IdNota != id)
            {
                return BadRequest("Nota nu este validă.");
            }
            var existingNota = await _context.Note.FindAsync(id);
            if (existingNota == null)
            {
                return NotFound($"Nota cu ID-ul {id} nu a fost găsită.");
            }
            existingNota.Valoare = nota.Valoare;
            existingNota.DataNotei = nota.DataNotei;
            existingNota.IdElev = nota.IdElev;
            existingNota.IdMaterie = nota.IdMaterie;
            _context.Note.Update(existingNota);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteNota(int id)
        {
            var nota = await _context.Note.FindAsync(id);
            if (nota == null)
            {
                return NotFound($"Nota cu ID-ul {id} nu a fost găsită.");
            }
            _context.Note.Remove(nota);
            await _context.SaveChangesAsync();
            return NoContent();


        }
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PatchNota(int id, [FromBody] JsonPatchDocument<Nota> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Patch document nu poate fi null.");
            }
            var nota = await _context.Note.FindAsync(id);
            if (nota == null)
            {
                return NotFound($"Nota cu ID-ul {id} nu a fost găsită.");
            }
            patchDoc.ApplyTo(nota, error =>
            {
                ModelState.AddModelError(string.Empty, error.ErrorMessage);
            });
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Note.Update(nota);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("elev/{elevId:int}")]
        public async Task<ActionResult<IEnumerable<Nota>>> GetNoteByElev(int elevId)
        {
            var note = await _context.Note
                .Where(n => n.IdElev == elevId)
                .Include(n => n.Materie)
                .ToListAsync();
            if (note == null || !note.Any())
            {
                return NotFound($"Nu s-au găsit note pentru elevul cu ID-ul {elevId}.");
            }
            return Ok(note);

        }
        [HttpGet("materie/{materieId:int}")]
        public async Task<ActionResult<IEnumerable<Nota>>> GetNoteByMaterie(int materieId)
        {
            var note = await _context.Note
                .Where(n => n.IdMaterie == materieId)
                .Include(n => n.Elev)
                .ToListAsync();
            if (note == null || !note.Any())
            {
                return NotFound($"Nu s-au găsit note pentru materia cu ID-ul {materieId}.");
            }
            return Ok(note);
        }
        [HttpGet("elev/{elevId:int}/materie/{materieId:int}")]
        public ActionResult<IEnumerable<Nota>> GetNoteByElevAndMaterie(int elevId, int materieId)
        {
            var note = _context.Note
                .Where(n => n.IdElev == elevId && n.IdMaterie == materieId)
                .Include(n => n.Elev)
                .Include(n => n.Materie)
                .ToList();
            if (note == null || !note.Any())
            {
                return NotFound($"Nu s-au găsit note pentru elevul cu ID-ul {elevId} și materia cu ID-ul {materieId}.");
            }
            return Ok(note);
        }

    }
    }
