using Microsoft.AspNetCore.Mvc;
using SchoolCatalog.Server.Data;
using SchoolCatalog.Server.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
namespace SchoolCatalog.Server.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrarController : ControllerBase
    {
        private readonly SchoolCatalogContext _context;
        public OrarController(SchoolCatalogContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orar>>> GetOrar()
        {
            var orar = await _context.Orare
                .Include(o => o.Clasa)
                .ToListAsync();
            if (orar == null || !orar.Any())
            {
                return NotFound("Nu s-au găsit orare.");
            }
            return Ok(orar);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Orar>> GetOrarById(int id)
        {
            var orar = await _context.Orare
                .Include(o => o.Clasa)
                .FirstOrDefaultAsync(o => o.IdOrar == id);
            if (orar == null)
            {
                return NotFound($"Orarul cu ID-ul {id} nu a fost găsită.");
            }
            return Ok(orar);
        }
        [HttpPost]
        public async Task<ActionResult<Orar>> CreateOrar([FromBody] Orar orar)
        {
            if (orar == null)
            {
                return BadRequest("Orarul nu poate fi null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Orare.Add(orar);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrarById), new { id = orar.IdOrar }, orar);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOrar(int id, [FromBody] Orar orar)
        {
            if (orar == null || id != orar.IdOrar)
            {
                return BadRequest("Orarul nu poate fi null sau ID-ul nu se potrivește.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Entry(orar).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrarExists(id))
                {
                    return NotFound($"Orarul cu ID-ul {id} nu a fost găsit.");
                }
                throw;
            }
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrar(int id)
        {
            var orar = await _context.Orare.FindAsync(id);
            if (orar == null)
            {
                return NotFound($"Orarul cu ID-ul {id} nu a fost găsit.");
            }
            _context.Orare.Remove(orar);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool OrarExists(int id)
        {
            return _context.Orare.Any(o => o.IdOrar == id);
        }
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PatchOrar(int id, [FromBody] JsonPatchDocument<Orar> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Patch document cannot be null.");
            }
            var orar = await _context.Orare.FindAsync(id);
            if (orar == null)
            {
                return NotFound($"Orarul cu ID-ul {id} nu a fost găsit.");
            }
            patchDoc.ApplyTo(orar, error =>
            {
                ModelState.AddModelError(string.Empty, error.ErrorMessage);
            });
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Entry(orar).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("clasa/{clasaId:int}")]
        public async Task<ActionResult<IEnumerable<Orar>>> GetOrarByClasa(int clasaId)
        {
            var orare = await _context.Orare
                .Where(o => o.IdClasa == clasaId)
                .Include(o => o.Clasa)
                .ToListAsync();
            if (orare == null || !orare.Any())
            {
                return NotFound($"Nu s-au găsit orare pentru clasa cu ID-ul {clasaId}.");
            }
            return Ok(orare);
        }

    }
}
