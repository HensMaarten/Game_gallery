using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Games.Models
{
    public class GameSpelModus
    {
        [Key]
        public int GameSpelModusID { get; set; }
        public Game game { get; set; }
        public int GameID { get; set; }
        public SpelModus spelModus { get; set; }
        public int SpelModusID { get; set; }
    }
}
