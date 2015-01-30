using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileKitten.Core.Model
{
    public class Board
    {
        [JsonProperty(PropertyName = "githubRepositoryId")]
        public int GithubRepositoryId { get; set; }

        [JsonProperty(PropertyName = "repositoryName")]
        public string RepositoryName { get; set; }

        [JsonProperty(PropertyName = "ownerName")]
        public string OwnerName { get; set; }

        [JsonProperty(PropertyName = "labels")]
        public IEnumerable<Label> labels { get; set; }

        [JsonProperty(PropertyName = "milestones")]
        public IEnumerable<Milestone> Milestones { get; set; }

        [JsonProperty(PropertyName = "issues")]
        public IEnumerable<Issue> Issues { get; set; }
    }
}
