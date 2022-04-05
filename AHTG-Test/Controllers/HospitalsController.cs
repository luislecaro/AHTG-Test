#nullable disable
using AHTG_Test.Data;
using AHTG_Test.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AHTG_Test.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]    
    public class HospitalsController : ControllerBase
    {
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IWebHostEnvironment _hostingEnv;
        private readonly ICache _cache;
        private readonly ILogger<HospitalsController> _logger;

        private const int FAKE_DELAY = 2000;        

        public HospitalsController(
            IHospitalRepository hospitalRepository,
            IWebHostEnvironment hostingEnv, 
            ICache cache,
            ILogger<HospitalsController> logger)
        {
            _hospitalRepository = hospitalRepository;
            _hostingEnv = hostingEnv;
            _cache = cache;
            _logger = logger;
        }

        // GET: api/Hospitals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hospital>>> GetHospital()
        {
            _logger.LogDebug($"{nameof(HospitalsController)}: Received request to list all hospitals");

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

            hospitals = await _hospitalRepository.GetHospitalsAsync();
            await _cache.PopulateHospitalsAsync(hospitals);

            return hospitals;
        }

        // GET: api/Hospitals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hospital>> GetHospital(int id)
        {
            _logger.LogDebug($"{nameof(HospitalsController)}: Received request to retrieve hospital {id}");

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

            var hospital = await _hospitalRepository.GetHospitalAsync(id);

            if (hospital == null)
            {
                return NotFound();
            }

            await _cache.AddHospital(hospital);

            return hospital;
        }

        // PUT: api/Hospitals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospital(int id, Hospital hospital)
        {
            _logger.LogDebug($"{nameof(HospitalsController)}: Received request to update hospital {id} with " + 
                $"info:{Environment.NewLine}{JsonSerializer.Serialize(hospital)}");

            if (_hostingEnv.IsDevelopment())
            {
                // simulate some delay in request
                await Task.Delay(FAKE_DELAY);
            }

            if (id != hospital.ID)
            {
                return BadRequest();
            }            

            try
            {
                await _hospitalRepository.UpdateHospitalAsync(id, hospital);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError($"Error occurred while updating hospital: {ex}");

                if (!_hospitalRepository.HospitalExists(id))
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
        [HttpPost]
        public async Task<ActionResult<Hospital>> PostHospital(Hospital hospital)
        {
            _logger.LogDebug($"{nameof(HospitalsController)}: Received request to create hospital with " +
                $"info:{Environment.NewLine}{JsonSerializer.Serialize(hospital)}");

            if (_hostingEnv.IsDevelopment())
            {
                // simulate some delay in request
                await Task.Delay(FAKE_DELAY);
            }

            try
            {
                await _hospitalRepository.AddHospitalAsync(hospital);
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
            _logger.LogDebug($"{nameof(HospitalsController)}: Received request to delete hospital {id}");

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
                await _hospitalRepository.DeleteHospitalAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while deleting hospital: {ex}");
            }

            await _cache.DeleteHospital(id);

            return NoContent();
        }

        
    }
}
