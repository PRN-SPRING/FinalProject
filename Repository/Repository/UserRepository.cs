using Data;

using BussinessObject.DTOs;
using BussinessObject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System.Data;
using System.Diagnostics;

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

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateUserAsync(UserInfoDTO user)
        {
            var updateUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (updateUser != null)
            {
                updateUser.Username = user.Username ?? updateUser.Username;
                updateUser.FullName = user.FullName ?? updateUser.FullName;
                updateUser.Email = user.Email ?? updateUser.Email;
                updateUser.Role = user.Role ?? updateUser.Role;

                // Nullable fields can be updated directly
                updateUser.PhoneNumber = user.PhoneNumber;
                updateUser.Address = user.Address;
                updateUser.Gender = user.Gender;
                updateUser.Birthdate = user.Birthdate;
                updateUser.Specialty = user.Specialty;
                updateUser.LicenseNumber = user.LicenseNumber;
                updateUser.Position = user.Position;
                updateUser.YearsOfExperience = user.YearsOfExperience;

                Debug.WriteLine(updateUser.LicenseNumber);
                Debug.WriteLine(user.LicenseNumber);

                _context.Users.Update(updateUser);
                await _context.SaveChangesAsync();
            }
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


        public async Task CreateUserAsync(string username, string password,
                string fullName, string email, string? phoneNumber,
                string? address, string? gender, string role, DateTime? birthdate)
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
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Error creating user in database", ex);
            }
        }

        public async Task CreateEmployeeUserAsync(UserInfoDTO user)
        {
            var newUser = new User();
            try
            {
                newUser.Username = user.Username;
                newUser.Password = user.Password; // Ensure this is hashed before storing!
                newUser.FullName = user.FullName;
                newUser.Email = user.Email;
                newUser.PhoneNumber = user.PhoneNumber;
                newUser.Role = user.Role;
                newUser.Address = user.Address;
                newUser.Gender = user.Gender;
                newUser.Birthdate = user.Birthdate;
                newUser.Specialty = user.Specialty;
                newUser.LicenseNumber = user.LicenseNumber;
                newUser.Position = user.Position;
                newUser.YearsOfExperience = user.YearsOfExperience;

                // Initialize navigation properties to prevent null reference issues
                newUser.Children = new List<Child>();
                newUser.Appointments = new List<Appointment>();
                newUser.AppointmentDetails = new List<AppointmentDetail>();
                newUser.Feedbacks = new List<Feedback>();
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
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

        public async Task<List<User>> GetEmployeeUserAsync(string[]? roles = null)
        {
            if (roles == null || roles.Length == 0)
            {
                return await _context.Users.ToListAsync();
            }

            // Convert roles to lowercase for case-insensitive comparison
            var lowercaseRoles = roles.Select(r => r.ToLower()).ToArray();

            return await _context.Users
                .Where(u => lowercaseRoles.Contains(u.Role.ToLower()))
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

        public async Task<List<UserInfoDTO>> GetAllUsersAsync()
        {
            return await _context.Users
                .Select(u => new UserInfoDTO
                {
                    Id = u.Id,
                    Username = u.Username,
                    Password = u.Password, // Note: Consider security implications of returning passwords
                    FullName = u.FullName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Role = u.Role,
                    Address = u.Address,
                    Gender = u.Gender,
                    Birthdate = u.Birthdate,
                    Specialty = u.Specialty,
                    LicenseNumber = u.LicenseNumber,
                    Position = u.Position,
                    YearsOfExperience = u.YearsOfExperience
                })
                .ToListAsync();
        }
    }
}
