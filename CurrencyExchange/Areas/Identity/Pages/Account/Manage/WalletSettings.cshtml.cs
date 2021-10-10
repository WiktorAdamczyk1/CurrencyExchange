using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CurrencyExchange.Areas.Identity.Pages.Account.Manage
{
    public class WalletSettingsModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserWalletsData _userWallet;

        public WalletSettingsModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IUserWalletsData userWallet)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userWallet = userWallet;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "PLN")]
            public int PLN { get; set; }

            [Required]
            [Display(Name = "USD")]
            public int USD { get; set; }

            [Required]
            [Display(Name = "EUR")]
            public int EUR { get; set; }

            [Required]
            [Display(Name = "CHF")]
            public int CHF { get; set; }

            [Required]
            [Display(Name = "RUB")]
            public int RUB { get; set; }

            [Required]
            [Display(Name = "CZK")]
            public int CZK { get; set; }

            [Required]
            [Display(Name = "GBP")]
            public int GBP { get; set; }

        }

        private async Task LoadAsync(IdentityUser user)
        {
            var wallet = await _userWallet.GetUserWallet(_userManager.GetUserId(User));

            Input = new InputModel
            {
                PLN = Convert.ToInt32(wallet.PLN),
                USD = wallet.USD,
                EUR = wallet.EUR,
                CHF = wallet.CHF,
                RUB = wallet.RUB,
                CZK = wallet.CZK,
                GBP = wallet.GBP

            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            UserWalletsModel userWalletData = new UserWalletsModel()
            {
                UserId = _userManager.GetUserId(User),
                PLN = Input.PLN,
                USD = Input.USD,
                EUR = Input.EUR,
                CHF = Input.CHF,
                RUB = Input.RUB,
                CZK = Input.CZK,
                GBP = Input.GBP
            };

            await _userWallet.UpdateUserWalletBalance(userWalletData);
            

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your wallet has been updated";
            return RedirectToPage();
        }
    }
}
