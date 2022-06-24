using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MedLedger.Models
{
    public class Clinic
    {
        [Key]
        public int ClinicID { get; set; }
        [Required]
        public string ClinicName { get; set; }
        public string ClinicLocation { get; set; }
        public string ClinicType { get; set; }
    }
}
