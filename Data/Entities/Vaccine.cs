using System;
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
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int MinAgeToUse { get; set; }
        public int? MaxAgeToUse { get; set; }
        public decimal Price { get; set; }
        public ICollection<VaccinePackageDetail>? VaccinePackageDetails { get; set; }
        public ICollection<VaccinationSchedule>? VaccinationSchedules{ get; set; }
        public ICollection<Appointment>? Appointments{ get; set; }
        
    }

}
