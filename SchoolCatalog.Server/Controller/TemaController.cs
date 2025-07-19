using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolCatalog.Server.Data;
using SchoolCatalog.Server.Model;

namespace SchoolCatalog.Server.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemaController : ControllerBase
    {
        private readonly SchoolCatalogContext _context;
        public TemaController(SchoolCatalogContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tema>>> GetTeme()
        {
            var teme = await _context.Teme
                .Include(t => t.Materie)
                .ToListAsync();
            if (teme == null || !teme.Any())
            {
                return NotFound("Nu s-au găsit teme.");
            }
            return Ok(teme);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Tema>> GetTemaById(int id)
        {
            var tema = await _context.Teme
                .Include(t => t.Materie)
                .FirstOrDefaultAsync(t => t.IdTema == id);
            if (tema == null)
            {
                return NotFound($"Tema cu ID-ul {id} nu a fost găsită.");
            }
            return Ok(tema);
        }
        [HttpPost]
        public async Task<ActionResult<Tema>> CreateTema([FromBody] Tema tema)
        {
            if (tema == null)
            {
                return BadRequest("Tema nu poate fi null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Teme.Add(tema);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTemaById), new { id = tema.IdTema }, tema);
        }
        [HttpPost]
        [Route("CreateTeme")]
        public async Task<ActionResult<IEnumerable<Tema>>> CreateTeme([FromBody] IEnumerable<Tema> teme)
        {
            if (teme == null || !teme.Any())
            {
                return BadRequest("Lista de teme nu poate fi null sau goală.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Teme.AddRange(teme);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTeme), teme);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Tema>> UpdateTema(int id, [FromBody] Tema tema)
        {
            if (tema == null || tema.IdTema != id)
            {
                return BadRequest("Tema nu poate fi null sau ID-ul nu se potrivește.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingTema = await _context.Teme.FindAsync(id);
            if (existingTema == null)
            {
                return NotFound($"Tema cu ID-ul {id} nu a fost găsită.");
            }
            existingTema.Descriere = tema.Descriere;
            existingTema.TermenLimita = tema.TermenLimita;
            existingTema.IdMaterie = tema.IdMaterie;
            existingTema.IdClasa = tema.IdClasa;
            _context.Teme.Update(existingTema);
            await _context.SaveChangesAsync();
            return Ok(existingTema);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteTema(int id)
        {
            var tema = await _context.Teme.FindAsync(id);
            if (tema == null)
            {
                return NotFound($"Tema cu ID-ul {id} nu a fost găsită.");
            }
            _context.Teme.Remove(tema);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("Clasa/{idClasa:int}")]
        public async Task<ActionResult<IEnumerable<Tema>>> GetTemeByClasa(int idClasa)
        {
            var teme = await _context.Teme
                .Where(t => t.IdClasa == idClasa)
                .Include(t => t.Materie)
                .ToListAsync();
            if (teme == null || !teme.Any())
            {
                return NotFound($"Nu s-au găsit teme pentru clasa cu ID-ul {idClasa}.");
            }
            return Ok(teme);
        }
        [HttpGet("Materie/{idMaterie:int}")]
        public async Task<ActionResult<IEnumerable<Tema>>> GetTemeByMaterie(int idMaterie)
        {
            var teme = await _context.Teme
                .Where(t => t.IdMaterie == idMaterie)
                .Include(t => t.Materie)
                .ToListAsync();
            if (teme == null || !teme.Any())
            {
                return NotFound($"Nu s-au găsit teme pentru materia cu ID-ul {idMaterie}.");
            }
            return Ok(teme);
        }
        [HttpGet("Clasa/{idClasa:int}/Materie/{idMaterie:int}")]
        public ActionResult<IEnumerable<Tema>> GetTemeByClasaAndMaterie(int idClasa, int idMaterie)
        {
            var teme = _context.Teme
                .Where(t => t.IdClasa == idClasa && t.IdMaterie == idMaterie)
                .Include(t => t.Materie)
                .ToList();
            if (teme == null || !teme.Any())
            {
                return NotFound($"Nu s-au găsit teme pentru clasa cu ID-ul {idClasa} și materia cu ID-ul {idMaterie}.");
            }
            return Ok(teme);
        }
        [HttpGet("Clasa/{idClasa:int}/Materie/{idMaterie:int}/TermenLimita")]
        public async Task<ActionResult<IEnumerable<Tema>>> GetTemeByClasaAndMaterieAndTermenLimita(int idClasa, int idMaterie, DateTime termenLimita)
        {
            var teme = await _context.Teme
                .Where(t => t.IdClasa == idClasa && t.IdMaterie == idMaterie && t.TermenLimita.Date == termenLimita.Date)
                .Include(t => t.Materie)
                .ToListAsync();
            if (teme == null || !teme.Any())
            {
                return NotFound($"Nu s-au găsit teme pentru clasa cu ID-ul {idClasa}, materia cu ID-ul {idMaterie} și termenul limită {termenLimita.ToShortDateString()}.");
            }
            return Ok(teme);
        }
        [HttpPatch]
        [Route("{id:int}/TermenLimita")]
        public ActionResult<Tema> UpdateTermenLimita(int id, [FromBody] DateTime termenLimita)
        {
            if (termenLimita == default)
            {
                return BadRequest("Termenul limită nu poate fi null sau invalid.");
            }
            var tema = _context.Teme.Find(id);
            if (tema == null)
            {
                return NotFound($"Tema cu ID-ul {id} nu a fost găsită.");
            }
            tema.TermenLimita = termenLimita;
            _context.Teme.Update(tema);
            _context.SaveChanges();
            return Ok(tema);
        }

        }
    }
