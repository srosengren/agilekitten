using Octokit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AgileKitten.Web.Controllers
{
    public class HomeController : BaseController
    {

        // GET: Home
        public async Task<ActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
                return View("Authenticated");

            return View();
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

                var token = await Client.Oauth.CreateAccessToken(
                    new OauthTokenRequest(ClientId, ClientSecret, code)
                    );

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                var identity = new ClaimsIdentity(new Claim[] { 
                    new Claim(ClaimTypes.Authentication,token.AccessToken)
                }, "ApplicationCookie");

                authManager.SignIn(identity);
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

        private string GetOauthLoginUrl()
        {
            string csrf = Membership.GeneratePassword(24, 1);
            Session["CSRF:State"] = csrf;

            // 1. Redirect users to request GitHub access
            var request = new OauthLoginRequest(ClientId)
            {
                Scopes = { "user", "notifications" },
                State = csrf
            };
            var oauthLoginUrl = Client.Oauth.GetGitHubLoginUrl(request);
            return oauthLoginUrl.ToString();
        }
    }
}