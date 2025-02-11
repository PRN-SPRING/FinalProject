using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Data.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string Role { get; set; } = null!;
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? Specialty { get; set; }
        public string? LicenseNumber { get; set; }
        public string? Position { get; set; }
        public int? YearsOfExperience { get; set; }
    }

}
