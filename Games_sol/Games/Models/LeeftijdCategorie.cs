using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Games.Models
{
    public class LeeftijdCategorie
    {
        [Key]
        public int LeeftijdCategorieID { get; set; }
        public string naam { get; set; }
    }
}
