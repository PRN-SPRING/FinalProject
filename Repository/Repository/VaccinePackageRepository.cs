using Data;
using Data.DTOs;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Repository.Repository
{
    public class VaccinePackageRepository : IVaccinePackageRepository
    {
        private readonly IDbContextFactory<PRNFinalProjectDBContext> _contextFactory;

        public VaccinePackageRepository(IDbContextFactory<PRNFinalProjectDBContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<VaccinePackage>> GetAllVaccinePackagesAsync()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Set<VaccinePackage>()
                    .Include(vp => vp.PackageDetails)
                    .ThenInclude(pd => pd.Vaccine)
                    .ToListAsync();
            }
        }

        public async Task<IEnumerable<VaccinePackageDTO>> GetAllVaccinePackagesDtoAsync()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var packages = await context.VaccinePackages
                    .Include(vp => vp.PackageDetails)
                    .ThenInclude(pd => pd.Vaccine)
                    .OrderByDescending(vp => vp.Id)
                    .ToListAsync();

                return packages.Select(MapToDTO);
            }
        }

        public async Task<VaccinePackageDTO?> GetVaccinePackageByIdAsync(int id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var vaccinePackage = await context.VaccinePackages
                    .Include(vp => vp.PackageDetails)
                    .ThenInclude(pd => pd.Vaccine)
                    .FirstOrDefaultAsync(vp => vp.Id == id);

                return vaccinePackage != null ? MapToDTO(vaccinePackage) : null;
            }
        }

        public async Task<VaccinePackageDTO> CreateVaccinePackageAsync(VaccinePackageDTO packageDto)
        {
            using (var context = _contextFactory.CreateDbContext())
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

                context.VaccinePackages.Add(vaccinePackage);
                await context.SaveChangesAsync();

                return MapToDTO(vaccinePackage);
            }
        }

        public async Task<bool> UpdateVaccinePackageAsync(int id, VaccinePackageDTO packageDto)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var existingPackage = await context.VaccinePackages
                    .Include(vp => vp.PackageDetails)
                    .FirstOrDefaultAsync(vp => vp.Id == id);

                if (existingPackage == null) return false;

                existingPackage.Name = packageDto.Name;
                existingPackage.Description = packageDto.Description;
                existingPackage.Price = packageDto.Price;

                context.VaccinePackageDetails.RemoveRange(existingPackage.PackageDetails);

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

                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> DeleteVaccinePackageAsync(int id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var vaccinePackage = await context.VaccinePackages
                    .Include(vp => vp.PackageDetails)
                    .FirstOrDefaultAsync(vp => vp.Id == id);

                if (vaccinePackage == null) return false;

                context.VaccinePackageDetails.RemoveRange(vaccinePackage.PackageDetails);
                context.VaccinePackages.Remove(vaccinePackage);

                await context.SaveChangesAsync();
                return true;
            }
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
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.VaccinePackages
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
}