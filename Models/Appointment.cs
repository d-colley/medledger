using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedLedger.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentID { get; set; }
        [Required]
        public DateTime AppointmentDate { get; set; }
        public string AppointmentService { get; set; }
        public string AppointmentDescription { get; set; }

        [ForeignKey("Professional")]
        public int ProfessionalID { get; set; }

        [ForeignKey("Patient")]
        public int PatientID { get; set; }

        [ForeignKey("Clinic")]
        public int ClinicID { get; set; }
    }
}
