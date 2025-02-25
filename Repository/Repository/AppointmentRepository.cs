﻿using Data;
using Data.DTOs;
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

        public Task CreateAppointmentAsync(AppointmentDTO appointment)
        {
            throw new NotImplementedException();
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
    }
}
