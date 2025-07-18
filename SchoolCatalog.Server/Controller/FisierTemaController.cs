using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolCatalog.Server.Data;
using SchoolCatalog.Server.Model;

namespace SchoolCatalog.Server.Controller
{
    [ApiController]
    [Route("api/fisiertema")]
    public class FisierTemaController : ControllerBase
    {
        private readonly SchoolCatalogContext _context;
        public FisierTemaController(SchoolCatalogContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFisierTemaById(int id)
        {
            var fisierTema = await _context.FisiereTema.FindAsync(id);
            if (fisierTema == null)
                return NotFound($"Fișierul de temă cu ID-ul {id} nu a fost găsit.");

            return Ok(fisierTema);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFisierTema([FromBody] FisierTema fisierTema)
        {
            if (fisierTema == null)
                return BadRequest("Fișierul de temă nu poate fi null.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.FisiereTema.Add(fisierTema);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFisierTemaById), new { id = fisierTema.IdFisier }, fisierTema);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateFisierTema(int id, [FromBody] FisierTema fisierTema)
        {
            if (fisierTema == null || fisierTema.IdFisier != id)
                return BadRequest("Fișierul de temă nu este valid.");

            var existingFisierTema = await _context.FisiereTema.FindAsync(id);
            if (existingFisierTema == null)
                return NotFound($"Fișierul de temă cu ID-ul {id} nu a fost găsit.");

            existingFisierTema.NumeFisier = fisierTema.NumeFisier;
            existingFisierTema.TemaId = fisierTema.TemaId;
            existingFisierTema.IdElev = fisierTema.IdElev;
            existingFisierTema.DataIncarcare = fisierTema.DataIncarcare;

            _context.FisiereTema.Update(existingFisierTema);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteFisierTema(int id)
        {
            var fisierTema = await _context.FisiereTema.FindAsync(id);
            if (fisierTema == null)
                return NotFound($"Fișierul de temă cu ID-ul {id} nu a fost găsit.");

            _context.FisiereTema.Remove(fisierTema);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFisiereTeme()
        {
            var fisiereTeme = await _context.FisiereTema.ToListAsync();
            if (!fisiereTeme.Any())
                return NotFound("Nu s-au găsit fișiere de teme.");

            return Ok(fisiereTeme);
        }

        [HttpGet("tid/{temaId:int}")]
        public async Task<IActionResult> GetFisiereTemeByTemaId(int temaId)
        {
            var fisiereTeme = await _context.FisiereTema.Where(f => f.TemaId == temaId).ToListAsync();
            if (!fisiereTeme.Any())
                return NotFound($"Nu s-au găsit fișiere de teme pentru Tema ID {temaId}.");

            return Ok(fisiereTeme);
        }

        [HttpGet("elev/{elevId:int}")]
        public async Task<IActionResult> GetFisiereTemeByElevId(int elevId)
        {
            var fisiereTeme = await _context.FisiereTema.Where(f => f.IdElev == elevId).ToListAsync();
            if (!fisiereTeme.Any())
                return NotFound($"Nu s-au găsit fișiere de teme pentru Elev ID {elevId}.");

            return Ok(fisiereTeme);
        }

        [HttpGet("tema/{temaId:int}/elev/{elevId:int}")]
        public async Task<IActionResult> GetFisierTemaByTemaIdAndElevId(int temaId, int elevId)
        {
            var fisierTema = await _context.FisiereTema
                .FirstOrDefaultAsync(f => f.TemaId == temaId && f.IdElev == elevId);

            if (fisierTema == null)
                return NotFound($"Nu s-a găsit fișier de temă pentru Tema ID {temaId} și Elev ID {elevId}.");

            return Ok(fisierTema);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PatchFisierTema(int id, [FromBody] JsonPatchDocument<FisierTema> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest("Documentul de patch nu poate fi nul.");

            var fisierTema = await _context.FisiereTema.FindAsync(id);
            if (fisierTema == null)
                return NotFound($"Fișierul de temă cu ID-ul {id} nu a fost găsit.");

            patchDoc.ApplyTo(fisierTema, error =>
            {
                ModelState.AddModelError(error.Operation?.path ?? string.Empty, error.ErrorMessage);
            });

            TryValidateModel(fisierTema);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.FisiereTema.Update(fisierTema);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
