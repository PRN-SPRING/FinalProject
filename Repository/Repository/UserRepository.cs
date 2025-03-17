using Data;
using Data;
using Data.DTOs;
using Data.Entities;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly PRNFinalProjectDBContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserRepository(PRNFinalProjectDBContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
                var normalizedUsername = username.ToLower();
                return await _context.Users
                    .FirstOrDefaultAsync(u => u.Username.ToLower() == normalizedUsername);
        }

        public async Task<UserInfoDTO> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return null;
            }

            return new UserInfoDTO
            {
                Id = user.Id,
                Username = user.Username,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                Address = user.Address,
                Gender = user.Gender,
                Birthdate = user.Birthdate,
                Specialty = user.Specialty,
                LicenseNumber = user.LicenseNumber,
                Position = user.Position,
                YearsOfExperience = user.YearsOfExperience
            };
        }

        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            //var user = await GetByUsernameAsync(username);
            //if (user == null) return false;
            //var result = _passwordHasher.VerifyHashedPassword(
            //    user,
            //    user.Password,
            //    password);

            //if (result == PasswordVerificationResult.Success)
            //{
            //    return true;
            //}

            //return false;
            var user = await GetByUsernameAsync(username);
            if (user == null) return false;

            // Directly compare the password (plaintext)
            if (user.Password == password)
            {
                return true;
            }

            return false;
        }


        public async Task<User> CreateUserAsync(string username, string password,
                string fullName, string email, string? phoneNumber,
                string? address, string? gender,string role, DateTime? birthdate)
        {
            // Validate required fields
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)
                || string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Required fields (Username, FullName, Email, Role) cannot be empty");
            }

            // Check for existing username
            if (await UsernameExistsAsync(username))
        {
                throw new InvalidOperationException("Username already exists");
            }

            // Check for existing email
            if (await EmailExistsAsync(email))
            {
                throw new InvalidOperationException("Email already exists");
            }

            try
            {
                User user = new User();
                // Hash password
                user.Password = password;

                // Initialize collections to prevent null reference
                user.Username = username;
                user.Email = email;
                user.PhoneNumber = phoneNumber;
                user.FullName = fullName;
                user.Address = address;
                user.Gender = gender;
                user.Birthdate = birthdate;
                user.Role = role;
                user.Children = new List<Child>();
                user.Appointments = new List<Appointment>();
                user.AppointmentDetails = new List<AppointmentDetail>();
                user.Feedbacks = new List<Feedback>();

                // Additional validation for doctor-specific fields
                if (user.Role.Equals("Doctor", StringComparison.OrdinalIgnoreCase))
                {
                    if (string.IsNullOrEmpty(user.Specialty) || string.IsNullOrEmpty(user.LicenseNumber))
                    {
                        throw new ArgumentException("Doctors must have Specialty and License Number");
                    }
                }

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Error creating user in database", ex);
                }
            }
        private async Task<bool> EmailExistsAsync(string email)
            {
            return await _context.Users.AnyAsync(u => u.Email == email);
            }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.Users
                .AnyAsync(u => u.Username.ToLower() == username.ToLower());
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(string? role)
        {
            if (string.IsNullOrEmpty(role))
            {
                return await _context.Users.ToListAsync();
            }

            return await _context.Users
                .Where(u => u.Role.ToLower() == role.ToLower())
                .ToListAsync();
        }

        public async Task<IEnumerable<UserInfoDTO>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();

            return users.Select(user => new UserInfoDTO
            {
                Id = user.Id,
                Username = user.Username,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                Address = user.Address,
                Gender = user.Gender,
                Birthdate = user.Birthdate,
                Specialty = user.Specialty,
                LicenseNumber = user.LicenseNumber,
                Position = user.Position,
                YearsOfExperience = user.YearsOfExperience
            });
        }
    }
}
