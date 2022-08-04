using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedLedger.Models
{
    public class ServiceSchedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceID { get; set; }
        [Required]
        public string ServiceName { get; set; }
        public string ServiceDays { get; set; }
        public DateTime ServiceStartTime { get; set; }
        public DateTime ServicEndTime { get; set; }
        [ForeignKey("Clinic")]
        public int ClinicID { get; set; }

        public int CurrentTimeAvailable{ get; set; }

        public int MaxTimeAvailable { get; set; }

        public int MaxAppointments{ get; set; }
        public int CurrentAppointments{ get; set; }
        public int ServiceTime { get; set; }

        public int EfficientTaktTime { get; set; }

        public int EfficientResources { get; set; }

        public int ActualTaktTime { get; set; }

        public int ActualResources { get; set; }
        public string ResourceList { get; set; }

    }
}
