using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolCatalog.Server.Data;
using SchoolCatalog.Server.Model;
using Microsoft.AspNetCore.JsonPatch;

namespace SchoolCatalog.Server.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaController : ControllerBase
    {
        private readonly SchoolCatalogContext _context;

        public MediaController(SchoolCatalogContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Media>>> GetMedii()
        {
            var medii = await _context.Medii
                .Include(m => m.Elev)
                .Include(m => m.Materie)
                .ToListAsync();

            if (!medii.Any())
                return NotFound(new { message = "Nu s-au găsit medii." });

            return Ok(medii);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Media>> GetMedieById(int id)
        {
            var medie = await _context.Medii
                .Include(m => m.Elev)
                .Include(m => m.Materie)
                .FirstOrDefaultAsync(m => m.IdMedie == id);

            if (medie == null)
                return NotFound(new { message = $"Media cu ID-ul {id} nu a fost găsită." });

            return Ok(medie);
        }

        [HttpPost]
        public async Task<ActionResult<Media>> CreateMedie([FromBody] Media medie)
        {
            if (medie == null)
                return BadRequest(new { message = "Media nu poate fi null." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Medii.Add(medie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMedieById), new { id = medie.IdMedie }, medie);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMedie(int id, [FromBody] Media medie)
        {
            if (medie == null || medie.IdMedie != id)
                return BadRequest(new { message = "Media nu este validă." });

            var existingMedie = await _context.Medii.FindAsync(id);
            if (existingMedie == null)
                return NotFound(new { message = $"Media cu ID-ul {id} nu a fost găsită." });

            existingMedie.IdElev = medie.IdElev;
            existingMedie.IdMaterie = medie.IdMaterie;
            existingMedie.Valoare = medie.Valoare;

            _context.Medii.Update(existingMedie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMedie(int id)
        {
            var medie = await _context.Medii.FindAsync(id);
            if (medie == null)
                return NotFound(new { message = $"Media cu ID-ul {id} nu a fost găsită." });

            _context.Medii.Remove(medie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PatchMedie(int id, [FromBody] JsonPatchDocument<Media> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest(new { message = "Documentul de patch nu poate fi null." });

            var medie = await _context.Medii.FindAsync(id);
            if (medie == null)
                return NotFound(new { message = $"Media cu ID-ul {id} nu a fost găsită." });

            patchDoc.ApplyTo(medie, error =>
            {
                ModelState.AddModelError(error.Operation?.path ?? string.Empty, error.ErrorMessage);
            });

            TryValidateModel(medie);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Medii.Update(medie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("elev/{elevId:int}")]
        public async Task<ActionResult<IEnumerable<Media>>> GetMediiByElev(int elevId)
        {
            var medii = await _context.Medii
                .Where(m => m.IdElev == elevId)
                .Include(m => m.Materie)
                .ToListAsync();

            if (!medii.Any())
                return NotFound(new { message = $"Nu s-au găsit medii pentru elevul cu ID-ul {elevId}." });

            return Ok(medii);
        }

        [HttpGet("materie/{materieId:int}")]
        public async Task<ActionResult<IEnumerable<Media>>> GetMediiByMaterie(int materieId)
        {
            var medii = await _context.Medii
                .Where(m => m.IdMaterie == materieId)
                .Include(m => m.Elev)
                .ToListAsync();

            if (!medii.Any())
                return NotFound(new { message = $"Nu s-au găsit medii pentru materia cu ID-ul {materieId}." });

            return Ok(medii);
        }
    }
}
