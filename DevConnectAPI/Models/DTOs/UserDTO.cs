using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevConnectAPI.Models
{
    public class UserDTO
    {

        public int user_id { get; set; }
        public string email { get; set; }
        public UserProfile userProfile { get; set; }
        public virtual HashSet<User> friends_list { get; set; }
        public List<UserMessage> Messages { get; set; }
        public string? Token { get; set; }

    }
}
