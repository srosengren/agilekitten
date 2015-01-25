using Octokit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AgileKitten.Web.Controllers
{
    public class HomeController : Controller
    {

        private GitHubClient client;
        public GitHubClient Client
        {
            get
            {
                return client ?? (client = new GitHubClient(new ProductHeaderValue("srosengren-agilekitten"), new Uri("https://github.com/")));
            }
        }

        private string clientId;

        public string ClientId
        {
            get { return clientId ?? (clientId = ConfigurationManager.AppSettings["GHClientId"]); }
        }

        private string clientSecret;

        public string ClientSecret
        {
            get { return clientSecret ?? (clientSecret = ConfigurationManager.AppSettings["GHClientSecret"]); }
        }


        // GET: Home
        public async Task<ActionResult> Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Type of access requested</param>
        /// <returns></returns>
        public async Task<ActionResult> Login(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return View();

            return Redirect(GetOauthLoginUrl());
        }

        public async Task<ActionResult> LoginCallback()
        {
            return RedirectToAction("index");
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