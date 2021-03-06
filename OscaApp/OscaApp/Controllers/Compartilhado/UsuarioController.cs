﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using OscaApp.Data;
using OscaApp.framework;
using OscaApp.Models;
using OscaApp.Models.AccountViewModels;
using OscaApp.ViewModels;
using OscaFramework.MicroServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace OscaApp.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private ContextPage contexto;
  
        private SqlGenericData sqlData;

        private readonly UsuarioData usuarioData;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;


        public UsuarioController(ApplicationDbContext db, IHttpContextAccessor httpContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILoggerFactory loggerFactory)
        {
            this.usuarioData = new UsuarioData(db);
            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
          
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
            sqlData = new SqlGenericData();
            this.contexto = new ContextPage().ExtractContext(httpContext);
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

        [HttpGet]
        public ViewResult FormUpdateUsuario(string id)
        {
            ApplicationUser user = new ApplicationUser();

            UsuarioViewModel modelo = new UsuarioViewModel();
            modelo.contexto = this.contexto;

            try
            {
                user = usuarioData.Get(id);
                modelo.usuario = user;

                //Prenche lista de preço para o contexto da página
                List<SelectListItem> itens = new List<SelectListItem>();
                itens = HelperAttributes.PreencheDropDownList(sqlData.RetornaTodosPerfis(this.contexto.idOrganizacao));
                modelo.perfis = itens;
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 10, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateUsuario-post", ex.Message);
            }

            return View(modelo);
        }
        [HttpPost]
        public ViewResult FormUpdateUsuario(UsuarioViewModel entrada)
        {
            ApplicationUser user = new ApplicationUser();

            UsuarioViewModel modelo = new UsuarioViewModel();
            modelo.contexto = this.contexto;

            try
            {
               
         
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 10, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateUsuario-post", ex.Message);
            }

            return View(modelo);
        }


        public ViewResult GridUsuario(string filtro, int Page)
        {
            IEnumerable<ApplicationUser> retorno = usuarioData.GetAll(contexto.idOrganizacao);

            if (!String.IsNullOrEmpty(filtro)) retorno = from A in retorno where (A.UserName.Equals(filtro, StringComparison.InvariantCultureIgnoreCase) || A.Email.Equals(filtro, StringComparison.InvariantCultureIgnoreCase)) select A;

            retorno = retorno.OrderByDescending(A => A.UserName);

            //Se não passar a número da página, caregar a primeira
            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<ApplicationUser>(Page, 10));
        }

        public ViewResult GridLookupUsuario(string filtro, int Page)
        {
            IEnumerable<ApplicationUser> retorno = usuarioData.GetAll(contexto.idOrganizacao);

            if (!String.IsNullOrEmpty(filtro)) retorno = from A in retorno where (A.UserName.Equals(filtro, StringComparison.InvariantCultureIgnoreCase) || A.Email.Equals(filtro, StringComparison.InvariantCultureIgnoreCase)) select A;

            retorno = retorno.OrderByDescending(A => A.UserName);

            //Se não passar a número da página, caregar a primeira
            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<ApplicationUser>(Page, 10));
        }
        public ViewResult LookupUsuario()
        {
            return View();
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
