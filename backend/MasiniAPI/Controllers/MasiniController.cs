using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MasiniAPI.Data;
using MasiniAPI.Models;

namespace MasiniAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MasiniController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MasiniController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Masini
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Masina>>> GetAllMasini()
        {
            try
            {
                var masini = await _context.Masini.ToListAsync();
                return Ok(new { Success = true, Message = "Lista de mașini obținută cu succes", Result = masini });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = $"Eroare la obținerea listei: {ex.Message}" });
            }
        }

        // GET: api/Masini/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Masina>> GetMasina(int id)
        {
            try
            {
                var masina = await _context.Masini.FindAsync(id);

                if (masina == null)
                {
                    return NotFound(new { Success = false, Message = "Mașina nu a fost găsită" });
                }

                return Ok(new { Success = true, Message = "Mașina găsită", Result = masina });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = $"Eroare la căutarea mașinii: {ex.Message}" });
            }
        }

        // POST: api/Masini
        [HttpPost]
        public async Task<ActionResult<Masina>> AddMasina(Masina masina)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Success = false, Message = "Date invalide", Result = ModelState });
                }

                _context.Masini.Add(masina);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetMasina), new { id = masina.Id }, 
                    new { Success = true, Message = "Mașina a fost adăugată cu succes", Result = masina });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = $"Eroare la adăugarea mașinii: {ex.Message}" });
            }
        }

        // PUT: api/Masini/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditMasina(int id, Masina masina)
        {
            try
            {
                if (id != masina.Id)
                {
                    return BadRequest(new { Success = false, Message = "ID-ul nu coincide" });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Success = false, Message = "Date invalide", Result = ModelState });
                }

                _context.Entry(masina).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return Ok(new { Success = true, Message = "Mașina a fost modificată cu succes", Result = masina });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MasinaExists(id))
                {
                    return NotFound(new { Success = false, Message = "Mașina nu a fost găsită" });
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = $"Eroare la modificarea mașinii: {ex.Message}" });
            }
        }

        // DELETE: api/Masini/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMasina(int id)
        {
            try
            {
                var masina = await _context.Masini.FindAsync(id);
                if (masina == null)
                {
                    return NotFound(new { Success = false, Message = "Mașina nu a fost găsită" });
                }

                _context.Masini.Remove(masina);
                await _context.SaveChangesAsync();

                return Ok(new { Success = true, Message = "Mașina a fost ștearsă cu succes" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = $"Eroare la ștergerea mașinii: {ex.Message}" });
            }
        }

        private bool MasinaExists(int id)
        {
            return _context.Masini.Any(e => e.Id == id);
        }
    }
}
