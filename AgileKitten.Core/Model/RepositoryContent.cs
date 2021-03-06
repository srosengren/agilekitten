﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileKitten.Core.Model
{
    public class RepositoryContent
    {
        [JsonProperty(PropertyName = "githubRepositoryId")]
        public int GithubRepositoryId { get; set; }

        [JsonProperty(PropertyName = "repositoryName")]
        public string RepositoryName { get; set; }

        [JsonProperty(PropertyName = "ownerLogin")]
        public string OwnerLogin { get; set; }

        [JsonProperty(PropertyName = "labels")]
        public IEnumerable<Label> Labels { get; set; }

        [JsonProperty(PropertyName = "milestones")]
        public IEnumerable<Milestone> Milestones { get; set; }

        [JsonProperty(PropertyName = "issues")]
        public IEnumerable<Issue> Issues { get; set; }
    }
}
