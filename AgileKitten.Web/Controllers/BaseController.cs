using AgileKitten.Core.Service;
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
    public class BaseController : Controller
    {

        private Service service;

        public Service Service
        {
            get { return service ?? (service = new Service() { Client = Client }); }
        }


        private GitHubClient client;
        public GitHubClient Client
        {
            get
            {
                if (client == null)
                    client = new GitHubClient(new ProductHeaderValue("srosengren-agilekitten"), new Uri("https://github.com/"));
                if (User != null && User.Identity != null && User.Identity.IsAuthenticated && !string.IsNullOrWhiteSpace(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Authentication).Value))
                    client.Credentials = new Credentials(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Authentication).Value);
                return client;
            }
        }

        private string clientId;

        protected string ClientId
        {
            get { return clientId ?? (clientId = ConfigurationManager.AppSettings["GHClientId"]); }
        }

        private string clientSecret;

        protected string ClientSecret
        {
            get { return clientSecret ?? (clientSecret = ConfigurationManager.AppSettings["GHClientSecret"]); }
        }

        protected async Task SignIn(string code)
        {
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

        protected string GetOauthLoginUrl()
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