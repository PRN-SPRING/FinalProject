using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Repository.Repository
{
    public class VaccinationScheduleRepository : IVaccinationScheduleRepository
    {
        public async Task AddVaccinationScheduleAsync(VaccinationSchedule vaccinationSchedule)
        {
            try
            {
                using (var context = new PRNFinalProjectDBContext())
                {
                    context.VaccinationSchedules.Add(vaccinationSchedule);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<VaccinationSchedule> GetVaccinationScheduleByAppointmentIdAsync(int appointmentId)
        {
            try
            {
                using (var context = new PRNFinalProjectDBContext())
                {
                    return await context.VaccinationSchedules
                        .FirstOrDefaultAsync(v => v.AppointmentId == appointmentId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateVaccinationScheduleAsync(VaccinationSchedule vaccinationSchedule)
        {
            try
            {
                using (var context = new PRNFinalProjectDBContext())
                {
                    var vaccinationScheduleToUpdate = await context.VaccinationSchedules.FindAsync(vaccinationSchedule.Id);
                    if (vaccinationScheduleToUpdate == null)
                    {
                        throw new Exception("Vaccination schedule not found");
                    }

                    context.VaccinationSchedules.Update(vaccinationSchedule);
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
