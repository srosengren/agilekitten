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
        public async Task<IEnumerable<RepositoryMeta>> GetRepositories()
        {
            return await Service.GetRepositories();
        }

        public async Task<RepositoryContent> GetRepository(string ownerlogin,string repoName)
        {
            return await Service.GetRepository(ownerlogin,repoName);
        }

        public async Task SortLabel(Core.Model.DTO.Label label)
        {
            //TODO: Check that the user has the correct repository rights to do this.
            await Service.SortLabel(label);
        }
    }
}
