using AHTG_Test.Models;
using Microsoft.Extensions.Caching.Memory;

namespace AHTG_Test.Data
{
    /// <summary>
    /// This is just a very naive implementation of caching for the purpose of this demo app.
    /// It uses the in-memory cache but I have made the method signatures async to demonstrate how it 
    /// could relatively easily switch to using a distributed cache as its internal cache provider.
    /// Again, as this is a naive implementation so not dealing with cache failures, distributed locks for
    /// updating, etc.
    /// </summary>
    public class Cache : ICache
    {
        const string HOSPITALS_KEY = "hospitals";

        IMemoryCache _memoryCache;

        public Cache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task PopulateHospitalsAsync(List<Hospital> hospitals)
        {
            _memoryCache.Set<List<Hospital>>(HOSPITALS_KEY, hospitals);
            await Task.CompletedTask;
        }

        public async Task<List<Hospital>?> GetHospitalsAsync()
        {
            List<Hospital> hospitals;
            var found = _memoryCache.TryGetValue<List<Hospital>>(HOSPITALS_KEY, out hospitals);

            await Task.CompletedTask;

            if (found)
            {
                return hospitals;
            }
            else
            {
                return null;
            }
        }

        public async Task<Hospital?> GetHospitalAsync(int id)
        {
            List<Hospital> hospitals;
            var found = _memoryCache.TryGetValue<List<Hospital>>(HOSPITALS_KEY, out hospitals);

            await Task.CompletedTask;

            if (found)
            {
                return hospitals.SingleOrDefault(h => h.ID == id);
            }
            else
            {
                return null;
            }
        }

        public async Task AddHospital(Hospital hospital)
        {
            List<Hospital> hospitals;
            var found = _memoryCache.TryGetValue<List<Hospital>>(HOSPITALS_KEY, out hospitals);

            await Task.CompletedTask;

            if (found)
            {
                hospitals.Add(hospital);
                _memoryCache.Set(HOSPITALS_KEY, hospitals);
            }
        }

        public async Task UpdateHospital(Hospital hospital)
        {
            List<Hospital> hospitals;
            var found = _memoryCache.TryGetValue<List<Hospital>>(HOSPITALS_KEY, out hospitals);

            await Task.CompletedTask;

            if (found)
            {
                var oldHospital = hospitals.SingleOrDefault(h => h.ID == hospital.ID);
                if (oldHospital != null)
                {
                    hospitals.Remove(oldHospital); 
                }

                hospitals.Add(hospital);
                _memoryCache.Set(HOSPITALS_KEY, hospitals);
            }
        }

        public async Task DeleteHospital(int id)
        {
            List<Hospital> hospitals;
            var found = _memoryCache.TryGetValue<List<Hospital>>(HOSPITALS_KEY, out hospitals);

            await Task.CompletedTask;

            if (found)
            {
                var hospital = hospitals.SingleOrDefault(h => h.ID == id);
                if (hospital != null)
                {
                    hospitals.Remove(hospital);
                }

                _memoryCache.Set(HOSPITALS_KEY, hospitals);
            }
        }
    }
}
