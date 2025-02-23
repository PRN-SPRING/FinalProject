using Data.DTOs;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<AppointmentDTO>> GetAppointmentsAsync();
        Task<AppointmentDTO> GetAppointmentByIdAsync(int id);
        Task CreateAppointmentAsync(AppointmentDTO appointment);
        Task UpdateAppointmentAsync(AppointmentDTO appointment);
        Task DeleteAppointmentAsync(int id);
        Task<bool> AppoinmentExistsAsync(int id);
    }
}
