using AHTG_Test.Models;

namespace AHTG_Test.Data
{
    public interface IHospitalRepository
    {
        Task<List<Hospital>?> GetHospitalsAsync();

        ValueTask<Hospital?> GetHospitalAsync(int id);

        Task UpdateHospitalAsync(int id, Hospital hospital);

        Task AddHospitalAsync(Hospital hospital);

        Task DeleteHospitalAsync(int id);

        bool HospitalExists(int id);        
    }
}
