using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Games.Models
{
    public class GamePlatform
    {
        [Key]
        public int GamePlatformID { get; set; }
        public Game game { get; set; }
        public int GameID { get; set; }
        public Platform platform { get; set; }
        public int PlatformID { get; set; }
    }
}
