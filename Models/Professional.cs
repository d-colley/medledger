using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedLedger.Models
{
    public class Professional
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfessionalID { get; set; }
        [Required]
        public string ProfessionalName { get; set; }
        public string ProfessionalEmail { get; set; }
        public string ProfessionalSpecialty { get; set; }
        public int ProfessionalExpYears { get; set; }

        [ForeignKey ("User")]
        public int UserID { get; set; }

        [ForeignKey("Clinic")]
        public int ClinicID { get; set; }

        [ForeignKey("Team")]
        public string TeamID { get; set; }
    }
}
