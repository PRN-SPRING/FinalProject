using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Repository.Interface;
using Repository.Repository;

namespace Repository
{
    public static class DependencyInjcection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {

            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            services.AddTransient<IVaccineRepository, VaccineRepository>();
            services.AddTransient<IAppointmentDetailRepository, AppointmentDetailRepository>();
            services.AddTransient<IVaccinationScheduleRepository, VaccinationScheduleRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddTransient<IVaccinePackageRepository, VaccinePackageRepository>();
            services.AddTransient<IChildRepository, ChildRepository>();
            return services;
        }
    }
}
