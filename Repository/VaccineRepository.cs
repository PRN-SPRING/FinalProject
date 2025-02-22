using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class VaccineRepository : IVaccineRepository
    {
        public async Task AddVaccineAsync(Vaccine vaccine)
        {
            try
            {
                using (var db = new PRNFinalProjectDBContext())
                {
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

        public async Task<Vaccine> GetVaccineByIdAsync(int id)
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

                    return vaccine;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Vaccine>> GetVaccinesAsync()
        {
            try
            {
                using (var db = new PRNFinalProjectDBContext())
                {
                    return await db.Vaccines.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateVaccineAsync(Vaccine vaccine)
        {
            try
            {
                using (var db = new PRNFinalProjectDBContext())
                {
                    var existingVaccine = await db.Vaccines.FindAsync(vaccine.Id);
                    if (existingVaccine == null)
                    {
                        throw new Exception("Vaccine not found");
                    }

                    // Update fields
                    existingVaccine.Name = vaccine.Name;
                    existingVaccine.Description = vaccine.Description;
                    existingVaccine.MinAgeToUse = vaccine.MinAgeToUse;
                    existingVaccine.MaxAgeToUse = vaccine.MaxAgeToUse;
                    existingVaccine.Price = vaccine.Price;

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
