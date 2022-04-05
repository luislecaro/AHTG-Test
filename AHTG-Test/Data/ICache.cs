using AHTG_Test.Models;

namespace AHTG_Test.Data
{
    public interface ICache
    {
        Task PopulateHospitalsAsync(List<Hospital> hospitals);

        Task<List<Hospital>?> GetHospitalsAsync();

        Task<Hospital?> GetHospitalAsync(int id);

        Task AddHospital(Hospital hospital);

        Task UpdateHospital(Hospital hospital);

        Task DeleteHospital(int id);
    }
}
