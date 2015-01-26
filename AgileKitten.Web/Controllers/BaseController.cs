using Octokit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace AgileKitten.Web.Controllers
{
    public class BaseController : Controller
    {
        private GitHubClient client;
        public GitHubClient Client
        {
            get
            {
                if (client == null)
                    client = new GitHubClient(new ProductHeaderValue("srosengren-agilekitten"), new Uri("https://github.com/"));
                if (User != null && User.Identity != null && !string.IsNullOrWhiteSpace(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Authentication).Value))
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
    }
}