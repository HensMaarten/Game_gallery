using Games.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games.ViewModels
{
    public class BewerkGameViewModel
    {     
        public bool IsNew { get; set; } = true;

        [BindProperty]
        public int? gameId { get; set; }
        public string gameAfbeelding { get; set; }
        public DateTime? gameDate { get; set; }
        public string gameDateString { get; set; }
        public Boolean digitalonly { get; set; }
        public string gameDescription { get; set;}
        public string gameReview { get; set; }
        public Game Game { get; set; } = new Game();
        public string GameNaam { get; set; }
        public List<Taal> Talen { get; set; }
        public List<GameTaal> GameTalen { get; set; } = new List<GameTaal>();
        public List<SpelModus> Spelmodussen{ get; set; }
        public List<GameSpelModus> GameSpelmodussen { get; set; } = new List<GameSpelModus>();
        public List<Platform> Platformen { get; set; }
        public List<Genre> SelectedGenres { get; set; } = new List<Genre>();
        public List<SpelModus> SelectedSpelmodussen { get; set; } = new List<SpelModus>();
        public List<Taal> SelectedTalen { get; set; } = new List<Taal>();

        public List<LeeftijdCategorie> leeftijdCategorieën { get; set; }

        public List<Platform> SelectedPlatformen { get; set; } = new List<Platform>();
        public List<GamePlatform> GamePlatformen { get; set; } = new List<GamePlatform>();
        public List<Genre> Genres { get; set; }
        public List<GameGenre> GameGenres { get; set; } = new List<GameGenre>();
    }
}
