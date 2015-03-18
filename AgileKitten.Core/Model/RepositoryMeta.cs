using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileKitten.Core.Model
{
    public class RepositoryMeta
    {
        [JsonProperty(PropertyName = "githubId")]
        public int GithubId { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "openIssues")]
        public int OpenIssues { get; set; }

        [JsonProperty(PropertyName = "ownerLogin")]
        public string OwnerLogin { get; set; }

        public static RepositoryMeta Make(Octokit.Repository repo)
        {
            return new RepositoryMeta
            {
                GithubId = repo.Id,
                Name = repo.Name,
                OpenIssues = repo.OpenIssuesCount,
                OwnerLogin = repo.Owner.Login
            };
        }
    }
}
