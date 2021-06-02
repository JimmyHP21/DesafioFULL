using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesafioFULLApi.Models;
using Microsoft.AspNetCore.Cors;

namespace DesafioFULLApi.Controllers
{
    [Route("api/DebtInstallments")]
    [ApiController]
    public class DebtInstallmentsController : ControllerBase
    {
        private readonly TitleDelayContext _context;

        public DebtInstallmentsController(TitleDelayContext context)
        {
            _context = context;
        }

        // GET: api/DebtInstallments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DebtInstallment>>> GetDebtInstallments()
        {
            return await _context.DebtInstallments.ToListAsync();
        }

        // GET: api/DebtInstallments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DebtInstallment>> GetDebtInstallments(int id)
        {
            var debtInstallments = await _context.DebtInstallments.FindAsync(id);

            if (debtInstallments == null)
            {
                return NotFound();
            }

            return debtInstallments;
        }

        // PUT: api/DebtInstallments/5
        [DisableCors]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDebtInstallments(int id, DebtInstallment debtInstallments)
        {
            if (id != debtInstallments.Id)
            {
                return BadRequest();
            }

            _context.Entry(debtInstallments).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DebtInstallmentsExists(id))
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

        // POST: api/DebtInstallments
        [DisableCors]
        [HttpPost]
        public async Task<ActionResult<DebtInstallment>> PostDebtInstallments(DebtInstallment debtInstallments)
        {
            _context.DebtInstallments.Add(debtInstallments);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDebtInstallments), new { id = debtInstallments.Id }, debtInstallments);
        }

        // DELETE: api/DebtInstallments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDebtInstallments(int id)
        {
            var debtInstallments = await _context.DebtInstallments.FindAsync(id);
            if (debtInstallments == null)
            {
                return NotFound();
            }

            _context.DebtInstallments.Remove(debtInstallments);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DebtInstallmentsExists(int id)
        {
            return _context.DebtInstallments.Any(e => e.Id == id);
        }
    }
}
