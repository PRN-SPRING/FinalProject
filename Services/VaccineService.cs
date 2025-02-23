using Data.Entities;
using Repository;

namespace Services
{
    public class VaccineService : IVaccineService
    {
        private readonly IVaccineRepository _vaccineRepository;

        public VaccineService(IVaccineRepository vaccineRepository)
        {
            _vaccineRepository = vaccineRepository ?? throw new ArgumentNullException(nameof(vaccineRepository));
        }

        public async Task AddVaccineAsync(Vaccine vaccine)
        {
            if (vaccine == null)
                throw new ArgumentNullException(nameof(vaccine));

            await _vaccineRepository.AddVaccineAsync(vaccine);
        }

        public async Task DeleteVaccineAsync(int id)
        {
            var existingVaccine = await _vaccineRepository.GetVaccineByIdAsync(id);
            if (existingVaccine == null)
                throw new KeyNotFoundException($"Vaccine with ID {id} not found.");

            await _vaccineRepository.DeleteVaccineAsync(id);
        }

        public async Task<Vaccine> GetVaccineByIdAsync(int id)
        {
            var vaccine = await _vaccineRepository.GetVaccineByIdAsync(id);
            if (vaccine == null)
                throw new KeyNotFoundException($"Vaccine with ID {id} not found.");

            return vaccine;
        }

        public async Task<IEnumerable<Vaccine>> GetVaccinesAsync()
        {
            return await _vaccineRepository.GetVaccinesAsync();
        }

        public async Task UpdateVaccineAsync(Vaccine vaccine)
        {
            if (vaccine == null)
                throw new ArgumentNullException(nameof(vaccine));

            var existingVaccine = await _vaccineRepository.GetVaccineByIdAsync(vaccine.Id);
            if (existingVaccine == null)
                throw new KeyNotFoundException($"Vaccine with ID {vaccine.Id} not found.");

            await _vaccineRepository.UpdateVaccineAsync(vaccine);
        }

        public async Task<bool> VaccineExistsAsync(int id)
        {
            return await _vaccineRepository.VaccineExistsAsync(id);
        }
    }
}
