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
        
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
        [Required]
        public string Email { get; set; }

        [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$")]
        [Required]
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
    }
}
