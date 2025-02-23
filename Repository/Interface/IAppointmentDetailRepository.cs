using Data.DTOs;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IAppointmentDetailRepository
    {
        Task AddAppointmentDetailAsync(AppointmentDetailDTO appointmentDetail);
        Task UpdateAppointmentDetailAsync(AppointmentDetailDTO appointmentDetail);
        Task<AppointmentDetailDTO> GetAppointmentDetailByAppointmentIdAsync(int appointmentId);
    }
}
