using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class UserDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Plase provide Username")]
        public string? Username { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Plase provide Password")]
        public string? Password { get; set; }
    }
}
