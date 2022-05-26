using Games.Data;
using Games.Models;
using Games.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Games.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        List<Game> games = new List<Game>();
        public ApplicationDbContext _applicationDbContext { get; set; }
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }

        public Task<IActionResult> Index(GameOverzichtViewModel viewModel)
        {
            if (viewModel == null)
            {
                viewModel = new GameOverzichtViewModel();
            }
            return GameOverzicht(viewModel);
            //return View();
        }
        public async Task<IActionResult> GameDetail(int id)
        {
            IdentityUser identityUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Email == this.User.Identity.Name);
            Game game = await _applicationDbContext.Games.Include(x => x.leeftijdCategorie).FirstOrDefaultAsync(x => x.GameID == id);
            GameUser? gameUser = await _applicationDbContext.GameUsers.FirstOrDefaultAsync(x => x.Game == game && x.User == identityUser);
            GameDetailViewModel gameDetailViewModel = new GameDetailViewModel(game);
            if (gameUser == null)
            {
                gameDetailViewModel.WishlistIcon = "far";
                gameDetailViewModel.CollectionIcon = "far";
            }
            else if (gameUser.Owned == false)
            {
                gameDetailViewModel.WishlistIcon = "fas";
                gameDetailViewModel.CollectionIcon = "far";
            }
            else if (gameUser.Owned == true)
            {
                gameDetailViewModel.WishlistIcon = "far";
                gameDetailViewModel.CollectionIcon = "fas";
            }
            gameDetailViewModel.Id = id;
            gameDetailViewModel.gamePlatforms = await _applicationDbContext.GamePlatforms.Include(x => x.platform).Where(x => x.GameID == id).ToListAsync();
            gameDetailViewModel.gameGenres = await _applicationDbContext.GameGenres.Include(x => x.genre).Where(x => x.GameID == id).ToListAsync();
            gameDetailViewModel.gameSpelModussen = await _applicationDbContext.GameSpelModussen.Include(x => x.spelModus).Where(x => x.GameID == id).ToListAsync();
            gameDetailViewModel.gameTalen = await _applicationDbContext.GameTalen.Include(x => x.taal).Where(x => x.GameID == id).ToListAsync();
            return View(gameDetailViewModel);
        }
        public IActionResult Info()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Wishlist(int id)
        {
            GameUser gameUser = null;
            IdentityUser identityUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Email == this.User.Identity.Name);
            Game game = await _applicationDbContext.Games.Include(x => x.leeftijdCategorie).FirstOrDefaultAsync(x => x.GameID == id);
            GameUser? ExistingGameUser = await _applicationDbContext.GameUsers.FirstOrDefaultAsync(x => x.Game == game && x.User == identityUser);
            gameUser = new GameUser(identityUser, game, false);
            bool Exists = true;
            if (ExistingGameUser != null)
            {
                if (ExistingGameUser?.Game.GameID != gameUser.Game.GameID && ExistingGameUser?.User.UserName != gameUser.User.UserName)
                {
                    Exists = false;
                }
            }
            else Exists = false;
            if (identityUser != null)
            {
                if (!Exists)
                {
                    _applicationDbContext.GameUsers.Add(gameUser);
                    _applicationDbContext.SaveChanges();
                }
                else
                {
                    if (ExistingGameUser.Owned == true)
                    {
                        ExistingGameUser.Owned = false;
                        _applicationDbContext.GameUsers.Update(ExistingGameUser);
                        _applicationDbContext.SaveChanges();
                    }
                    else
                    {
                        _applicationDbContext.GameUsers.Remove(ExistingGameUser);
                        _applicationDbContext.SaveChanges();
                    }
                }
            }
            return RedirectToAction("GameDetail", new { id = id });
        }
        [HttpPost]
        public async Task<IActionResult> Collection(int id)
        {
            GameUser gameUser = null;
            IdentityUser identityUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Email == this.User.Identity.Name);
            Game game = await _applicationDbContext.Games.Include(x => x.leeftijdCategorie).FirstOrDefaultAsync(x => x.GameID == id);
            GameUser? ExistingGameUser = await _applicationDbContext.GameUsers.FirstOrDefaultAsync(x => x.Game == game && x.User == identityUser);
            gameUser = new GameUser(identityUser, game, true);
            bool Exists = true;

            if (ExistingGameUser != null)
            {
                if (ExistingGameUser?.Game.GameID != gameUser.Game.GameID && ExistingGameUser?.User.UserName != gameUser.User.UserName)
                {
                    Exists = false;
                }
            }
            else Exists = false;
            if (identityUser != null)
            {
                if (!Exists)
                {
                    _applicationDbContext.GameUsers.Add(gameUser);
                    _applicationDbContext.SaveChanges();
                }
                else
                {
                    if (ExistingGameUser.Owned == false)
                    {
                        ExistingGameUser.Owned = true;
                        _applicationDbContext.GameUsers.Update(ExistingGameUser);
                        _applicationDbContext.SaveChanges();
                    }
                    else
                    {
                        _applicationDbContext.GameUsers.Remove(ExistingGameUser);
                        _applicationDbContext.SaveChanges();
                    }
                }
            }
            return RedirectToAction("GameDetail", new { id = id });
        }

        [HttpPost]
        public async Task<IActionResult> GameOverzicht(GameOverzichtViewModel viewModel)
        {
            viewModel.amountToGet = 6;        
            viewModel = await FillGameOverzichtViewmodel(viewModel);
            viewModel.games = _applicationDbContext.Games.ToList();
            viewModel.totalItems = viewModel.games.Count();
            double calc = viewModel.totalItems / viewModel.amountToGet;
            viewModel.pageNumbers = new List<int>();
            viewModel.amountOfPages = Convert.ToInt32(Math.Floor(calc));
            for (int i = 1; i <= viewModel.amountOfPages + 1; i++)
            {
                viewModel.pageNumbers.Add(i);
            }
            if (viewModel.currentPage == 0) viewModel.currentPage = 1;
            viewModel.startIndex = viewModel.currentPage * viewModel.amountToGet - 6;
            viewModel.gamesToShow = _applicationDbContext.Games.Skip(viewModel.startIndex).Take(viewModel.amountToGet).ToList();
            return View("Index", viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Filter(GameOverzichtViewModel viewModel)
        {            
            List<Game> games = await _applicationDbContext.Games.ToListAsync();
            List<int> amountGames = new List<int>();
            for (int i = 0; i < games.Count; i++)
            {
                amountGames.Add(games[i].GameID);
            }
            List<int> newGamePlatforms = new List<int>();
            List<int> newgameGenres = new List<int>();
            List<int> newGameSpelmodussen = new List<int>();
            List<int> newgamesYear = new List<int>();
            if (viewModel.PlatformsAreChecked != null)
            {
                List<GamePlatform> gameplatforms = _applicationDbContext.GamePlatforms.ToList();
                foreach (int id in viewModel.PlatformsAreChecked)
                {
                    foreach (GamePlatform gamePlatform in gameplatforms)
                    {
                        if (gamePlatform.PlatformID == id)
                        {
                            newGamePlatforms.Add(gamePlatform.GameID);
                        }
                    }
                }
            }
            else newGamePlatforms = amountGames;
            if (viewModel.GenresAreChecked != null)
            {
                List<GameGenre> gameGenres = _applicationDbContext.GameGenres.ToList();

                foreach (int id in viewModel.GenresAreChecked)
                {
                    foreach (GameGenre gameGenre in gameGenres)
                    {
                        if (gameGenre.GenreID == id)
                        {
                            newgameGenres.Add(gameGenre.GameID);
                        }
                    }
                }
            }
            else newgameGenres = amountGames;
            if (viewModel.JaartallenAreChecked != null)
            {
                foreach (DateTime date in viewModel.JaartallenAreChecked)
                {
                    foreach (Game game in games)
                    {
                        if (game.ReleaseDatum is DateTime dateTime)
                        {
                            if (dateTime.Year == date.Year)
                            {
                                newgamesYear.Add(game.GameID);
                            }
                        }
                    }
                }
            }
            else newgamesYear = amountGames;
            if (viewModel.SpelmodussenAreChecked != null)
            {
                List<GameSpelModus> gameSpelmodussen = _applicationDbContext.GameSpelModussen.ToList();
                foreach (int id in viewModel.SpelmodussenAreChecked)
                {
                    foreach (GameSpelModus gameSpelModus in gameSpelmodussen)
                    {

                        if (gameSpelModus.SpelModusID == id)
                        {
                            newGameSpelmodussen.Add(gameSpelModus.GameID);
                        }
                    }
                }
            }
            else newGameSpelmodussen = amountGames;
            List<int> filteredGameIDs = newgamesYear.Intersect(newGameSpelmodussen.Intersect(newgameGenres.Intersect(newGamePlatforms))).ToList();
            viewModel.games = new List<Game>();
            foreach (Game game in games)
            {
                if (filteredGameIDs.Contains(game.GameID))
                {
                    viewModel.games.Add(game);
                }
            }
            viewModel.filterActive = true;
            viewModel.gamesToShow = viewModel.games;
            viewModel = await FillGameOverzichtViewmodel(viewModel);
            return View("Index", viewModel);
        }
        public async Task<GameOverzichtViewModel> FillGameOverzichtViewmodel(GameOverzichtViewModel viewModel)
        {
            viewModel.gamePlatforms = await _applicationDbContext.GamePlatforms.ToListAsync();
            viewModel.platforms = await _applicationDbContext.Platformen.ToListAsync();
            viewModel.spelModussen = await _applicationDbContext.Spelmodussen.ToListAsync();
            viewModel.jaartallen = new List<DateTime>();
            for (var dt = new DateTime(2014, 1, 1); dt <= new DateTime(2023, 1, 1); dt = dt.AddYears(1))
            {
                viewModel.jaartallen.Add(dt);
            }
            viewModel.genres = _applicationDbContext.Genres.ToList();
            return viewModel;
        }
    }
}
