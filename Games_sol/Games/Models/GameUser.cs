using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Games.Models
{
    public class GameUser
    {
        [Key]
        public int GameUserID { get; set; }
        public IdentityUser User { get; set; }
        public Game Game { get; set; }
        public bool Uitgespeeld { get; set; }
        public bool Owned { get; set; }
        public GameUser()
        {

        }
        public GameUser(IdentityUser user, Game game, bool owned = false)
        {
            this.User = user;
            this.Game = game;
            this.Owned = owned;
        }
    public override bool Equals(object obj)
    {
      if (obj is GameUser gameUser)
      {
        if (this.User == gameUser.User && this.Game == gameUser.Game)
        {
          return true;
        }
        else return false;
      }
      else return false;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(User, Game);
    }
  }
}
