using AutoMapper;
using BOMBEROSCR.F5.ENT;
using BOMBEROSCR.F5.UI.Models;
using BOMBEROSCR.F5.UI.WSISeguridad;
using BOMBEROSCR.F5.UTL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BOMBEROSCR.F5.UI.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
    
        public LoginController()
        {
        }

        public LoginController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (TempData["mensaje"] != null)
            {
                ModelState.AddModelError("", "La sesión ha caducado");
                return View("Login");
            }
            return View();
        }

        public ActionResult SalirLogin()
        {
            Session.Remove("UsuarioAutenticado");
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login", null);
        }

        public ActionResult FinalizaSesion()
        {
            LoginViewModel model = new LoginViewModel();
            FormsAuthentication.SignOut();
            Session.Remove("UsuarioAutenticado");
            TempData["mensaje"] = "Sesión ha caducado";

            return RedirectToAction("Login", "Login", null);
        }


        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                Session.Remove("UsuarioAutenticado");

                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                using (var proxy = new SeguridadSoapClient())
                {
                    var passwordEncriptado = UTL.Seguridad.Encriptar(model.Contrasena);


                    var result = proxy.Autenticar(new WSISeguridad.AutenticarRequestBCBC
                    {
                        Usuario = model.Usuario,
                        CLAVE = passwordEncriptado,
                        IP = HttpContext.Request.UserHostAddress
                    });


                    if (result.Resultado)
                    {

                        UsuarioAutenticado UsuarioAutenticado = new UsuarioAutenticado
                        {
                            Usuario = model.Usuario,
                            NombreUsuario = result.NombreUsuario,
                            ContraseniaEncriptada = passwordEncriptado,
                            IP = Dns.GetHostEntry(Dns.GetHostName()).HostName,
                            IdUsuario = result.IdFuncionario,
                            ContextInfo = "AgregarContextInfo",
                            SessionID = Session.SessionID
                        };

                        var respuestaRoles = proxy.ObtenerUsuariosRolSistema(new WSISeguridad.ObtenerUsuariosRolSistemaRequestBCBC
                        {
                            Usuario = ConfigurationManager.AppSettings["UsuarioSistema"],
                            Password = ConfigurationManager.AppSettings["PasswordSistema"],
                            IdModulo = int.Parse(string.IsNullOrEmpty(ConfigurationManager.AppSettings["CodigoModulo"]) ? "0" : ConfigurationManager.AppSettings["CodigoModulo"]),
                            IP = Dns.GetHostEntry(Dns.GetHostName()).HostName,
                        });

                        if (respuestaRoles.Codigo == "00" && respuestaRoles.Items.Any())
                        {
                            var usuarioRol = respuestaRoles.Items.LastOrDefault(x => x.Codigo == UsuarioAutenticado.Usuario);
                            if (usuarioRol != null)
                            {
                                var rolUsuarioSIMAV = usuarioRol.Roles.LastOrDefault();
                                UsuarioAutenticado.Rol = new ParametroBase
                                {
                                    Codigo = rolUsuarioSIMAV.CodRol.ToString(),
                                    Descripcion = rolUsuarioSIMAV.DesRol
                                };
                            }
                        }

                        var resultad = proxy.Opciones(new WSISeguridad.OpcionesRequestBCBC
                        {
                            Usuario = UsuarioAutenticado.Usuario,
                            OPCIONESMENU = true
                        });


                        var listaopciones = new List<OpcionesMenu>();

                        foreach (var item in resultad.Items)
                        {

                            //TODO las opciones de Marco viene con un caracter especial, al tratar de pintarlo en la vista no se puede procesar 'Index/AutorizacionDefinitivaAprobacion?tipo=0'
                            //Para esto lo que se propone es Eliminar todo lo que exista despues del signo de pregunta y dejar solo metodo y controller y se maneja el menú igual que se hace
                            //con Plan de visita, en el mapper se usa un metodo para verificar que sea una opcion especial y si es así se maneja diferente      
                            item.DesUrlFormulario = item.DesUrlFormulario.Split('?').Length > 0 ? item.DesUrlFormulario.Split('?')[0] : item.DesUrlFormulario;

                            listaopciones.Add(Mapper.Map<WSISeguridad.InfoOpcion, OpcionesMenu>(item));
                        }

                        UsuarioAutenticado.OpcionesMenu = listaopciones;

                        Session["UsuarioAutenticadoTemporal"] = UsuarioAutenticado;

                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            ViewBag.ReturnUrl = returnUrl;
                        }
                        WSISeguridad.DobleFactorResponseBCBC resultCodigoSeguridad = this.SolicitarCodigoSeguridad();
                        if (resultCodigoSeguridad.Codigo == "00")
                        {
                            ViewBag.Mensaje = resultCodigoSeguridad.Descripcion;
                            return View("CodigoSeguridad", new CodigoSeguridadViewModel { Usuario = model.Usuario });
                        }
                        else
                        {
                            ModelState.AddModelError("", resultCodigoSeguridad.Descripcion);
                            return View(model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", result.Descripcion);
                        return View(model);
                    }
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Ocurrió un error autenticando el usuario.");
                return View(model);
            }


        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult EnviarCodigoSeguridad(CodigoSeguridadViewModel model, string returnUrl)
        {
            try
            {
                using (var proxy = new SeguridadSoapClient())
                {
                    UsuarioAutenticado usuarioAutenticado = (UsuarioAutenticado)Session["UsuarioAutenticadoTemporal"];
                    model.Usuario = usuarioAutenticado.Usuario;
                    WSISeguridad.DobleFactorResponseBCBC result = proxy.VerificarOTP(new WSISeguridad.DobleFactorRequestBCBC
                    {
                        Cod_Usuario = model.Usuario,
                        OTP_Encriptado = UTL.Seguridad.Encriptar(model.CodigoSeguridad)
                    });
                    if (result.Codigo != "00")
                    {
                        ModelState.AddModelError("", result.Descripcion);
                        return View("CodigoSeguridad", model);
                    }
                    FormsAuthentication.SetAuthCookie(usuarioAutenticado.NombreUsuario, false);
                    Session["UsuarioAutenticado"] = Session["UsuarioAutenticadoTemporal"];
                    Session.Remove("UsuarioAutenticadoTemporal");
                    return RedirectToLocal(returnUrl);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Ocurrió un error validando el código de seguridad.");
                return View("CodigoSeguridad", model);
            }
        }

        private WSISeguridad.DobleFactorResponseBCBC SolicitarCodigoSeguridad()
        {
            using (var proxy = new SeguridadSoapClient())
            {
                UsuarioAutenticado usuarioAutenticado = (UsuarioAutenticado)Session["UsuarioAutenticadoTemporal"];
                WSISeguridad.DobleFactorResponseBCBC result = proxy.GenerarOTP(new WSISeguridad.DobleFactorRequestBCBC
                {
                    Cod_Usuario = usuarioAutenticado.Usuario,
                    Ind_Reenvio = true
                });
                if (result.Codigo == "00")
                {
                    result.Descripcion = "Se ha enviado el código de seguridad.";
                }
                return result;
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [AllowAnonymous]
        public JsonResult SolicitarNuevoCodigoSeguridad()
        {
            int codigo = Constantes.Codigos.Exito;
            string descripcion = string.Empty;
            try
            {
                WSISeguridad.DobleFactorResponseBCBC result = this.SolicitarCodigoSeguridad();
                if (result.Codigo != "00")
                {
                    codigo = Constantes.Codigos.ErrorPersonalizado;
                }
                descripcion = result.Descripcion;
            }
            catch (Exception e)
            {
                codigo = Constantes.Codigos.ErrorNoControlado;
                descripcion = "Ocurrió al solicitar el código de seguridad.";
            }
            return Json(new
            {
                codigo = codigo,
                descripcion = descripcion
            });
        }

        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
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
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
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
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
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
            var result = await UserManager.ConfirmEmailAsync(userId, code);
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
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
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
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
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
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Login", new { ReturnUrl = returnUrl }));
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
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
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
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
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

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

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
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
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