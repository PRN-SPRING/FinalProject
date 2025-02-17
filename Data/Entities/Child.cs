using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Child
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime Birthdate { get; set; }
        public string? Gender { get; set; }
        public User Customer { get; set; } = null!;
        public ICollection<VaccinationSchedule>? VaccinationSchedules { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }

}
