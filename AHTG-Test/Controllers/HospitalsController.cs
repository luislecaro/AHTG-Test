#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AHTG_Test.Data;
using AHTG_Test.Models;

namespace AHTG_Test.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]    
    public class HospitalsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private const int FAKE_DELAY = 2000;

        public HospitalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Hospitals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hospital>>> GetHospital()
        {
            // simulate some delay in request
            await Task.Delay(FAKE_DELAY);

            return await _context.Hospital.ToListAsync();
        }

        // GET: api/Hospitals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hospital>> GetHospital(int id)
        {
            var hospital = await _context.Hospital.FindAsync(id);

            // simulate some delay in request
            await Task.Delay(FAKE_DELAY);

            if (hospital == null)
            {
                return NotFound();
            }

            return hospital;
        }

        // PUT: api/Hospitals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospital(int id, Hospital hospital)
        {
            if (id != hospital.ID)
            {
                return BadRequest();
            }

            _context.Entry(hospital).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalExists(id))
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

        // POST: api/Hospitals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hospital>> PostHospital(Hospital hospital)
        {
            _context.Hospital.Add(hospital);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHospital", new { id = hospital.ID }, hospital);
        }

        // DELETE: api/Hospitals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHospital(int id)
        {
            var hospital = await _context.Hospital.FindAsync(id);
            if (hospital == null)
            {
                return NotFound();
            }

            _context.Hospital.Remove(hospital);
            await _context.SaveChangesAsync();

            // simulate some delay in request
            await Task.Delay(FAKE_DELAY);

            return NoContent();
        }

        private bool HospitalExists(int id)
        {
            return _context.Hospital.Any(e => e.ID == id);
        }
    }
}
