using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevConnectAPI.Models
{
    public class Friends
    {
        [Key]
        public int relation_id { get; set; }

        public int user_id { get; set; }

        public User user { get; set; }

        public int FriendId { get; set; }

        public User friend { get; set; }
    }
}
