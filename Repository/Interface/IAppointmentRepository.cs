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
        Task CreateAppointmentAsync(CreateAppointmentDTO appointment);
        Task<List<AppointmentDTO>> GetAppointmentsByUserIdAsync(int userId);
        Task UpdateAppointmentAsync(AppointmentDTO appointment);
        Task DeleteAppointmentAsync(int id);
        Task<bool> AppoinmentExistsAsync(int id);

        Task<List<AppointmentDTO>> GetAppointmentsNotificationAsync(int userId, DateTime startDate, DateTime endDate, string status);

        Task<List<AppointmentDTO>> GetAppointmentsByDoctorIdAsync(int id);
        Task<string> CreateAppointmentForDoctorAsync(CreateAppointmentDTO appointmentDto, AppointmentDetailDTO appointmentDetailDto, int doctorId);


    }
}
