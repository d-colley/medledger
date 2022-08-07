using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedLedger.Models
{
    public class Clinic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClinicID { get; set; }
        [Required]
        public string ClinicName { get; set; }
        public string ClinicLocation { get; set; }
        public string ClinicType { get; set; }
        public string ClinicServices { get; set; }
        public bool ClinicOverprovision { get; set; }
        public bool ClinicUnderprovision { get; set; }
    }
}
