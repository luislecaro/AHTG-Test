﻿#nullable disable
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
        private readonly IWebHostEnvironment _hostingEnv;
        private readonly ICache _cache;
        private readonly ILogger<HospitalsController> _logger;

        private const int FAKE_DELAY = 2000;

        

        public HospitalsController(ApplicationDbContext context, IWebHostEnvironment hostingEnv, ICache cache,
            ILogger<HospitalsController> logger)
        {
            _context = context;
            _hostingEnv = hostingEnv;
            _cache = cache;
            _logger = logger;
        }

        // GET: api/Hospitals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hospital>>> GetHospital()
        {
            // Even if I have the "is development" check here I still wouldn't do this
            // in a real production application. This is just a quick way to force
            // loading spinners in the demo app
            if (_hostingEnv.IsDevelopment())
            {
                // simulate some delay in request
                await Task.Delay(FAKE_DELAY); 
            }

            var hospitals = await _cache.GetHospitalsAsync();
            if (hospitals != null)
            {
                return hospitals;
            }

            hospitals = await _context.Hospital.ToListAsync();
            await _cache.PopulateHospitalsAsync(hospitals);

            return hospitals;
        }

        // GET: api/Hospitals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hospital>> GetHospital(int id)
        {
            if (_hostingEnv.IsDevelopment())
            {
                // simulate some delay in request
                await Task.Delay(FAKE_DELAY);
            }

            var cachedHospital = await _cache.GetHospitalAsync(id);
            
            if (cachedHospital != null)
            {
                return cachedHospital;
            }

            var hospital = await _context.Hospital.FindAsync(id);            

            if (hospital == null)
            {
                return NotFound();
            }

            await _cache.AddHospital(hospital);

            return hospital;
        }

        // PUT: api/Hospitals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospital(int id, Hospital hospital)
        {
            if (_hostingEnv.IsDevelopment())
            {
                // simulate some delay in request
                await Task.Delay(FAKE_DELAY);
            }

            if (id != hospital.ID)
            {
                return BadRequest();
            }

            _context.Entry(hospital).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError($"Error occurred while updating hospital: {ex}");

                if (!HospitalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            await _cache.UpdateHospital(hospital);

            return NoContent();
        }

        // POST: api/Hospitals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hospital>> PostHospital(Hospital hospital)
        {
            if (_hostingEnv.IsDevelopment())
            {
                // simulate some delay in request
                await Task.Delay(FAKE_DELAY);
            }

            try
            {
                _context.Hospital.Add(hospital);
                await _context.SaveChangesAsync();                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while creating hospital: {ex}");
                return StatusCode(500);
            }

            await _cache.AddHospital(hospital);

            return CreatedAtAction("GetHospital", new { id = hospital.ID }, hospital);
        }

        // DELETE: api/Hospitals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHospital(int id)
        {
            if (_hostingEnv.IsDevelopment())
            {
                // simulate some delay in request
                await Task.Delay(FAKE_DELAY);
            }

            var hospital = await _cache.GetHospitalAsync(id);
            if (hospital == null)
            {
                return NotFound();
            }

            try
            {
                _context.Hospital.Remove(hospital);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while deleting hospital: {ex}");
            }

            await _cache.DeleteHospital(id);

            return NoContent();
        }

        private bool HospitalExists(int id)
        {
            return _context.Hospital.Any(e => e.ID == id);
        }
    }
}
