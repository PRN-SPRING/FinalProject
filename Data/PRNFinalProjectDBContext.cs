using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class PRNFinalProjectDBContext :DbContext
    {
        public PRNFinalProjectDBContext()
        {
        }

        public PRNFinalProjectDBContext(DbContextOptions<PRNFinalProjectDBContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }

        private string GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true).Build();
            return configuration["ConnectionStrings:DefaultConnection"];
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

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Customer)
                .WithMany(u => u.Appointments)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.NoAction); // Prevents multiple cascade paths

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Child)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.ChildId)
                .OnDelete(DeleteBehavior.NoAction); // Prevents multiple cascade paths

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.VaccinePackage)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.VaccinePackageId)
                .OnDelete(DeleteBehavior.NoAction); // Prevents multiple cascade paths
        }

    }
}
