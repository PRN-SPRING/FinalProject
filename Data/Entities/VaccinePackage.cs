﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class VaccinePackage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<VaccinePackageDetail>? PackageDetails { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
        public ICollection<VaccinationSchedule>? VaccinationSchedules{ get; set; }

    }
}


