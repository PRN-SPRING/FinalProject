using Data;
using BussinessObject.DTOs;
using BussinessObject.Entities;
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
        private readonly PRNFinalProjectDBContext _context;
        public AppointmentDetailRepository(PRNFinalProjectDBContext context)
        {
            _context = context;
        }
        public async Task AddAppointmentDetailAsync(AppointmentDetailDTO appointmentDetailDto)
        {
            try
            {
                using (var context = new PRNFinalProjectDBContext())
                {
                    var appointmentDetail = new AppointmentDetail
                    {
                        AppointmentId = appointmentDetailDto.AppointmentId,
                        DoctorId = appointmentDetailDto.DoctorId,
                        Status = appointmentDetailDto.Status,
                        Diagnosis = appointmentDetailDto.Diagnosis,
                        Treatment = appointmentDetailDto.Treatment
                    };

                    context.AppointmentDetails.Add(appointmentDetail);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AppointmentDetailDTO> GetAppointmentDetailByAppointmentIdAsync(int appointmentId)
        {
            try
            {
                using (var context = new PRNFinalProjectDBContext())
                {
                    var appointmentDetail = await context.AppointmentDetails
                        .Include(a => a.Doctor)
                        .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);

                    if (appointmentDetail == null)
                    {
                        //throw new Exception("Appointment detail not found");
                        return null;
                    }

                    return new AppointmentDetailDTO
                    {
                        Id = appointmentDetail.Id,
                        AppointmentId = appointmentDetail.AppointmentId,
                        DoctorId = appointmentDetail.DoctorId,
                        Status = appointmentDetail.Status,
                        Diagnosis = appointmentDetail.Diagnosis,
                        Treatment = appointmentDetail.Treatment,
                        DoctorName = appointmentDetail.Doctor?.FullName
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAppointmentDetailAsync(AppointmentDetailDTO appointmentDetailDto)
        {
            try
            {
                using (var context = new PRNFinalProjectDBContext())
                {
                    var appointmentDetailToUpdate = await context.AppointmentDetails.FindAsync(appointmentDetailDto.Id);

                    if (appointmentDetailToUpdate == null)
                    {
                        throw new Exception("Appointment detail not found");
                    }

                    appointmentDetailToUpdate.Status = appointmentDetailDto.Status;
                    appointmentDetailToUpdate.Diagnosis = appointmentDetailDto.Diagnosis;
                    appointmentDetailToUpdate.Treatment = appointmentDetailDto.Treatment;
                    appointmentDetailToUpdate.DoctorId = appointmentDetailDto.DoctorId;

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task CreateAppointmentDetailAsync(AppointmentDetailDTO detail)
        {
            var entity = new AppointmentDetail // Your entity class
            {
                AppointmentId = detail.AppointmentId,
                DoctorId = detail.DoctorId,
                Status = detail.Status,
                Diagnosis = detail.Diagnosis,
                Treatment = detail.Treatment
                // Map other fields as needed
            };
            _context.AppointmentDetails.Add(entity);
            await _context.SaveChangesAsync();

            detail.Id = entity.Id; // Update DTO with new ID
        }
    }
}
