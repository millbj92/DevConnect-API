using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevConnectAPI.Models
{
    public class UserProfile
    {
        [Key]
        public int profile_id { get; set; }
        public User user { get; set; }
        public int user_id { get; set; }
        public string? Bio { get; set; }
        public DateTime? Birthday { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? UpdateTime { get; set; }
        public UserStatus UserStatus { get; set; }

        public List<WorkPlace> EmploymentHistory { get; set; }
    }
}
