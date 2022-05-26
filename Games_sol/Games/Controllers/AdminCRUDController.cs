using Games.Data;
using Games.Models;
using Games.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games.Controllers
{
    public class AdminCRUDController : Controller
    {
        private readonly ILogger<AdminCRUDController> _logger;
        public ApplicationDbContext _applicationDbContext { get; set; }
        // GET: AdminCRUDController
        public AdminCRUDController(ILogger<AdminCRUDController> logger, ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }        
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await configureAdminCrudViewModel());
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BewerkGame(int? id = null)
        {
            BewerkGameViewModel bewerkGameViewModel = new BewerkGameViewModel();          
            bewerkGameViewModel.leeftijdCategorieën = await _applicationDbContext.LeeftijdCategorieën.ToListAsync();
            bewerkGameViewModel.Platformen = await _applicationDbContext.Platformen.ToListAsync();
            bewerkGameViewModel.Genres = await _applicationDbContext.Genres.ToListAsync();
            bewerkGameViewModel.Spelmodussen = await _applicationDbContext.Spelmodussen.ToListAsync();
            bewerkGameViewModel.Talen = await _applicationDbContext.Talen.ToListAsync();
            bewerkGameViewModel.gameId = id;          
            if (id != null)
            {
                bewerkGameViewModel.IsNew = false;
                bewerkGameViewModel.Game = await _applicationDbContext.Games.Where(x => x.GameID == id).Include(x => x.leeftijdCategorie).FirstOrDefaultAsync();             
                bewerkGameViewModel.GameNaam = bewerkGameViewModel.Game.Naam;
                bewerkGameViewModel.GameGenres = await _applicationDbContext.GameGenres.Include(x => x.genre).Where(x => x.GameID == id).ToListAsync();
                bewerkGameViewModel.gameDate = bewerkGameViewModel.Game.ReleaseDatum;
                foreach (GameGenre gameGenre in bewerkGameViewModel.GameGenres)
                {
                    foreach (Genre genre in bewerkGameViewModel.Genres)
                    {
                        if (gameGenre.GenreID == genre.GenreID)
                        {
                            bewerkGameViewModel.SelectedGenres.Add(genre);
                        }
                    }
                }                
                bewerkGameViewModel.GamePlatformen = await _applicationDbContext.GamePlatforms.Include(x => x.platform).Where(x => x.GameID == id).ToListAsync();
                foreach (GamePlatform gamePlatform in bewerkGameViewModel.GamePlatformen)
                {
                    foreach (Platform platform in bewerkGameViewModel.Platformen)
                    {
                        if (gamePlatform.PlatformID == platform.PlatformID)
                        {
                            bewerkGameViewModel.SelectedPlatformen.Add(platform);
                        }
                    }
                }              
                bewerkGameViewModel.GameSpelmodussen = await _applicationDbContext.GameSpelModussen.Include(x => x.spelModus).Where(x => x.GameID == id).ToListAsync();
                foreach (GameSpelModus gameSpelModus in bewerkGameViewModel.GameSpelmodussen)
                {
                    foreach (SpelModus spelmodus in bewerkGameViewModel.Spelmodussen)
                    {
                        if (gameSpelModus.SpelModusID == spelmodus.SpelModusID)
                        {
                            bewerkGameViewModel.SelectedSpelmodussen.Add(spelmodus);
                        }
                    }
                }               
                bewerkGameViewModel.GameTalen = await _applicationDbContext.GameTalen.Include(x => x.taal).Where(x => x.GameID == id).ToListAsync();
                foreach (GameTaal gameTaal in bewerkGameViewModel.GameTalen)
                {
                    foreach (Taal taal in bewerkGameViewModel.Talen)
                    {
                        if (gameTaal.TaalID == taal.TaalID)
                        {
                            bewerkGameViewModel.SelectedTalen.Add(taal);
                        }
                    }
                }
            }
            return View(bewerkGameViewModel);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(BewerkGameViewModel bewerkGameViewModel)
        {
            Game gameToEdit = await _applicationDbContext.Games.Where(x => x.GameID == bewerkGameViewModel.gameId).Include(x => x.leeftijdCategorie).FirstOrDefaultAsync();
            int.TryParse(Request.Form["LeeftijdsCategorieën"], out int leeftijdsCategorieId);
            string talenString = Request.Form["Talen"];
            string[] talenStringList = talenString.Split(',');
            List<GameTaal> gameTalenToAdd = new List<GameTaal>();
            foreach (string taal in talenStringList)
            {
                int.TryParse(taal, out int taalId);
                Taal taalToAdd = await _applicationDbContext.Talen.Where(x => x.TaalID == taalId).FirstOrDefaultAsync();
                GameTaal gametaal = new GameTaal();
                gametaal.game = gameToEdit;
                gametaal.GameID = gameToEdit.GameID;
                gametaal.taal = taalToAdd;
                gametaal.TaalID = taalToAdd.TaalID;
                gameTalenToAdd.Add(gametaal);
            }
            string spelmodussenString = Request.Form["Spelmodussen"];
            string[] spelmodussenStringList = spelmodussenString.Split(',');
            List<GameSpelModus> gameSpelModussenToAdd = new List<GameSpelModus>();
            foreach (string spelmodus in spelmodussenStringList)
            {
                int.TryParse(spelmodus, out int spelmodusId);
                SpelModus spelmodusToAdd = await _applicationDbContext.Spelmodussen.Where(x => x.SpelModusID == spelmodusId).FirstOrDefaultAsync();
                GameSpelModus gameSpelModus = new GameSpelModus();
                gameSpelModus.game = gameToEdit;
                gameSpelModus.GameID = gameToEdit.GameID;
                gameSpelModus.spelModus = spelmodusToAdd;
                gameSpelModus.SpelModusID = spelmodusToAdd.SpelModusID;
                gameSpelModussenToAdd.Add(gameSpelModus);
            }
            string genresString = Request.Form["Genres"];
            string[] genresStringList = genresString.Split(',');
            List<GameGenre> gameGenresToAdd = new List<GameGenre>();
            foreach (string genre in genresStringList)
            {
                int.TryParse(genre, out int genreId);
                Genre genreToAdd = await _applicationDbContext.Genres.Where(x => x.GenreID == genreId).FirstOrDefaultAsync();
                GameGenre gameGenre = new GameGenre();
                gameGenre.game = gameToEdit;
                gameGenre.GameID = gameToEdit.GameID;
                gameGenre.genre = genreToAdd;
                gameGenre.GenreID = genreToAdd.GenreID;
                gameGenresToAdd.Add(gameGenre);
            }
            string platformenString = Request.Form["Platformen"];
            string[] platformenStringList = platformenString.Split(',');
            List<GamePlatform> gamePlatformsToAdd = new List<GamePlatform>();
            foreach (string platform in platformenStringList)
            {
                int.TryParse(platform, out int platformId);
                Platform platformToAdd = await _applicationDbContext.Platformen.Where(x => x.PlatformID == platformId).FirstOrDefaultAsync();
                GamePlatform gamePlatform = new GamePlatform();
                gamePlatform.game = gameToEdit;
                gamePlatform.GameID = gameToEdit.GameID;
                gamePlatform.platform = platformToAdd;
                gamePlatform.PlatformID = platformToAdd.PlatformID;
                gamePlatformsToAdd.Add(gamePlatform);
            }
            List<GameTaal> gametalen = await _applicationDbContext.GameTalen.Where(x => x.GameID == gameToEdit.GameID).ToListAsync();
            List<GameSpelModus> gameSpelModussen = await _applicationDbContext.GameSpelModussen.Where(x => x.GameID == gameToEdit.GameID).ToListAsync();
            List<GameGenre> gameGenres = await _applicationDbContext.GameGenres.Where(x => x.GameID == gameToEdit.GameID).ToListAsync();
            List<GamePlatform> gamePlatforms = await _applicationDbContext.GamePlatforms.Where(x => x.GameID == gameToEdit.GameID).ToListAsync();
            gametalen.ForEach(x => _applicationDbContext.Remove(x));
            gameSpelModussen.ForEach(x => _applicationDbContext.Remove(x));
            gameGenres.ForEach(x => _applicationDbContext.Remove(x));
            gamePlatforms.ForEach(x => _applicationDbContext.Remove(x));
            LeeftijdCategorie gameLeeftijdCategorie = await _applicationDbContext.LeeftijdCategorieën.Where(x => x.LeeftijdCategorieID == leeftijdsCategorieId).FirstOrDefaultAsync();
            gameToEdit.Naam = bewerkGameViewModel.GameNaam;
            gameToEdit.Afbeelding = bewerkGameViewModel.gameAfbeelding;
            gameToEdit.Beschrijving = bewerkGameViewModel.gameDescription;
            gameToEdit.leeftijdCategorieID = leeftijdsCategorieId;
            gameToEdit.leeftijdCategorie = gameLeeftijdCategorie;
            gameToEdit.Review = bewerkGameViewModel.gameReview;
            gameToEdit.ReleaseDatum = bewerkGameViewModel.gameDate;
            gameToEdit.DigitalOnly = bewerkGameViewModel.digitalonly;
            _applicationDbContext.Update(gameToEdit);
            gameTalenToAdd.ForEach(x => _applicationDbContext.Add(x));
            gameSpelModussenToAdd.ForEach(x => _applicationDbContext.Add(x));
            gameGenresToAdd.ForEach(x => _applicationDbContext.Add(x));
            gamePlatformsToAdd.ForEach(x => _applicationDbContext.Add(x));
            await _applicationDbContext.SaveChangesAsync();
            return View("Index", await configureAdminCrudViewModel());
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(BewerkGameViewModel bewerkGameViewModel)
        {
            Game gameToDelete = await _applicationDbContext.Games.Where(x => x.GameID == bewerkGameViewModel.gameId).Include(x => x.leeftijdCategorie).FirstOrDefaultAsync();
            _applicationDbContext.Games.Remove(gameToDelete);
            await _applicationDbContext.SaveChangesAsync();
            return View("Index", await configureAdminCrudViewModel());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(BewerkGameViewModel bewerkGameViewModel)
        {
            Game NewGame = new Game();
            int.TryParse(Request.Form["LeeftijdsCategorieën"], out int leeftijdsCategorieId);
            LeeftijdCategorie gameLeeftijdCategorie = await _applicationDbContext.LeeftijdCategorieën.Where(x => x.LeeftijdCategorieID == leeftijdsCategorieId).FirstOrDefaultAsync();
            NewGame.Naam = bewerkGameViewModel.GameNaam;
            NewGame.Afbeelding = bewerkGameViewModel.gameAfbeelding;
            NewGame.Beschrijving = bewerkGameViewModel.gameDescription;
            NewGame.leeftijdCategorieID = leeftijdsCategorieId;
            NewGame.leeftijdCategorie = gameLeeftijdCategorie;
            NewGame.Review = bewerkGameViewModel.gameReview;
            NewGame.ReleaseDatum = bewerkGameViewModel.gameDate;
            NewGame.DigitalOnly = bewerkGameViewModel.digitalonly;
            _applicationDbContext.Add(NewGame);
            await _applicationDbContext.SaveChangesAsync();
            int test = NewGame.GameID;
            string talenString = Request.Form["Talen"];
            string[] talenStringList = talenString.Split(',');
            List<GameTaal> gameTalenToAdd = new List<GameTaal>();
            foreach (string taal in talenStringList)
            {
                int.TryParse(taal, out int taalId);
                Taal taalToAdd = await _applicationDbContext.Talen.Where(x => x.TaalID == taalId).FirstOrDefaultAsync();
                GameTaal gametaal = new GameTaal();
                gametaal.game = NewGame;
                gametaal.GameID = NewGame.GameID;
                gametaal.taal = taalToAdd;
                gametaal.TaalID = taalToAdd.TaalID;
                gameTalenToAdd.Add(gametaal);
            }
            string spelmodussenString = Request.Form["Spelmodussen"];
            string[] spelmodussenStringList = spelmodussenString.Split(',');
            List<GameSpelModus> gameSpelModussenToAdd = new List<GameSpelModus>();
            foreach (string spelmodus in spelmodussenStringList)
            {
                int.TryParse(spelmodus, out int spelmodusId);
                SpelModus spelmodusToAdd = await _applicationDbContext.Spelmodussen.Where(x => x.SpelModusID == spelmodusId).FirstOrDefaultAsync();
                GameSpelModus gameSpelModus = new GameSpelModus();
                gameSpelModus.game = NewGame;
                gameSpelModus.GameID = NewGame.GameID;
                gameSpelModus.spelModus = spelmodusToAdd;
                gameSpelModus.SpelModusID = spelmodusToAdd.SpelModusID;
                gameSpelModussenToAdd.Add(gameSpelModus);
            }
            string genresString = Request.Form["Genres"];
            string[] genresStringList = genresString.Split(',');
            List<GameGenre> gameGenresToAdd = new List<GameGenre>();
            foreach (string genre in genresStringList)
            {
                int.TryParse(genre, out int genreId);
                Genre genreToAdd = await _applicationDbContext.Genres.Where(x => x.GenreID == genreId).FirstOrDefaultAsync();
                GameGenre gameGenre = new GameGenre();
                gameGenre.game = NewGame;
                gameGenre.GameID = NewGame.GameID;
                gameGenre.genre = genreToAdd;
                gameGenre.GenreID = genreToAdd.GenreID;
                gameGenresToAdd.Add(gameGenre);
            }
            string platformenString = Request.Form["Platformen"];
            string[] platformenStringList = platformenString.Split(',');
            List<GamePlatform> gamePlatformsToAdd = new List<GamePlatform>();
            foreach (string platform in platformenStringList)
            {
                int.TryParse(platform, out int platformId);
                Platform platformToAdd = await _applicationDbContext.Platformen.Where(x => x.PlatformID == platformId).FirstOrDefaultAsync();
                GamePlatform gamePlatform = new GamePlatform();
                gamePlatform.game = NewGame;
                gamePlatform.GameID = NewGame.GameID;
                gamePlatform.platform = platformToAdd;
                gamePlatform.PlatformID = platformToAdd.PlatformID;
                gamePlatformsToAdd.Add(gamePlatform);
            }
            gameTalenToAdd.ForEach(x => _applicationDbContext.Add(x));
            gameSpelModussenToAdd.ForEach(x => _applicationDbContext.Add(x));
            gameGenresToAdd.ForEach(x => _applicationDbContext.Add(x));
            gamePlatformsToAdd.ForEach(x => _applicationDbContext.Add(x));
            await _applicationDbContext.SaveChangesAsync();
            return View("Index", await configureAdminCrudViewModel());
        }      
        public async Task<AdminCRUDViewModel> configureAdminCrudViewModel(int pageNumber = 1)
        {
            AdminCRUDViewModel adminCRUDViewModel = new AdminCRUDViewModel();
            adminCRUDViewModel.games = await _applicationDbContext.Games.Include(x => x.leeftijdCategorie).ToListAsync();
            adminCRUDViewModel.amountToGet = 6;
            adminCRUDViewModel.totalItems = adminCRUDViewModel.games.Count();
            double calc = adminCRUDViewModel.totalItems / adminCRUDViewModel.amountToGet;
            adminCRUDViewModel.pageNumbers = new List<int>();
            adminCRUDViewModel.amountOfPages = Convert.ToInt32(Math.Floor(calc));
            for (int i = 1; i <= adminCRUDViewModel.amountOfPages + 1; i++)
            {
                adminCRUDViewModel.pageNumbers.Add(i);
            }
            adminCRUDViewModel.currentPage = pageNumber;
            adminCRUDViewModel.startIndex = adminCRUDViewModel.currentPage * adminCRUDViewModel.amountToGet - 6;
            adminCRUDViewModel.gamesToShow = _applicationDbContext.Games.Skip(adminCRUDViewModel.startIndex).Take(adminCRUDViewModel.amountToGet).ToList();
            return adminCRUDViewModel;
        }
        [HttpPost]
        public async Task<IActionResult> ChangePage(AdminCRUDViewModel adminCRUDViewModel)
        {
            return View("Index", await configureAdminCrudViewModel(adminCRUDViewModel.currentPage));
        }
    }
}
