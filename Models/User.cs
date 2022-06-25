using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedLedger.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserRassword { get; set; }
        public string UserRole { get; set; }
        public DateTime Created { get; set; }

        [NotMapped]
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }
    }
}
