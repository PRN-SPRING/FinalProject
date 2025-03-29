using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.DTOs
{
    public class ChildDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "CustomerId is required.")]
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, ErrorMessage = "Full Name must be at most 100 characters.")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Birthdate is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Birthdate format.")]
        public DateTime Birthdate { get; set; }

        [StringLength(10, ErrorMessage = "Gender must be at most 10 characters.")]
        public string? Gender { get; set; }
    }
}
