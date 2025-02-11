using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class VaccinePackageDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PackageId { get; set; }
        public int VaccineId { get; set; }
        public int Quantity { get; set; } = 1;
        public VaccinePackage Package { get; set; } = null!;
        public Vaccine Vaccine { get; set; } = null!;
    }
}


