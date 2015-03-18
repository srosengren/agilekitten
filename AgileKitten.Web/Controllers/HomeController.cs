using AgileKitten.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AgileKitten.Web.Controllers
{
    public class HomeController : BaseController
    {

        // GET: Home
        public async Task<ActionResult> Index()
        {

            if (!User.Identity.IsAuthenticated)
                return View();

            
            var vm = new AuthenticatedVM(Url.Content("~/"));
            vm.Repositories = await Service.GetRepositories();

            return View("Authenticated",vm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Type of access requested</param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> Login(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return View();

            return Redirect(GetOauthLoginUrl());
        }

        [AllowAnonymous]
        public async Task<ActionResult> LoginCallback(string code, string state)
        {
            if (!String.IsNullOrEmpty(code))
            {
                var expectedState = Session["CSRF:State"] as string;
                if (state != expectedState)
                    throw new InvalidOperationException("SECURITY FAIL!");
                Session["CSRF:State"] = null;

                await SignIn(code);
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Index");
        }
    }
}