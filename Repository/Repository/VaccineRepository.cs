using Data;
using Data.DTOs;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Repository.Interface;

namespace Repository.Repository
{
    public class VaccineRepository : IVaccineRepository
    {
        private readonly PRNFinalProjectDBContext _context;

        public VaccineRepository(PRNFinalProjectDBContext context)
        {
            _context = context;
        }
        public async Task AddVaccineAsync(VaccineDTO vaccineDto)
        {
            try
            {
                using (var db = new PRNFinalProjectDBContext())
                {
                    var vaccine = new Vaccine
                    {
                        Name = vaccineDto.Name,
                        Description = vaccineDto.Description,
                        MinAgeToUse = vaccineDto.MinAgeToUse,
                        MaxAgeToUse = vaccineDto.MaxAgeToUse,
                        Price = vaccineDto.Price
                    };

                    await db.Vaccines.AddAsync(vaccine);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteVaccineAsync(int id)
        {
            try
            {
                using (var db = new PRNFinalProjectDBContext())
                {
                    var vaccine = await db.Vaccines.FindAsync(id);
                    if (vaccine == null)
                    {
                        throw new Exception("Vaccine not found");
                    }

                    db.Vaccines.Remove(vaccine);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<VaccineDTO> GetVaccineByIdAsync(int id)
        {
            try
            {
                using (var db = new PRNFinalProjectDBContext())
                {
                    var vaccine = await db.Vaccines.FindAsync(id);
                    if (vaccine == null)
                    {
                        throw new Exception("Vaccine not found");
                    }

                    return MapToDTO(vaccine);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<VaccineDTO>> GetVaccinesAsync()
        {
            try
            {
                using (var db = new PRNFinalProjectDBContext())
                {
                    return await db.Vaccines
                        .OrderByDescending(vp => vp.Id)
                        .Select(vaccine => MapToDTO(vaccine))
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateVaccineAsync(VaccineDTO vaccineDto)
        {
            try
            {
                using (var db = new PRNFinalProjectDBContext())
                {
                    var existingVaccine = await db.Vaccines.FindAsync(vaccineDto.Id);
                    if (existingVaccine == null)
                    {
                        throw new Exception("Vaccine not found");
                    }

                    // Cập nhật thông tin vaccine
                    existingVaccine.Name = vaccineDto.Name;
                    existingVaccine.Description = vaccineDto.Description;
                    existingVaccine.MinAgeToUse = vaccineDto.MinAgeToUse;
                    existingVaccine.MaxAgeToUse = vaccineDto.MaxAgeToUse;
                    existingVaccine.Price = vaccineDto.Price;

                    db.Vaccines.Update(existingVaccine);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> VaccineExistsAsync(int id)
        {
            try
            {
                using (var db = new PRNFinalProjectDBContext())
                {
                    return await db.Vaccines.AnyAsync(v => v.Id == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static VaccineDTO MapToDTO(Vaccine vaccine)
        {
            return new VaccineDTO
            {
                Id = vaccine.Id,
                Name = vaccine.Name,
                Description = vaccine.Description,
                MinAgeToUse = vaccine.MinAgeToUse,
                MaxAgeToUse = vaccine.MaxAgeToUse,
                Price = vaccine.Price
            };
        }

        public async Task<List<VaccineDTO>> GetAllVaccinesAsync()
        {
            return await _context.Vaccines
                .Select(vaccine => new VaccineDTO
                {
                    Id = vaccine.Id,
                    Name = vaccine.Name,
                    Description = vaccine.Description,
                    MinAgeToUse = vaccine.MinAgeToUse,
                    MaxAgeToUse = vaccine.MaxAgeToUse,
                    Price = vaccine.Price
                })
                .ToListAsync();
        }


    }
}
