using Newtonsoft.Json;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileKitten.Core.Model
{
    public class Milestone
    {
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "dueOn")]
        public DateTimeOffset? DueOn { get; set; }
        [JsonProperty(PropertyName = "number")]
        public int Number { get; set; }
        [JsonProperty(PropertyName = "state")]
        public ItemState State { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "url")]
        public Uri Url { get; set; }

        public static Milestone Make(Octokit.Milestone milestone)
        {
            return new Milestone
            {
                Description = milestone.Description,
                DueOn = milestone.DueOn,
                Number = milestone.Number,
                State = milestone.State,
                Title = milestone.Title,
                Url = milestone.Url
            };
        }
    }
}
