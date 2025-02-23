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
        Task AddVaccinationScheduleAsync(VaccinationSchedule vaccinationSchedule);
        Task UpdateVaccinationScheduleAsync(VaccinationSchedule vaccinationSchedule);
        Task<VaccinationSchedule> GetVaccinationScheduleByAppointmentIdAsync(int appointmentId);
    }
}
