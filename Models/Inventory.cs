using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedLedger.Models
{
    public class Inventory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InventoryID { get; set; }
        [Required]
        public string InventoryName { get; set; }  
        public int InventoryLevel { get; set; }

        [ForeignKey("Clinic")]
        public int ClinicID { get; set; }

        public int InventoryUnit { get; set; }
    }
}
