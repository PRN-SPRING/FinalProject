using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Entities
{
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; }
        public string? Comments { get; set; }
        public Appointment Appointment { get; set; } = null!;
        public User Customer { get; set; } = null!;
    }

}
