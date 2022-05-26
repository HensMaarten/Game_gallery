using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Games.Data;
using Games.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Games.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        public ApplicationDbContext _applicationDbContext { get; set; }
        private List<GameUser> _gameUsers;        
        public List<GameUser> GameUsers
        {
            get { return _gameUsers; }
            set { _gameUsers = value; }
        }
        private List<GameUser> _collectionUser;
        public List<GameUser> CollectionUser
        {
            get { return _collectionUser; }
            set { _collectionUser = value; }
        }
        private List<GameUser> _wishlistUser;
        public List<GameUser> WishlistUser
        {
            get { return _wishlistUser; }
            set { _wishlistUser = value; }
        }

        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<PersonalDataModel> _logger;

        public PersonalDataModel(
            UserManager<IdentityUser> userManager,
            ILogger<PersonalDataModel> logger, ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            else
            {
                GameUsers = new List<GameUser>();
                CollectionUser = new List<GameUser>();
                WishlistUser = new List<GameUser>();
                GameUsers = await _applicationDbContext.GameUsers.Include(x => x.Game).Where(x => x.User.Id == user.Id).ToListAsync();
                CollectionUser = GameUsers.Where(x => x.Owned == true).ToList();
                WishlistUser = GameUsers.Where(x => x.Owned == false).ToList();
            }

            return Page();
        }
    }
}