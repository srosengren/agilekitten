using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileKitten.Core.Model
{
    public class Label
    {
        [JsonProperty(PropertyName = "githubRepositoryId")]
        public int GithubRepositoryId { get; set; }

        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "url")]
        public Uri Url { get; set; }

        [JsonProperty(PropertyName = "isIssueList")]
        public bool IsIssueList { get; set; }

        [JsonProperty(PropertyName = "sort")]
        public int Sort { get; set; }

        public static Label Make(Octokit.Label label)
        {
            return new Label
            {
                Color = label.Color,
                Name = label.Name,
                Url = label.Url
            };
        }
    }
}
