using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevConnectAPI.Models
{
    public class Message
    {
        [Key]
        public int message_id { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Read { get; set; }
        public User sender { get; set; }
    }
}
