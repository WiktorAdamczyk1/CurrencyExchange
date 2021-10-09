using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using DataAccessLibrary;
using DataAccessLibrary.Models;
namespace CurrencyExchange.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IAspNetUsersData _aspNetUsersData;
        private readonly IUserWalletsData _userWalletData;
        private readonly IUserCurrencySettingsData _userCurrencySettingsData;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IAspNetUsersData aspNetUsersData,
            IUserWalletsData userWalletData,
            IUserCurrencySettingsData userCurrencySettingsData)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _aspNetUsersData = aspNetUsersData;
            _userWalletData = userWalletData;
            _userCurrencySettingsData = userCurrencySettingsData;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            // Initial userWallet values
            [Required]
            [DataType(DataType.Currency)]
            [Display(Name = "PLN")]
            public int PLN { get; set; }

            [Required]
            [DataType(DataType.Currency)]
            [Display(Name = "USD")]
            public int USD { get; set; }

            [Required]
            [DataType(DataType.Currency)]
            [Display(Name = "EUR")]
            public int EUR { get; set; }

            [Required]
            [DataType(DataType.Currency)]
            [Display(Name = "CHF")]
            public int CHF { get; set; }

            [Required]
            [DataType(DataType.Currency)]
            [Display(Name = "RUB")]
            public int RUB { get; set; }

            [Required]
            [DataType(DataType.Currency)]
            [Display(Name = "CZK")]
            public int CZK { get; set; }

            [Required]
            [DataType(DataType.Currency)]
            [Display(Name = "GBP")]
            public int GBP { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };

                UserWalletsModel userWallet = new UserWalletsModel { PLN = Input.PLN, CHF = Input.CHF, CZK = Input.CZK,
                    EUR = Input.EUR, GBP = Input.GBP, RUB = Input.RUB, USD = Input.USD };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    // Insert UserWallet
                    userWallet.UserId = await _aspNetUsersData.GetUserId(user.UserName);
                    await _userWalletData.InsertUserWallet(userWallet);

                    // Insert UserCurrencySettings
                    var userCurrencySettings = new UserCurrencySettingsModel
                    {
                        UserId = userWallet.UserId,
                        USD = userWallet.USD > 0 ? 1 : 0,
                        EUR = userWallet.EUR > 0 ? 1 : 0,
                        CHF = userWallet.CHF > 0 ? 1 : 0,
                        RUB = userWallet.RUB > 0 ? 1 : 0,
                        CZK = userWallet.CZK > 0 ? 1 : 0,
                        GBP = userWallet.GBP > 0 ? 1 : 0
                    };
                    await _userCurrencySettingsData.InsertUserCurrencySettings(userCurrencySettings);

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
