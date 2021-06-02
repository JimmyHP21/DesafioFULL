using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesafioFULLApi.Models;
using DesafioFULLApi.Helper;
using Microsoft.AspNetCore.Cors;

namespace DesafioFULLApi.Controllers
{
    [EnableCors("EnableCORS")]
    [Route("api/TitleDelay")]
    [ApiController]
    public class TitleDelaysController : ControllerBase
    {
        private readonly TitleDelayContext _context;

        public TitleDelaysController(TitleDelayContext context)
        {
            _context = context;
        }

        // GET: api/TitleDelays
        [DisableCors]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TitleDelayDTO>>> GetTitleDelays()
        {
            var dbList = await _context.TitleDelays.Include(x => x.DebtInstallments).ToListAsync();
            return dbList.Select(x => x.ToTitleDelayDTO()).ToList();
        }

        // GET: api/TitleDelays/5
        [DisableCors]
        [HttpGet("{id}")]
        public async Task<ActionResult<TitleDelayDTO>> GetTitleDelay(int id)
        {
            var titleDelay = await _context.TitleDelays.FindAsync(id);

            if (titleDelay == null)
            {
                return NotFound();
            }

            return titleDelay.ToTitleDelayDTO();
        }

        // PUT: api/TitleDelays/update/5
        [DisableCors]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutTitleDelay(int id, TitleDelayDTO titleDelay)
        {
            if (id != titleDelay.Id)
            {
                return BadRequest();
            }

            _context.Entry(titleDelay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TitleDelayExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TitleDelays/new
        [DisableCors]
        [HttpPost("new")]
        public async Task<ActionResult<TitleDelayDTO>> PostTitleDelay(TitleDelayDTO titleDelay)
        {
            _context.TitleDelays.Add(titleDelay.ToTitleDelay());
            await _context.SaveChangesAsync();

            return CreatedAtAction( nameof(GetTitleDelay), new { id = titleDelay.Id }, titleDelay);
        }

        // DELETE: api/TitleDelays/delete/5
        [DisableCors]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTitleDelay(int id)
        {
            var titleDelay = await _context.TitleDelays.FindAsync(id);
            if (titleDelay == null)
            {
                return NotFound();
            }

            _context.TitleDelays.Remove(titleDelay);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TitleDelayExists(int id)
        {
            return _context.TitleDelays.Any(e => e.Id == id);
        }
    }
}
