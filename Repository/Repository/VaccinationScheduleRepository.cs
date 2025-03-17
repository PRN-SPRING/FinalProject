using Data;
using Data.DTOs;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Repository.Repository
{
    public class VaccinationScheduleRepository : IVaccinationScheduleRepository
    {
        public async Task AddVaccinationScheduleAsync(VaccinationScheduleDTO vaccinationScheduleDto)
        {
            try
            {
                using (var context = new PRNFinalProjectDBContext())
                {
                    var vaccinationSchedule = new VaccinationSchedule
                    {
                        ChildId = vaccinationScheduleDto.ChildId,
                        VaccineId = vaccinationScheduleDto.VaccineId,
                        VaccinePackageId = vaccinationScheduleDto.VaccinePackageId,
                        ScheduledDate = vaccinationScheduleDto.ScheduledDate,
                        Status = vaccinationScheduleDto.Status,
                        AppointmentId = vaccinationScheduleDto.AppointmentId
                    };

                    context.VaccinationSchedules.Add(vaccinationSchedule);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<VaccinationScheduleDTO> GetVaccinationScheduleByAppointmentIdAsync(int appointmentId)
        {
            try
            {
                using (var context = new PRNFinalProjectDBContext())
                {
                    var vaccinationSchedule = await context.VaccinationSchedules
                        .Include(v => v.Child)
                        .Include(v => v.Vaccine)
                        .Include(v => v.VaccinePackage)
                        .FirstOrDefaultAsync(v => v.AppointmentId == appointmentId);

                    if (vaccinationSchedule == null)
                    {
                        //throw new Exception("Vaccination schedule not found");
                        return null;
                    }

                    return new VaccinationScheduleDTO
                    {
                        Id = vaccinationSchedule.Id,
                        ChildId = vaccinationSchedule.ChildId,
                        ChildName = vaccinationSchedule.Child?.FullName,
                        VaccineId = vaccinationSchedule.VaccineId,
                        VaccineName = vaccinationSchedule.Vaccine?.Name,
                        VaccinePackageId = vaccinationSchedule.VaccinePackageId,
                        VaccinePackageName = vaccinationSchedule.VaccinePackage?.Name,
                        ScheduledDate = vaccinationSchedule.ScheduledDate,
                        Status = vaccinationSchedule.Status,
                        AppointmentId = vaccinationSchedule.AppointmentId
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateVaccinationScheduleAsync(VaccinationScheduleDTO vaccinationScheduleDto)
        {
            try
            {
                using (var context = new PRNFinalProjectDBContext())
                {
                    var vaccinationScheduleToUpdate = await context.VaccinationSchedules.FindAsync(vaccinationScheduleDto.Id);
                    if (vaccinationScheduleToUpdate == null)
                    {
                        throw new Exception("Vaccination schedule not found");
                    }

                    vaccinationScheduleToUpdate.ChildId = vaccinationScheduleDto.ChildId;
                    vaccinationScheduleToUpdate.VaccineId = vaccinationScheduleDto.VaccineId;
                    vaccinationScheduleToUpdate.VaccinePackageId = vaccinationScheduleDto.VaccinePackageId;
                    vaccinationScheduleToUpdate.ScheduledDate = vaccinationScheduleDto.ScheduledDate;
                    vaccinationScheduleToUpdate.Status = vaccinationScheduleDto.Status;
                    vaccinationScheduleToUpdate.AppointmentId = vaccinationScheduleDto.AppointmentId;

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
