using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevConnectAPI.Models
{
    public class PostComment
    {
        [Key]
        public int comment_id { get; set; }
        public string text { get; set; }
        public Post post { get; set; }
        public User Owner { get; set; }
        public List<UserLike>? Likes { get; set; }
        public DateTime Created { get; set; }
    }
}
