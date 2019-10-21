using System;
using System.ComponentModel.DataAnnotations;

namespace ContactMaintenance.Models
{
    public class LogRecord
    {
        [Key]
        public int EventID { get; set; }
        [Required]
        [MaxLength(50)]
        public string LogLevel { get; set; }
        [MaxLength(4000)]
        public string Message { get; set; }
        [Required]
        public DateTime? CreatedTime { get; set; }

    }

   
}
