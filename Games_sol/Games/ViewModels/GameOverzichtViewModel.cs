using Games.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games.ViewModels
{
    public class GameOverzichtViewModel
    {
#nullable enable
        public string? Beschrijving { get; set; }
#nullable disable
        public int GameID { get; set; }
        public bool filterActive { get; set; }
        public string Afbeelding { get; set; }
        public string Naam { get; set; }
        public List<Game> games;
        public int startIndex { get; set; }
        public int currentPage { get; set; }
        public int amountToGet { get; set; }
        public int totalItems { get; set; }
        public int amountOfPages { get; set; }
        public List<int> pageNumbers { get; set; }
        public List<Game> gamesToShow;
        public List<Platform> platforms;
        public List<GamePlatform> gamePlatforms;
        public List<Genre> genres;
        public List<SpelModus> spelModussen;
        public List<DateTime> jaartallen;

        [BindProperty]
        public List<int> PlatformsAreChecked { get; set; }
        [BindProperty]
        public List<int> GenresAreChecked { get; set; }
        [BindProperty]
        public List<int> SpelmodussenAreChecked { get; set; }
        [BindProperty]
        public List<DateTime> JaartallenAreChecked { get; set; }

    }
}
