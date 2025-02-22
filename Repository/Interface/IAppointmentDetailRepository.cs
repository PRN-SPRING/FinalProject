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
        Task AddAppointmentDetailAsync(AppointmentDetail appointmentDetail);
        Task UpdateAppointmentDetailAsync(AppointmentDetail appointmentDetail);
        Task<AppointmentDetail> GetAppointmentDetailByAppointmentIdAsync(int appointmentId);
    }
}
