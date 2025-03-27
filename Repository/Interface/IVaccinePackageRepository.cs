using Data.DTOs;
using Data.Entities;


namespace Repository.Interface
{
    public interface IVaccinePackageRepository
    {
        Task<IEnumerable<VaccinePackage>> GetAllVaccinePackagesAsync();
        Task<IEnumerable<VaccinePackageDTO>> GetAllVaccinePackagesDtoAsync();
        Task<VaccinePackageDTO?> GetVaccinePackageByIdAsync(int id);
        Task<VaccinePackageDTO> CreateVaccinePackageAsync(VaccinePackageDTO packageDto);
        Task<bool> UpdateVaccinePackageAsync(int id, VaccinePackageDTO packageDto);
        Task<bool> DeleteVaccinePackageAsync(int id);
        Task<List<VaccinePackageDTO>> GetListVaccinePackagesAsync();
    }
}
