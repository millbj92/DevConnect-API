using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevConnectAPI.Models
{
    public class UserMessage
    {
        [Key]
        public int user_message_id { get; set; }
        public User user { get; set; }
        public Message message { get; set; }
    }
}
