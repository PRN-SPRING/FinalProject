using Data.DTOs;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IVaccinationScheduleRepository
    {
        Task AddVaccinationScheduleAsync(VaccinationScheduleDTO vaccinationSchedule);
        Task UpdateVaccinationScheduleAsync(VaccinationScheduleDTO vaccinationSchedule);
        Task<VaccinationScheduleDTO> GetVaccinationScheduleByAppointmentIdAsync(int appointmentId);
    }
}
