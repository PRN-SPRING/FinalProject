using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.DTOs
{
    public class VaccinePackageDetailDTO
    {
        public int Id { get; set; }
        public int VaccineId { get; set; }
        public string VaccineName { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
