﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Games.Models
{
    public class GameGenre
    {
        [Key]
        public int GameGenreID{ get; set; }
        public Game game { get; set; }
        public int GameID { get; set; }
        public Genre genre { get; set; }
        public int GenreID { get; set; }
    }
}
