using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.DTOs
{
    public class AppointmentDetailDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "AppointmentId is required")]
        public int AppointmentId { get; set; }

        [Required(ErrorMessage = "DoctorId is required")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; } = null!;

        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }

        public string? DoctorName { get; set; }
    }
}
