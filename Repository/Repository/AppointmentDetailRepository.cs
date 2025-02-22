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
    public class AppointmentDetailRepository : IAppointmentDetailRepository
    {
        public async Task AddAppointmentDetailAsync(AppointmentDetail appointmentDetail)
        {
            try
            {
                using (var context = new PRNFinalProjectDBContext())
                {
                    context.AppointmentDetails.Add(appointmentDetail);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AppointmentDetail> GetAppointmentDetailByAppointmentIdAsync(int appointmentId)
        {
            try
            {
                using (var context = new PRNFinalProjectDBContext())
                {
                    return await context.AppointmentDetails
                        .Include(a => a.Doctor)
                        .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAppointmentDetailAsync(AppointmentDetail appointmentDetail)
        {
            try
            {
                using (var context = new PRNFinalProjectDBContext())
                {
                    var appointmentDetailToUpdate = await context.AppointmentDetails.FindAsync(appointmentDetail.Id);

                    if (appointmentDetailToUpdate == null)
                    {
                        throw new Exception("Appointment detail not found");
                    }

                    context.AppointmentDetails.Update(appointmentDetail);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
