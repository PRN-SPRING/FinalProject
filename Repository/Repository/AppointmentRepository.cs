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
    public class AppointmentRepository : IAppointmentRepository
    {
        public Task<bool> AppoinmentExistsAsync(int id)
        {
            throw new NotImplementedException();
        }
        private readonly PRNFinalProjectDBContext _context;
        public AppointmentRepository(PRNFinalProjectDBContext context)
        {
            _context = context;
        }
        public async Task CreateAppointmentAsync(CreateAppointmentDTO appointmentDto)
        {
            var appointment = new Appointment
            {
                CustomerId = appointmentDto.CustomerId,
                ChildId = appointmentDto.ChildId,
                VaccineId = appointmentDto.VaccineId,
                VaccinePackageId = appointmentDto.VaccinePackageId,
                AppointmentDate = appointmentDto.AppointmentDate,
                Status = appointmentDto.Status,
                Notes = appointmentDto.Notes
            };

            // Add the appointment to the database
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
        }
        public async Task<List<AppointmentDTO>> GetAppointmentsByUserIdAsync(int userId)
        {
            return await _context.Appointments
                .Where(a => a.CustomerId == userId)
                .Select(a => new AppointmentDTO
                {
                    Id = a.Id,
                    CustomerId = a.CustomerId,
                    ChildId = a.ChildId,
                    ChildName = a.Child.FullName,
                    VaccineId = a.VaccineId,
                    VaccineName = a.Vaccine != null ? a.Vaccine.Name : null,
                    VaccinePackageId = a.VaccinePackageId,
                    VaccinePackageName = a.VaccinePackage != null ? a.VaccinePackage.Name : null,
                    AppointmentDate = a.AppointmentDate,
                    Status = a.Status,
                    Notes = a.Notes
                })
                .ToListAsync();
        }

        public Task DeleteAppointmentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AppointmentDTO> GetAppointmentByIdAsync(int id)
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
                    return new AppointmentDTO
                    {
                        Id = appointment.Id,
                        CustomerId = appointment.CustomerId,
                        ChildId = appointment.ChildId,
                        VaccineId = appointment.VaccineId,
                        VaccinePackageId = appointment.VaccinePackageId,
                        AppointmentDate = appointment.AppointmentDate,
                        Status = appointment.Status,
                        Notes = appointment.Notes,
                        CustomerName = appointment.Customer.FullName,
                        ChildName = appointment.Child.FullName,
                        VaccineName = appointment.Vaccine?.Name,
                        VaccinePackageName = appointment.VaccinePackage?.Name
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<AppointmentDTO>> GetAppointmentsAsync()
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
                        .Select(a => new AppointmentDTO
                        {
                            Id = a.Id,
                            CustomerId = a.CustomerId,
                            ChildId = a.ChildId,
                            VaccineId = a.VaccineId,
                            VaccinePackageId = a.VaccinePackageId,
                            AppointmentDate = a.AppointmentDate,
                            Status = a.Status,
                            Notes = a.Notes,
                            CustomerName = a.Customer.FullName,
                            ChildName = a.Child.FullName,
                            VaccineName = a.Vaccine.Name,
                            VaccinePackageName = a.VaccinePackage.Name
                        })
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAppointmentAsync(AppointmentDTO appointmentDto)
        {
            try
            {
                using (var db = new PRNFinalProjectDBContext())
                {
                    var existingAppointment = await db.Appointments.FindAsync(appointmentDto.Id);
                    if (existingAppointment == null)
                    {
                        throw new Exception("Appointment not found");
                    }

                    existingAppointment.CustomerId = appointmentDto.CustomerId;
                    existingAppointment.ChildId = appointmentDto.ChildId;
                    existingAppointment.VaccineId = appointmentDto.VaccineId;
                    existingAppointment.VaccinePackageId = appointmentDto.VaccinePackageId;
                    existingAppointment.AppointmentDate = appointmentDto.AppointmentDate;
                    existingAppointment.Status = appointmentDto.Status;
                    existingAppointment.Notes = appointmentDto.Notes;

                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<AppointmentDTO>> GetAppointmentsByDoctorIdAsync(int doctorId)
        {
            try
            {
                using (var db = new PRNFinalProjectDBContext())
                {
                    // Get all AppointmentIds linked to the given DoctorId in AppointmentDetails table
                    var appointmentIds = await db.AppointmentDetails
                        .Where(ad => ad.DoctorId == doctorId)
                        .Select(ad => ad.AppointmentId)
                        .ToListAsync();

                    if (!appointmentIds.Any())
                    {
                        throw new Exception("No appointments found for this doctor.");
                    }

                    // Retrieve all Appointments that match the AppointmentIds
                    var appointments = await db.Appointments
                        .Where(a => appointmentIds.Contains(a.Id))
                        .Include(a => a.Customer)
                        .Include(a => a.Child)
                        .Include(a => a.Vaccine)
                        .Include(a => a.VaccinePackage)
                        .ToListAsync();

                    // Map to DTO
                    return appointments.Select(appointment => new AppointmentDTO
                    {
                        Id = appointment.Id,
                        CustomerId = appointment.CustomerId,
                        ChildId = appointment.ChildId,
                        VaccineId = appointment.VaccineId,
                        VaccinePackageId = appointment.VaccinePackageId,
                        AppointmentDate = appointment.AppointmentDate,
                        Status = appointment.Status,
                        Notes = appointment.Notes,
                        CustomerName = appointment.Customer?.FullName,
                        ChildName = appointment.Child?.FullName,
                        VaccineName = appointment.Vaccine?.Name,
                        VaccinePackageName = appointment.VaccinePackage?.Name
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> CreateAppointmentForDoctorAsync(CreateAppointmentDTO appointmentDto, AppointmentDetailDTO appointmentDetailDto, int doctorId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Validate Customer
                var customer = await _context.Users.FindAsync(appointmentDto.CustomerId);
                if (customer == null) return "Invalid customer.";

                // Validate Child and ensure it belongs to the Customer
                var child = await _context.Children
                    .FirstOrDefaultAsync(c => c.Id == appointmentDto.ChildId && c.CustomerId == appointmentDto.CustomerId);
                if (child == null) return "Invalid child or child does not belong to the customer.";

                // Validate Vaccine or Vaccine Package (optional logic from earlier)
                if (appointmentDto.VaccineId != null && appointmentDto.VaccinePackageId != null)
                    return "You cannot select both a vaccine and a vaccine package.";
                if (appointmentDto.VaccineId != null && !await _context.Vaccines.AnyAsync(v => v.Id == appointmentDto.VaccineId))
                    return "Selected vaccine does not exist.";
                if (appointmentDto.VaccinePackageId != null && !await _context.VaccinePackages.AnyAsync(p => p.Id == appointmentDto.VaccinePackageId))
                    return "Selected vaccine package does not exist.";

                // Create Appointment
                var appointment = new Appointment
                {
                    CustomerId = appointmentDto.CustomerId,
                    ChildId = appointmentDto.ChildId,
                    VaccineId = appointmentDto.VaccineId,
                    VaccinePackageId = appointmentDto.VaccinePackageId,
                    AppointmentDate = appointmentDto.AppointmentDate,
                    Status = appointmentDto.Status,
                    Notes = appointmentDto.Notes
                };

                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();

                // Create AppointmentDetail
                var appointmentDetail = new AppointmentDetail
                {
                    AppointmentId = appointment.Id,
                    DoctorId = doctorId,
                    Status = appointmentDetailDto.Status,
                    Diagnosis = appointmentDetailDto.Diagnosis,
                    Treatment = appointmentDetailDto.Treatment
                };

                _context.AppointmentDetails.Add(appointmentDetail);
                await _context.SaveChangesAsync();

                // Optionally create VaccinationSchedule (if required)
                if (appointmentDto.VaccineId != null || appointmentDto.VaccinePackageId != null)
                {
                    var schedule = new VaccinationSchedule
                    {
                        ChildId = appointmentDto.ChildId,
                        VaccineId = appointmentDto.VaccineId,
                        VaccinePackageId = appointmentDto.VaccinePackageId,
                        ScheduledDate = appointmentDto.AppointmentDate,
                        Status = "Scheduled",
                        AppointmentId = appointment.Id
                    };
                    _context.VaccinationSchedules.Add(schedule);
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();
                return "Appointment successfully created.";
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return $"Error creating appointment: {ex.Message}";
            }
        }


        public async Task<List<AppointmentDTO>> GetAppointmentsNotificationAsync(int userId,DateTime startDate, DateTime endDate, string status)
        {
            try
            {
                var appointments = await _context.Appointments
                    .Where(a => a.CustomerId == userId && a.AppointmentDate.Date >= startDate.Date &&
                                 a.AppointmentDate.Date <= endDate.Date &&
                                 a.Status == status)
                    .Select(a => new AppointmentDTO
                    {

                        Id = a.Id,
                        CustomerId = a.CustomerId,
                        ChildId = a.ChildId,
                        VaccineId = a.VaccineId,
                        VaccinePackageId = a.VaccinePackageId,
                        AppointmentDate = a.AppointmentDate,
                        Status = a.Status,
                        Notes = a.Notes,
                        CustomerName = a.Customer.FullName,
                        ChildName = a.Child.FullName,
                        VaccineName = a.Vaccine.Name,
                        VaccinePackageName = a.VaccinePackage.Name
                    })
                    .ToListAsync();

                return appointments;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception(ex.Message);
            }
        }
    }


    
}
