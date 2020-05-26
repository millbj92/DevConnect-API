using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevConnectAPI.Models
{
    public class UserLike
    {
        [Key]
        public int like_id { get; set; }
        public User User { get; set; }
    }
}
