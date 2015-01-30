using AgileKitten.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace AgileKitten.Web.Controllers.Api
{
    public class GHApiController : ApiBaseController
    {
        public async Task<IEnumerable<Repository>> GetRepositories()
        {
            return await Service.GetRepositories();
        }

        public async Task<Board> GetBoard(int repoId)
        {
            return await Service.GetBoard(repoId);
        }
    }
}
