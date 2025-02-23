using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class VaccineDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
        public string Name { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Description can't be longer than 500 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Min Age To Use is required")]
        [Range(0, 150, ErrorMessage = "Min Age must be between 0 and 150")]
        public int MinAgeToUse { get; set; }

        [Required(ErrorMessage = "Max Age To Use is required")]
        [Range(0, 150, ErrorMessage = "Max Age must be between 0 and 150")]
        public int MaxAgeToUse { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value")]
        public decimal Price { get; set; }
    }
}
