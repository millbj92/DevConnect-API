using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace DevConnectAPI.Models
{
    public class User
    {
        [Key]
        public int user_id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public UserProfile userProfile { get; set; }

        public virtual HashSet<User> friends_list { get; set; }
        public virtual User Parent { get; set; }

        public List<UserMessage> Messages { get; set; }

        [NotMapped]
        public string? Token { get; set; }

        [JsonIgnore]
        public string verificationToken { get; set; }

        public bool isConfirmed { get; set; }
    }
}
