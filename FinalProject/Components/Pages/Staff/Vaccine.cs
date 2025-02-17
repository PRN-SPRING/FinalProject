namespace FinalProject.Components.Pages.Staff
{
    public class Vaccine
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int MinAgeToUse { get; set; }
        public int? MaxAgeToUse { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
