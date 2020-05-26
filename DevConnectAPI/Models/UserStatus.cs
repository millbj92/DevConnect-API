using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevConnectAPI.Models
{
    public class UserStatus
    {
        [Key]
        public int status_id { get; set; }
        public string Status { get; set; }
        public DateTime UpdateTime { get; set; }
        public User User { get; set; }
    }
}
