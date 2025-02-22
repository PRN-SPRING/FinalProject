using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IVaccineService
    {
        Task<Vaccine> GetVaccineByIdAsync(int id);
        Task<IEnumerable<Vaccine>> GetVaccinesAsync();
        Task AddVaccineAsync(Vaccine vaccine);
        Task UpdateVaccineAsync(Vaccine vaccine);
        Task DeleteVaccineAsync(int id);
        Task<bool> VaccineExistsAsync(int id);
    }
}
