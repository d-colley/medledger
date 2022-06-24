using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MedLedger.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string UserName { get; set; }
        public string UserRassword { get; set; }
        public string UserRole { get; set; }
        public DateTime Created { get; set; }

    }
}
