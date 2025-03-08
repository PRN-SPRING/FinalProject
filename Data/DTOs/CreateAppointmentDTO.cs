
using Data.Entities;

namespace Data.DTOs
{
    public class CreateAppointmentDTO
    {
        public int CustomerId { get; set; }
        public int ChildId { get; set; }
        public int? VaccineId { get; set; }
        public int? VaccinePackageId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = null!;
        public string? Notes { get; set; }
        
    }
}
