using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolCatalog.Server.Data;
using SchoolCatalog.Server.Model;
using Microsoft.AspNetCore.JsonPatch;
namespace SchoolCatalog.Server.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrarItemController : ControllerBase
    {
        private readonly SchoolCatalogContext _context;
        public OrarItemController(SchoolCatalogContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrarItem>>> GetOrarItems()
        {
            var orarItems = await _context.OrarItems
                .Include(oi => oi.Orar)
                .Include(oi => oi.Materie)
                .Include(oi => oi.Profesor)
                .ToListAsync();
            if (!orarItems.Any())
                return NotFound("Nu s-au găsit elemente de orar.");
            return Ok(orarItems);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrarItem>> GetOrarItemById(int id)
        {
            var orarItem = await _context.OrarItems
                .Include(oi => oi.Orar)
                .Include(oi => oi.Materie)
                .Include(oi => oi.Profesor)
                .FirstOrDefaultAsync(oi => oi.IdOrarItem == id);
            if (orarItem == null)
                return NotFound($"Elementul de orar cu ID-ul {id} nu a fost găsit.");
            return Ok(orarItem);
        }
        [HttpPost]
        public async Task<ActionResult<OrarItem>> CreateOrarItem([FromBody] OrarItem orarItem)
        {
            if (orarItem == null)
            {
                return BadRequest("Elementul de orar nu poate fi null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.OrarItems.Add(orarItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrarItemById), new { id = orarItem.IdOrarItem }, orarItem);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOrarItem(int id, [FromBody] OrarItem orarItem)
        {
            if (orarItem == null || orarItem.IdOrarItem != id)
            {
                return BadRequest("Elementul de orar nu este valid.");
            }
            var existingOrarItem = await _context.OrarItems.FindAsync(id);
            if (existingOrarItem == null)
            {
                return NotFound($"Elementul de orar cu ID-ul {id} nu a fost găsit.");
            }
            existingOrarItem.IdOrar = orarItem.IdOrar;
            existingOrarItem.IdMaterie = orarItem.IdMaterie;
            existingOrarItem.IdProfesor = orarItem.IdProfesor;
            existingOrarItem.ZiSaptamana = orarItem.ZiSaptamana;
            existingOrarItem.OraInceput = orarItem.OraInceput;
            existingOrarItem.OraSfarsit = orarItem.OraSfarsit;
            _context.OrarItems.Update(existingOrarItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrarItem(int id)
        {
            var orarItem = await _context.OrarItems.FindAsync(id);
            if (orarItem == null)
            {
                return NotFound($"Elementul de orar cu ID-ul {id} nu a fost găsit.");
            }
            _context.OrarItems.Remove(orarItem);
            await _context.SaveChangesAsync();
            return NoContent();

        }
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PatchOrarItem(int id, [FromBody] JsonPatchDocument<OrarItem> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Documentul de patch nu poate fi null.");
            }
            var orarItem = await _context.OrarItems.FindAsync(id);
            if (orarItem == null)
            {
                return NotFound($"Elementul de orar cu ID-ul {id} nu a fost găsit.");
            }
            patchDoc.ApplyTo(orarItem, error =>
            {
                ModelState.AddModelError(string.Empty, error.ErrorMessage);
            });
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.OrarItems.Update(orarItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("orar/{orarId:int}")]
        public async Task<ActionResult<IEnumerable<OrarItem>>> GetOrarItemsByOrarId(int orarId)
        {
            var orarItems = await _context.OrarItems
                .Where(oi => oi.IdOrar == orarId)
                .Include(oi => oi.Materie)
                .Include(oi => oi.Profesor)
                .ToListAsync();
            if (!orarItems.Any())
                return NotFound($"Nu s-au găsit elemente de orar pentru ID-ul {orarId}.");
            return Ok(orarItems);
        }

    }
    }
