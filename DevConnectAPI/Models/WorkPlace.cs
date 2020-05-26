using DevConnectAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DevConnectAPI
{
    public class WorkPlace
    {
        [Key]
        public int workplace_id { get; set; }
        public Location WorkLocation { get; set; }
        public string CompanyName { get; set; }
        public DateTime? EndDate { get; set; }
        public string Position { get; set; }
        public DateTime StartDate { get; set; }
    }
}
