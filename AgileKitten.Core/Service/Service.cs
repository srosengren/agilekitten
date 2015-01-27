using AgileKitten.Core.Model;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileKitten.Core.Service
{
    public class Service
    {
        public GitHubClient Client { get; set; }

        public async Task<IEnumerable<Model.Repository>> GetRepositories()
        {
            var ghRepos = await Client.Repository.GetAllForCurrent();

            return ghRepos.Select(r => AgileKitten.Core.Model.Repository.Make(r));
        }
    }
}
