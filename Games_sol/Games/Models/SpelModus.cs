using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Games.Models
{
    public class SpelModus
    {
        [Key]
        public int SpelModusID { get; set; }
        public string Naam { get; set; }
    }
}
