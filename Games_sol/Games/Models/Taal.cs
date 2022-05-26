using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Games.Models
{
    public class Taal
    {
        [Key]
        public int TaalID { get; set; }
        public string Naam { get; set; }
    }
}
