using Data;
using Data.DTOs;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class VaccineRepository : IVaccineRepository
    {
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

                    // Update fields
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
    }
}
