﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.Handlers;
using BusinessLogic.Helpers;
using DataAccessLayer.Constructor;
using DataAccessLayer.EF;
using HotelBookingWebsite.Filters;
using HotelBookingWebsite.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace HotelBookingWebsite.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            private set => _signInManager = value;
        }

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            SignInStatus result =
                await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,
                    false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new {ReturnUrl = returnUrl, model.RememberMe});
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }

            return View(new VerifyCodeViewModel {Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe});
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            SignInStatus result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code,
                model.RememberMe, model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        //[AllowAnonymous]
        //public ActionResult Register()
        //{
        //    return View();
        //}

        [AllowAnonymous]
        public ActionResult Register(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model, string returnUrl)
        {
            var response = Request["g-recaptcha-response"];
            var status = RecaptchaHelper.GetRecaptchaResult(response);
            if (!status)
            {
                ViewBag.Message = "Google reCaptcha validation failed";
            }
            else if (ModelState.IsValid)
            {
                Address address = new AddressConstructor(Guid.NewGuid()).AddFirstLine(model.Address1)
                    .AddSecondLine(model.Address2)
                    .AddCity(model.City)
                    .AddState(model.State)
                    .AddZipcode(model.PostalCode)
                    .Build();
                Profile profile = new ProfileConstructor(Guid.NewGuid())
                    .AddPreferredRoomType(model.PreferredRoomType)
                    .AddAddress(address)
                    .AddEmail(model.Email)
                    .AddName(model.FirstName, model.LastName)
                    .AddPhoneNumber(model.PhoneNumber)
                    .Build();
                var user = new AspNetUser
                {
                    Profile = profile,
                    UserName = model.Email,
                    Email = model.Email,
                    LoyaltyYear = DateTimeHandler.GetCurrentTime(),
                    LoyaltyProgress = 0
                };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    user = UserManager.FindByName(model.Email);
                    result = UserManager.AddToRole(user.Id, "customer");
                }
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, false, false);
                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }

            return View(model);
        }

        [CustomAuthorize]
        public ActionResult Update(string returnUrl)
        {
            var userHandler = new AspNetUserHandler();
            AspNetUser user = userHandler.GetAspNetUser(User.Identity.GetUserName());

            var model = new UpdateProfileViewModel
            {
                Address1 = user.Profile.Address.FirstLine,
                Address2 = user.Profile.Address.SecondLine,
                City = user.Profile.Address.City,
                State = user.Profile.Address.State,
                PostalCode = user.Profile.Address.ZipCode,
                FirstName = user.Profile.FirstName,
                LastName = user.Profile.LastName,
                PhoneNumber = user.Profile.PhoneNumber,
                PreferredRoomType = user.Profile.PreferredRoomType
            };

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(UpdateProfileViewModel model, string returnUrl)
        {
            var response = Request["g-recaptcha-response"];
            var status = RecaptchaHelper.GetRecaptchaResult(response);
            if (!status)
            {
                ViewBag.Message = "Google reCaptcha validation failed";
            }
            else if (ModelState.IsValid)
            {
                Address address = new AddressConstructor(Guid.NewGuid()).AddFirstLine(model.Address1)
                    .AddSecondLine(model.Address2)
                    .AddCity(model.City)
                    .AddState(model.State)
                    .AddZipcode(model.PostalCode)
                    .Build();
                var aspNetUserHandler = new AspNetUserHandler();
                Guid profileId = aspNetUserHandler.GetProfile(User.Identity.GetUserName()).Id;

                var profileHandler = new ProfileHandler();
                profileHandler.SetAddress(profileId, address);
                profileHandler.SetName(profileId, model.FirstName, model.LastName);
                profileHandler.SetPhoneNumber(profileId, model.PhoneNumber);
                profileHandler.SetRoomPreference(profileId, model.PreferredRoomType);
                profileHandler.Save();
                AspNetUser user = aspNetUserHandler.GetAspNetUser(User.Identity.GetUserName());
                await SignInManager.SignInAsync(user, false, false);
                return RedirectToLocal(returnUrl);
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult RegisterStaff(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterStaff(RegisterViewModel model, string returnUrl)
        {
            var response = Request["g-recaptcha-response"];
            var status = RecaptchaHelper.GetRecaptchaResult(response);
            if (!status)
            {
                ViewBag.Message = "Google reCaptcha validation failed";
            }
            else if (ModelState.IsValid)
            {
                Address address = new AddressConstructor(Guid.NewGuid()).AddFirstLine(model.Address1)
                    .AddSecondLine(model.Address2)
                    .AddCity(model.City)
                    .AddState(model.State)
                    .AddZipcode(model.PostalCode)
                    .Build();
                Profile profile = new ProfileConstructor(Guid.NewGuid())
                    .AddPreferredRoomType(model.PreferredRoomType)
                    .AddAddress(address)
                    .AddEmail(model.Email)
                    .AddName(model.FirstName, model.LastName)
                    .AddPhoneNumber(model.PhoneNumber)
                    .Build();
                var user = new AspNetUser
                {
                    Profile = profile,
                    UserName = model.Email,
                    Email = model.Email,
                    LoyaltyYear = DateTimeHandler.GetCurrentTime(),
                    LoyaltyProgress = 0
                };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    user = UserManager.FindByName(model.Email);
                    result = UserManager.AddToRole(user.Id, "staff");
                }
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, false, false);
                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }

            return View(model);
        }
        
        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            IdentityResult result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                AspNetUser user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !await UserManager.IsEmailConfirmedAsync(user.Id))
                {
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AspNetUser user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            IdentityResult result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider,
                Url.Action("ExternalLoginCallback", "Account", new {ReturnUrl = returnUrl}));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }

            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem {Text = purpose, Value = purpose})
                .ToList();
            return View(new SendCodeViewModel
            {
                Providers = factorOptions,
                ReturnUrl = returnUrl,
                RememberMe = rememberMe
            });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }

            return RedirectToAction("VerifyCode",
                new {Provider = model.SelectedProvider, model.ReturnUrl, model.RememberMe});
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            ExternalLoginInfo loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            SignInStatus result = await SignInManager.ExternalSignInAsync(loginInfo, false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new {ReturnUrl = returnUrl, RememberMe = false});
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation",
                        new ExternalLoginConfirmationViewModel {Email = loginInfo.Email});
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model,
            string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                ExternalLoginInfo info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }

                var user = new AspNetUser {UserName = model.Email, Email = model.Email};
                IdentityResult result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, false, false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties {RedirectUri = RedirectUri};
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }

                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        #endregion
    }
}