using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevConnectAPI.Models
{
    public class PhotoAlbum
    {
        [Key]
        public int album_id { get; set; }
        public Photo? CoverPhoto { get; set; }
        public DateTime created { get; set; }
        public string? Description { get; set; }
        public DateTime? Modified { get; set; }
        public string Name { get; set; }
        public User owner { get; set; }
    }
}
