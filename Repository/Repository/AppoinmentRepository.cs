using Data.DTOs;
using Data.Entities;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class AppoinmentRepository : IAppointmentRepository
    {
        public Task<bool> AppoinmentExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task CreateAppointmentAsync(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public Task CreateAppointmentAsync(AppointmentDTO appointment)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAppointmentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Appointment>> GetAppointmentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAppointmentAsync(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAppointmentAsync(AppointmentDTO appointment)
        {
            throw new NotImplementedException();
        }

        Task<AppointmentDTO> IAppointmentRepository.GetAppointmentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<AppointmentDTO>> IAppointmentRepository.GetAppointmentsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
