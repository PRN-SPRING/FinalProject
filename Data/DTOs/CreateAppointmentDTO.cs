
using Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Data.DTOs
{
    public class CreateAppointmentDTO
    {
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "A child must be selected")]
        public int ChildId { get; set; }
        public int? VaccineId { get; set; }
        public int? VaccinePackageId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = null!;
        public string? Notes { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (VaccineId == null && VaccinePackageId == null)
            {
                yield return new ValidationResult(
                    "Either a vaccine or a vaccine package is required.",
                    new[] { nameof(VaccineId), nameof(VaccinePackageId) });
            }
        }
    }
}
