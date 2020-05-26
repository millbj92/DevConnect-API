using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevConnectAPI.Models
{
    public class Photo
    {
        [Key]
        public int photo_id { get; set; }
        public PhotoAlbum? Album { get; set; }
        public int album_id { get; set; }
        public string? caption { get; set; }
        public DateTime Created { get; set; }
        public string img_url { get; set; }
        public User owner { get; set; }
    }
}
