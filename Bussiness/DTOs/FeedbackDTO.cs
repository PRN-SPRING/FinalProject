using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.DTOs
{
    public class FeedbackDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "AppointmentId is required.")]
        public int AppointmentId { get; set; }

        [Required(ErrorMessage = "CustomerId is required.")]
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        [StringLength(500, ErrorMessage = "Comments must be at most 500 characters.")]
        public string? Comments { get; set; }
    }
}
