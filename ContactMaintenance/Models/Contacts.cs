using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMaintenance.Models
{
    public class Contacts
    {
        [Key]
        public long ContactId { get; set; }

        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$")]
        [Required]
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
    }
}
