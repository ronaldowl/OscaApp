using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OscaApp.Data;
using OscaApp.Models;
using OscaApp.Models.AccountViewModels;
using OscaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private ContextPage contexto;
        private ApplicationDbContext db;
        private readonly UsuarioData usuarioData;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;


        public UsuarioController(ApplicationDbContext db, IHttpContextAccessor httpContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILoggerFactory loggerFactory)
        {
            this.usuarioData = new UsuarioData(db);
            this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        [HttpPost]
        public IActionResult FormUpdateUsuario(UsuarioViewModel entrada)
        {
            ApplicationUser user = new ApplicationUser();


            try
            {
                //if (ClienteRules.MontaClienteUpdate(entrada, out cli))
                //{
                //    clienteDataService.Update(cli);
                //    return RedirectToAction("FormUpdateUsuario", new { id = cli.id.ToString(), idOrg = contexto.idOrganizacao });
                //}

            }
            catch (Exception ex)
            {
                //TODO: Gravar exceção no LOG
            }

            return RedirectToAction("FormUpdateUsuario", new { id = user.Id.ToString() });
        }


        public ViewResult GridUsuario()
        {
            return View(usuarioData.GetAll(contexto.idOrganizacao));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult FormCreateUsuario(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FormCreateUsuario(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
              
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, idOrganizacao = contexto.idOrganizacao };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {                 
                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(UsuarioController.GridUsuario), "Usuario");
            }
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
