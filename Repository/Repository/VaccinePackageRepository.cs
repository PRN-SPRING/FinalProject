using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;


namespace Repository.Repository
{
    public class VaccinePackageRepository : IVaccinePackageRepository
    {
        private readonly PRNFinalProjectDBContext _context;

        public VaccinePackageRepository(PRNFinalProjectDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VaccinePackage>> GetAllVaccinePackagesAsync()
        {
            return await _context.Set<VaccinePackage>().Include(vp => vp.PackageDetails)
                .ThenInclude(pd => pd.Vaccine)
                .ToListAsync();
        }
    }
}
