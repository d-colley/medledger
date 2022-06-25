using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedLedger.Models
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }
        [Required]
        public string PatientName { get; set; }
        public DateTime PatientDOB { get; set; }
        public string PatientAddress { get; set; }
        public int PatientPhoneNumber { get; set; }
        public string PatientInsuranceProvider { get; set; }
        public string Purpose { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
    }
}
