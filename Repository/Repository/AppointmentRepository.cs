using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        public Task<bool> AppoinmentExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task CreateAppointmentAsync(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAppointmentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            try
            {
                using (var db = new PRNFinalProjectDBContext())
                {
                    var appointment = await db.Appointments.FindAsync(id);
                    if (appointment == null)
                    {
                        throw new Exception("Appointment not found");
                    }
                    return appointment;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsAsync()
        {
            try
            {
                using (var db = new PRNFinalProjectDBContext())
                {
                    return await db.Appointments
                        .AsNoTracking()
                        .Include(a => a.Customer)
                        .Include(a => a.Child)
                        .Include(a => a.Vaccine)
                        .Include(a => a.VaccinePackage)
                        .Include(a => a.AppointmentDetails)
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            try
            {
                using (var db = new PRNFinalProjectDBContext())
                {
                    var existingAppointment = await db.Appointments.FindAsync(appointment.Id);
                    if (existingAppointment == null)
                    {
                        throw new Exception("Appointment not found");
                    }

                    db.Entry(existingAppointment).CurrentValues.SetValues(appointment);

                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
