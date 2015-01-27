using AgileKitten.Core.Service;
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
        private Service service;
        public Service Service
        {
            get
            {
                return service ?? (service = new Service());
            }
        }

        public ApiBaseController()
        {
            Service.Client = new GitHubClient(new ProductHeaderValue("srosengren-agilekitten"), new Uri("https://github.com/"));
            if (User != null && User.Identity != null && !string.IsNullOrWhiteSpace(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Authentication).Value))
                    Service.Client.Credentials = new Credentials(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Authentication).Value);
        }
    }
}