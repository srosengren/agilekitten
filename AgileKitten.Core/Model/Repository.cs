using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileKitten.Core.Model
{
    public class Repository
    {
        [JsonProperty(PropertyName = "githubId")]
        public int GithubId { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "openIssues")]
        public int OpenIssues { get; set; }

        [JsonProperty(PropertyName = "ownerName")]
        public string OwnerName { get; set; }

        [JsonProperty(PropertyName = "issues")]
        public IEnumerable<Issue> Issues { get; set; }

        public static Repository Make(Octokit.Repository repo)
        {
            return new Repository
            {
                GithubId = repo.Id,
                Name = repo.Name,
                OpenIssues = repo.OpenIssuesCount,
                OwnerName = repo.Owner.Name
            };
        }
    }
}
