using Games.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games.ViewModels
{
  public class GameDetailViewModel
  {
    public Game game;
    public int Id;
    public List<GamePlatform> gamePlatforms;
    public List<GameGenre> gameGenres;
    public List<GameTaal> gameTalen;
    public List<GameSpelModus> gameSpelModussen;
    public string WishlistIcon;
    public string CollectionIcon;
#nullable enable
    public IdentityUser? identityUser;
    public GameDetailViewModel(Game game, IdentityUser identityUser = null)
    {
      this.game = game;
    }
    public GameDetailViewModel()
    {

    }
  }
}
