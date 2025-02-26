using Data.Entities;


namespace Repository.Interface
{
    public interface IVaccinePackageRepository
    {
        Task<IEnumerable<VaccinePackage>> GetAllVaccinePackagesAsync();
    }
}
