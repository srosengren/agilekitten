using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace AgileKitten.Web.Controllers.Api
{
    public class ApiBaseController : ApiController
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
    }
}