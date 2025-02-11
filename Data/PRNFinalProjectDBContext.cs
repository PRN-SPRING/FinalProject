using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class PRNFinalProjectDBContext :DbContext
    {
        public PRNFinalProjectDBContext(DbContextOptions<PRNFinalProjectDBContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<VaccinePackage> VaccinePackages { get; set; }
        public DbSet<VaccinePackageDetail> VaccinePackageDetails { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<VaccinationSchedule> VaccinationSchedules { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentDetail> AppointmentDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Prevents cascade delete for Users to avoid accidental deletions
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Customer)
                .WithMany()
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Child)
                .WithMany()
                .HasForeignKey(a => a.ChildId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Vaccine)
                .WithMany()
                .HasForeignKey(a => a.VaccineId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.VaccinePackage)
                .WithMany()
                .HasForeignKey(a => a.VaccinePackageId)
                .OnDelete(DeleteBehavior.NoAction);

            // Fix multiple cascade paths issue for AppointmentDetail
            modelBuilder.Entity<AppointmentDetail>()
                .HasOne(ad => ad.Appointment)
                .WithMany()
                .HasForeignKey(ad => ad.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppointmentDetail>()
                .HasOne(ad => ad.Doctor)
                .WithMany()
                .HasForeignKey(ad => ad.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            // Fix multiple cascade paths issue for Feedback
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Appointment)
                .WithMany()
                .HasForeignKey(f => f.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Customer)
                .WithMany()
                .HasForeignKey(f => f.CustomerId)
                .OnDelete(DeleteBehavior.NoAction); // Prevent cascade delete
        }

    }
}
