using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Games.Models
{
    public class GameTaal
    {
        [Key]
        public int GameTaalID { get; set; }
        public Game game { get; set; }
        public int GameID { get; set; }
        public Taal taal { get; set; }
        public int TaalID { get; set; }
    }
}
