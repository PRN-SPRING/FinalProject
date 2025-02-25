using System.ComponentModel.DataAnnotations;
namespace FinalProject.ViewModel
{
    public class ViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Plase provide Username")]
        public string? Username { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Plase provide Password")]
        public string? Password { get; set; }
    }
}
