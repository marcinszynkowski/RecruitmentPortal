using RP.AccountModule.Interfaces;
using RP.Entities.AccountModule.ViewModels;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Security;

namespace RP.Controllers
{
    public class AccountController : Controller
    {
        #region Fields
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        #endregion

        #region Constructor
        public AccountController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }
        #endregion

        #region SignUp
        public ActionResult SignUp()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult SignUp(UserSignUpViewModel userSignUpViewModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                ////using (_db)
                //{
                //    using (DbContextTransaction transaction = _db.Database.BeginTransaction())
                //    {
                if (!_userService.IsEmailExist(userSignUpViewModel.Email))
                {
                    try
                    {
                        _userService.CreateUserAccount(userSignUpViewModel.Email, userSignUpViewModel.Password,
                             userSignUpViewModel.FirstName, userSignUpViewModel.LastName);
                        SendConfirmationEmail(userSignUpViewModel.Email);
                        //transaction.Commit();
                        return View("AfterSignUp");
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić rejestrację.");
                        //transaction.Rollback();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Z tego emaila już się zarejestrowano.");
                }
                //    }
                //}
            }
            return View();
        }

        public ActionResult TermOfUse()
        {
            return View();
        }

        private void SendConfirmationEmail(string email)
        {
            SmtpClient smtpClient = new SmtpClient();

            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(new MailAddress(email));
            mailMessage.Subject = "Aktywacja konta";
            mailMessage.Body = string.Format("Witaj {0}<BR/>Dziękujemy Ci za rejestrację, proszę kliknij w podany link by zakończyć rejestrację: <a href=\"{1}\" title=\"Aktywacja konta\">{1}</a>", email, Url.Action("ConfirmEmail", "Account", new { email }, Request.Url.Scheme));
            mailMessage.IsBodyHtml = true;

            smtpClient.Send(mailMessage);
        }

        public ActionResult ConfirmEmail(string email)
        {
            ////using (_db)
            //{
            //    using (DbContextTransaction transaction = _db.Database.BeginTransaction())
            //    {
            try
            {
                int roleId = _roleService.GetRoleId("Użytkownik");

                _userService.SetUserRole(email, roleId);
                FormsAuthentication.SetAuthCookie(email, false);
                //transaction.Commit();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                //transaction.Rollback();
                return View("Index", "Home");
            }
            //    }
            //}
        }
        #endregion

        #region SignIn
        public ActionResult SignIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(UserSignInViewModel userSignInViewModel)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(userSignInViewModel.Email, userSignInViewModel.Password))
                {
                    if (_userService.CanSignIn(userSignInViewModel.Email))
                    {
                        _userService.SignIn(userSignInViewModel.Email);
                        FormsAuthentication.SetAuthCookie(userSignInViewModel.Email, userSignInViewModel.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                    else if (_userService.GetLockoutEnabled(userSignInViewModel.Email))
                    {
                        return RedirectToAction("AccountBlocked", "Account");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Nieaktywowałeś jeszcze swojego konta.");
                    }
                }
                else
                {
                    if (_userService.IsBadPassword(userSignInViewModel.Email))
                    {
                        _userService.ProvidedBadPassword(userSignInViewModel.Email);
                    }
                    ModelState.AddModelError("", "Login lub hasło są nieprawidłowe.");
                }
            }
            return View(userSignInViewModel);
        }
        #endregion

        #region Account blocked
        public ActionResult AccountBlocked()
        {
            return View();
        }
        #endregion

        #region Reset password
        public ActionResult ResetPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                if (_userService.CanResetPassword(resetPasswordViewModel.Email))
                {
                    try
                    {
                        SendResetPasswordEmail(resetPasswordViewModel.Email);
                        ModelState.AddModelError("", "Na Twój adres email został wysłany link potwierdzający reset hasła");
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Wystąpiły błędy przy wysyłaniu wadomości, proszę ponowić resetowanie hasła.");
                    }
                }
                else if (!_userService.CanResetPassword(resetPasswordViewModel.Email))
                {
                    ModelState.AddModelError("", "Najpierw aktywuj swoje konto.");
                }
                else
                {
                    ModelState.AddModelError("", "Jeszcze się nie zarejestrowałeś.");
                }
            }
            return View(resetPasswordViewModel);
        }

        private void SendResetPasswordEmail(string email)
        {
            SmtpClient smtpClient = new SmtpClient();

            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(new MailAddress(email));
            mailMessage.Subject = "Reset hasła";
            mailMessage.Body = string.Format("Witaj {0}<BR/>Jeśli chcesz zresetować swoje hasło, proszę kliknij w podany link: <a href=\"{1}\" title=\"Reset hasła\">{1}</a>", email, Url.Action("ConfirmResetPasswordEmail", "Account", new { email }, Request.Url.Scheme));
            mailMessage.IsBodyHtml = true;

            smtpClient.Send(mailMessage);
        }

        public ActionResult ConfirmResetPasswordEmail(string email)
        {
            string password = _userService.ResetPassword(email);
            if (password != null)
            {
                SendNewPasswordEmail(email, password);
                return View("SendNewPassword");
            }

            return RedirectToAction("Index", "Home");
        }

        private void SendNewPasswordEmail(string email, string password)
        {
            SmtpClient smtpClient = new SmtpClient();

            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(new MailAddress(email));
            mailMessage.Subject = "Nowe hasło";
            mailMessage.Body = string.Format("Witaj {0}<BR/>Twoje nowe hasło: {1}, po zalogowaniu prosimy je jak najszybciej zmienić.", email, password);
            mailMessage.IsBodyHtml = true;

            smtpClient.Send(mailMessage);
        }
        #endregion

        #region Send confirmation email again
        public ActionResult SendConfirmationEmailAgain()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult SendConfirmationEmailAgain(SendConfirmationEmailAgainViewModel sendConfirmationEmailAgainViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Membership.ValidateUser(sendConfirmationEmailAgainViewModel.Email, sendConfirmationEmailAgainViewModel.Password))
                    {
                        if (_userService.IsAccountNotActivated(sendConfirmationEmailAgainViewModel.Email))
                        {
                            SendConfirmationEmail(sendConfirmationEmailAgainViewModel.Email);
                            return View("AfterSignUp");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Twoje konto zostało już aktywowane.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Login lub hasło są nieprawidłowe.");
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Błędy przy wysyłaniu wiadomości");
                }
            }
            return View(sendConfirmationEmailAgainViewModel);
        }
        #endregion

        #region SignOut
        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}