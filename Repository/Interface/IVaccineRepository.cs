using Data.DTOs;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IVaccineRepository
    {
        Task<VaccineDTO> GetVaccineByIdAsync(int id);
        Task<IEnumerable<VaccineDTO>> GetVaccinesAsync();
        Task AddVaccineAsync(VaccineDTO vaccine);
        Task UpdateVaccineAsync(VaccineDTO vaccine);
        Task DeleteVaccineAsync(int id);
        Task<bool> VaccineExistsAsync(int id);  
    }
}
