using BussinessObject.DTOs;

namespace Repository.Interface
{
    public interface IVaccineRepository
    {
        Task<VaccineDTO> GetVaccineByIdAsync(int id);
        Task<IEnumerable<VaccineDTO>> GetVaccinesAsync();
        Task AddVaccineAsync(VaccineDTO vaccine);
        Task UpdateVaccineAsync(VaccineDTO vaccine);
        Task DeleteVaccineAsync(int id);
        Task<bool> VaccineExistsAsync(int id);
        Task<List<VaccineDTO>> GetAllVaccinesAsync();

    }
}
