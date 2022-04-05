using AHTG_Test.Models;
using Microsoft.EntityFrameworkCore;

namespace AHTG_Test.Data
{
    public class HospitalRepository : IHospitalRepository
    {
        private readonly ApplicationDbContext _context;

        public HospitalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<Hospital>> GetHospitalsAsync()
        {
            return _context.Hospital.ToListAsync();
        }

        public ValueTask<Hospital?> GetHospitalAsync(int id)
        {
            return _context.Hospital.FindAsync(id);
        }

        public Task UpdateHospitalAsync(int id, Hospital hospital)
        {
            _context.Entry(hospital).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public Task AddHospitalAsync(Hospital hospital)
        {
            _context.Hospital.Add(hospital);
            return _context.SaveChangesAsync();
        }

        public async Task DeleteHospitalAsync(int id)
        {
            var hospital = await GetHospitalAsync(id);
            if (hospital != null)
            {
                _context.Hospital.Remove(hospital);
                await _context.SaveChangesAsync(); 
            }
        }

        public bool HospitalExists(int id)
        {
            return _context.Hospital.Any(e => e.ID == id);
        }

    }
}
