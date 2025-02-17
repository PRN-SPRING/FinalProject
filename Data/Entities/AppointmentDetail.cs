using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class AppointmentDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }
        public string Status { get; set; } = null!;
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public Appointment Appointment { get; set; } = null!;
        public User Doctor { get; set; } = null!;
        
    }

}
