using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.DTOs
{
    public class UserInfoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username must be at most 50 characters.")]
        public string Username { get; set; } = null!;

        [StringLength(100, ErrorMessage = "Password must be at most 100 characters.")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, ErrorMessage = "Full Name must be at most 100 characters.")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(100, ErrorMessage = "Email must be at most 100 characters.")]
        public string Email { get; set; } = null!;

        [Phone(ErrorMessage = "Invalid Phone Number.")]
        [StringLength(20, ErrorMessage = "Phone Number must be at most 20 characters.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [StringLength(50, ErrorMessage = "Role must be at most 50 characters.")]
        public string Role { get; set; } = null!;

        [StringLength(200, ErrorMessage = "Address must be at most 200 characters.")]
        public string? Address { get; set; }

        [StringLength(10, ErrorMessage = "Gender must be at most 10 characters.")]
        public string? Gender { get; set; }

        public DateTime? Birthdate { get; set; }

        [StringLength(100, ErrorMessage = "Specialty must be at most 100 characters.")]
        public string? Specialty { get; set; }

        [StringLength(50, ErrorMessage = "License Number must be at most 50 characters.")]
        public string? LicenseNumber { get; set; }

        [StringLength(100, ErrorMessage = "Position must be at most 100 characters.")]
        public string? Position { get; set; }

        [Range(0, 100, ErrorMessage = "Years of Experience must be between 0 and 100.")]
        public int? YearsOfExperience { get; set; }
    }
}
