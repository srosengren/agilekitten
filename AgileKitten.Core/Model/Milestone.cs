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
        public string Description { get; set; }
        public DateTimeOffset? DueOn { get; set; }
        public int Nummber { get; set; }
        public ItemState State { get; set; }
        public string Title { get; set; }
        public Uri Url { get; set; }

        public static Milestone Make(Octokit.Milestone milestone)
        {
            return new Milestone
            {
                Description = milestone.Description,
                DueOn = milestone.DueOn,
                Nummber = milestone.Number,
                State = milestone.State,
                Title = milestone.Title,
                Url = milestone.Url
            };
        }
    }
}
