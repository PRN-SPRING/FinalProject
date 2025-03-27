using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class VaccinationScheduleDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Child ID is required")]
        public int ChildId { get; set; }

        public int? VaccineId { get; set; }

        public int? VaccinePackageId { get; set; }

        [Required(ErrorMessage = "Scheduled Date is required")]
        public DateTime ScheduledDate { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [StringLength(50, ErrorMessage = "Status can't be longer than 50 characters")]
        public string Status { get; set; } = null!;

        public string ChildName { get; set; } = null!;
        public string? VaccineName { get; set; }
        public string? VaccinePackageName { get; set; }
        public int? AppointmentId { get; set; }
    }
}
