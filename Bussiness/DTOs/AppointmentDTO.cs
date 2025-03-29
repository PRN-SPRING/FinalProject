using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.DTOs
{
    public class AppointmentDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "CustomerId is required")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "ChildId is required")]
        public int ChildId { get; set; }

        public int? VaccineId { get; set; }
        public int? VaccinePackageId { get; set; }

        [Required(ErrorMessage = "AppointmentDate is required")]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; } = null!;

        public string? Notes { get; set; }

        [Required(ErrorMessage = "CustomerName is required")]
        public string CustomerName { get; set; } = null!;

        [Required(ErrorMessage = "ChildName is required")]
        public string ChildName { get; set; } = null!;

        public string? VaccineName { get; set; }
        public string? VaccinePackageName { get; set; }
    }
}
