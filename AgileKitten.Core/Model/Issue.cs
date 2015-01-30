using Newtonsoft.Json;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileKitten.Core.Model
{
    public class Issue
    {
        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }
        [JsonProperty(PropertyName = "labels")]
        public IEnumerable<string> Labels { get; set; }
        [JsonProperty(PropertyName = "milestoneNumber")]
        public int? MilestoneNumber { get; set; }
        [JsonProperty(PropertyName = "number")]
        public int Number { get; set; }
        [JsonProperty(PropertyName = "state")]
        public ItemState State { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "url")]
        public Uri Url { get; set; }

        public static Issue Make(Octokit.Issue issue)
        {
            return new Issue
            {
                Body = issue.Body,
                Labels = issue.Labels.Select(l => l.Name),
                MilestoneNumber = issue.Milestone != null ? issue.Milestone.Number : (int?)null,
                Number = issue.Number,
                State = issue.State,
                Title = issue.Title,
                Url = issue.Url
            };
        }
    }
}
