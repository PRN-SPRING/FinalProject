﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ChildId { get; set; }
        public int? VaccineId { get; set; }
        public int? VaccinePackageId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = null!;
        public string? Notes { get; set; }
        public User Customer { get; set; } = null!;
        public Child Child { get; set; } = null!;
        public Vaccine? Vaccine { get; set; }
        public VaccinePackage? VaccinePackage { get; set; }
        public ICollection<AppointmentDetail>? AppointmentDetails { get; set; }
        public ICollection<Payment>? Payments { get; set; }
        public ICollection<Feedback>? Feedbacks { get; set; }
    }

}
