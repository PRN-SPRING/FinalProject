﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Vaccine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description can't be longer than 500 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Min Age To Use is required")]
        [Range(0, 150, ErrorMessage = "Min Age To Use must be between 0 and 150")]
        public int MinAgeToUse { get; set; }

        [Required(ErrorMessage = "Max Age To Use is required")]
        [Range(0, 150, ErrorMessage = "Max Age To Use must be between 0 and 150")]
        public int MaxAgeToUse { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value")]
        public decimal Price { get; set; }

        public ICollection<VaccinePackageDetail>? VaccinePackageDetails { get; set; }
        public ICollection<VaccinationSchedule>? VaccinationSchedules { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }

    }

}
