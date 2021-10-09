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
    public partial class CurrencySettings : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserCurrencySettingsData _userCurrencySettingsData;

        public CurrencySettings(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IUserCurrencySettingsData userCurrencySettingsData)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userCurrencySettingsData = userCurrencySettingsData;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {

            [Required]
            [Display(Name = "USD")]
            public bool USD { get; set; }

            [Required]
            [Display(Name = "EUR")]
            public bool EUR { get; set; }

            [Required]
            [Display(Name = "CHF")]
            public bool CHF { get; set; }

            [Required]
            [Display(Name = "RUB")]
            public bool RUB { get; set; }

            [Required]
            [Display(Name = "CZK")]
            public bool CZK { get; set; }

            [Required]
            [Display(Name = "GBP")]
            public bool GBP { get; set; }

        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userCurrencySettings = await _userCurrencySettingsData.GetUserCurrencySettings(_userManager.GetUserId(User));

            Input = new InputModel
            {
                USD = Convert.ToBoolean(userCurrencySettings.USD),
                EUR = Convert.ToBoolean(userCurrencySettings.EUR),
                CHF = Convert.ToBoolean(userCurrencySettings.CHF),
                RUB = Convert.ToBoolean(userCurrencySettings.RUB),
                CZK = Convert.ToBoolean(userCurrencySettings.CZK),
                GBP = Convert.ToBoolean(userCurrencySettings.GBP)

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

            UserCurrencySettingsModel userCurrencySettingsNew = new UserCurrencySettingsModel
            {
                USD = Input.USD,
                EUR = Input.EUR,
                CHF = Input.CHF,
                RUB = Input.RUB,
                CZK = Input.CZK,
                GBP = Input.GBP
            };
            var userCurrencySettingsOld = await _userCurrencySettingsData.GetUserCurrencySettings(_userManager.GetUserId(User));
            if (userCurrencySettingsNew != userCurrencySettingsOld)
            {
                await _userCurrencySettingsData.UpdateUserCurrencySettings(userCurrencySettingsNew);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
