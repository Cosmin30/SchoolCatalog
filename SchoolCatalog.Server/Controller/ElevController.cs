using Microsoft.AspNetCore.Mvc;
using SchoolCatalog.Server.Model;
using SchoolCatalog.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace SchoolCatalog.Server.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ElevController : ControllerBase
    {
        private readonly SchoolCatalogContext _context;
        private readonly ILogger<ElevController> _logger;
        
        public ElevController(SchoolCatalogContext context, ILogger<ElevController> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Elev>>> GetElevi()
        {
            try
            {
                var elevi = await _context.Elevi
                    .Include(e => e.Clasa)
                    .ToListAsync();
                if (elevi == null || !elevi.Any())
                {
                    return NotFound("Nu s-au găsit elevi.");
                }
                return Ok(elevi);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving elevi");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Elev>> GetElevById(int id)
        {
            var elev = await _context.Elevi
                .Include(e => e.Clasa)
                .FirstOrDefaultAsync(e => e.IdElev == id);
            if (elev == null)
            {
                return NotFound($"Elevul cu ID-ul {id} nu a fost găsit.");
            }
            return Ok(elev);
        }
        [HttpPost]
        public async Task<ActionResult<Elev>> CreateElev([FromBody] Elev elev)
        {
            if (elev == null)
            {
                return BadRequest("Elevul nu poate fi null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Elevi.Add(elev);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetElevById), new { id = elev.IdElev }, elev);
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Elev>> UpdateElev(int id, [FromBody] Elev elev)
        {
            if (elev == null || elev.IdElev != id)
            {
                return BadRequest("Elevul nu poate fi null sau ID-ul nu se potrivește.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingElev = await _context.Elevi.FindAsync(id);
            if (existingElev == null)
            {
                return NotFound($"Elevul cu ID-ul {id} nu a fost găsit.");
            }
            existingElev.NumeElev = elev.NumeElev;
            existingElev.PrenumeElev = elev.PrenumeElev;
            existingElev.DataNasterii = elev.DataNasterii;
            existingElev.ClasaId = elev.ClasaId;
            _context.Elevi.Update(existingElev);
            await _context.SaveChangesAsync();
            return Ok(existingElev);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteElev(int id)
        {
            var elev = await _context.Elevi.FindAsync(id);
            if (elev == null)
            {
                return NotFound($"Elevul cu ID-ul {id} nu a fost găsit.");
            }
            _context.Elevi.Remove(elev);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet]
        [Route("clasa/{clasaId:int}")]
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
        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<IEnumerable<Elev>>> SearchElevi([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Căutarea nu poate fi goală.");
            }
            var elevi = await _context.Elevi
                .Where(e => e.NumeElev.Contains(query) || e.PrenumeElev.Contains(query))
                .Include(e => e.Clasa)
                .ToListAsync();
            if (elevi == null || !elevi.Any())
            {
                return NotFound($"Nu s-au găsit elevi care să corespundă căutării '{query}'.");
            }
            return Ok(elevi);
        }
        [HttpPatch]
        [Route("{id:int}/update-clasa")]
        public async Task<ActionResult<Elev>> UpdateElevClasa(int id, [FromBody] int clasaId)
        {
            var elev = await _context.Elevi.FindAsync(id);
            if (elev == null)
            {
                return NotFound($"Elevul cu ID-ul {id} nu a fost găsit.");
            }
            var clasa = await _context.Clase.FindAsync(clasaId);
            if (clasa == null)
            {
                return NotFound($"Clasa cu ID-ul {clasaId} nu a fost găsită.");
            }
            elev.ClasaId = clasaId;
            _context.Elevi.Update(elev);
            await _context.SaveChangesAsync();
            return Ok(elev);
        }
        [HttpGet]
        [Route("count")]
        public async Task<ActionResult<int>> GetElevCount()
        {
            var count = await _context.Elevi.CountAsync();
            return Ok(count);
        }
        [HttpGet]
        [Route("count-by-clasa/{clasaId:int}")]
        public async Task<ActionResult<int>> GetElevCountByClasa(int clasaId)
        {
            var count = await _context.Elevi.CountAsync(e => e.ClasaId == clasaId);
            return Ok(count);
        }
       
    }
    }
