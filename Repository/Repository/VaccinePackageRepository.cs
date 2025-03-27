using Data;
using Data.DTOs;
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

        public async Task<IEnumerable<VaccinePackageDTO>> GetAllVaccinePackagesDtoAsync()
        {
            var packages = await _context.VaccinePackages
                .Include(vp => vp.PackageDetails)
                .ThenInclude(pd => pd.Vaccine)
                .ToListAsync();

            return packages.Select(MapToDTO);
        }

        public async Task<VaccinePackageDTO?> GetVaccinePackageByIdAsync(int id)
        {
            var vaccinePackage = await _context.VaccinePackages
                .Include(vp => vp.PackageDetails)
                .ThenInclude(pd => pd.Vaccine)
                .FirstOrDefaultAsync(vp => vp.Id == id);

            return vaccinePackage != null ? MapToDTO(vaccinePackage) : null;
        }

        public async Task<VaccinePackageDTO> CreateVaccinePackageAsync(VaccinePackageDTO packageDto)
        {
            var vaccinePackage = new VaccinePackage
            {
                Name = packageDto.Name,
                Description = packageDto.Description,
                Price = packageDto.Price,
                PackageDetails = new List<VaccinePackageDetail>()
            };

            if (packageDto.PackageDetails != null && packageDto.PackageDetails.Any())
            {
                foreach (var pd in packageDto.PackageDetails)
                {
                    vaccinePackage.PackageDetails.Add(new VaccinePackageDetail
                    {
                        VaccineId = pd.VaccineId,
                        Quantity = pd.Quantity
                    });
                }
            }

            _context.VaccinePackages.Add(vaccinePackage);
            await _context.SaveChangesAsync();

            return MapToDTO(vaccinePackage);
        }

        public async Task<bool> UpdateVaccinePackageAsync(int id, VaccinePackageDTO packageDto)
        {
            var existingPackage = await _context.VaccinePackages
                .Include(vp => vp.PackageDetails)
                .FirstOrDefaultAsync(vp => vp.Id == id);

            if (existingPackage == null) return false;

            // Cập nhật thông tin package
            existingPackage.Name = packageDto.Name;
            existingPackage.Description = packageDto.Description;
            existingPackage.Price = packageDto.Price;

            // Xóa danh sách VaccinePackageDetail cũ
            _context.VaccinePackageDetails.RemoveRange(existingPackage.PackageDetails);

            // Thêm danh sách VaccinePackageDetail mới
            if (packageDto.PackageDetails != null && packageDto.PackageDetails.Any())
            {
                foreach (var pd in packageDto.PackageDetails)
                {
                    existingPackage.PackageDetails.Add(new VaccinePackageDetail
                    {
                        VaccineId = pd.VaccineId,
                        Quantity = pd.Quantity
                    });
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteVaccinePackageAsync(int id)
        {
            var vaccinePackage = await _context.VaccinePackages
                .Include(vp => vp.PackageDetails)
                .FirstOrDefaultAsync(vp => vp.Id == id);

            if (vaccinePackage == null) return false;

            // Xóa VaccinePackageDetail trước
            _context.VaccinePackageDetails.RemoveRange(vaccinePackage.PackageDetails);

            // Xóa VaccinePackage
            _context.VaccinePackages.Remove(vaccinePackage);

            await _context.SaveChangesAsync();
            return true;
        }

        private static VaccinePackageDTO MapToDTO(VaccinePackage vp)
        {
            return new VaccinePackageDTO
            {
                Id = vp.Id,
                Name = vp.Name,
                Description = vp.Description,
                Price = vp.Price,
                PackageDetails = vp.PackageDetails?.Any() == true
                    ? vp.PackageDetails.Select(pd => new VaccinePackageDetailDTO
                    {
                        Id = pd.Id,
                        VaccineId = pd.VaccineId,
                        VaccineName = pd.Vaccine?.Name,
                        Quantity = pd.Quantity
                    }).ToList()
                    : null
            };
        }

        public async Task<List<VaccinePackageDTO>> GetListVaccinePackagesAsync()
        {
            return await _context.VaccinePackages
                .Select(p => new VaccinePackageDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price
                })
                .ToListAsync();
        }
    }
}
