using AHTG_Test.Data;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace AHTG_Test.Models
{
    public class SeedDataService
    {
        ApplicationDbContext _context;

        public SeedDataService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            using (_context)
            {
                if (_context == null || _context.Hospital == null)
                {
                    throw new ArgumentNullException("Null ApplicationDbContext");
                }

                if (_context.Hospital.Any())
                {
                    return;   // DB has been seeded
                }

                _context.Hospital.AddRange(
                    Enumerable.Range(1, 30).Select(range => 
                    new Hospital
                    {
                        Name = $"Hospital{range}",
                        NumberOfEmployees = Random.Shared.Next(500),
                        PhoneNumber = "469455" + Random.Shared.Next(1000,9999),
                        AddressLine1 = $"Some street {range}",
                        AddressLine2 = $"Some details {range}",
                        AddressCity = "Frisco",
                        AddressState = "TX",
                        AddressZip = "75" + Random.Shared.Next(100,999)
                    })
                );
                _context.SaveChanges();
            }
        }
    }
}