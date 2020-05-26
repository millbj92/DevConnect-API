using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevConnectAPI.Models
{
    public class Post
    {
        [Key]
        public int post_id { get; set; }
        public string text { get; set; }
        public List<UserLike>? likes { get; set; }
        public List<Photo>? images { get; set; }
        public DateTime Created { get; set; }
        public User Owner { get; set; }
    }
}
