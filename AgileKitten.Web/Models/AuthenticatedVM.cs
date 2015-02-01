using AgileKitten.Core.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgileKitten.Web.Models
{
    public class AuthenticatedVM
    {
        [JsonProperty(PropertyName = "rootUrl")]
        public string RootUrl { get; set; }

        [JsonProperty(PropertyName = "repositories")]
        public IEnumerable<RepositoryMeta> Repositories { get; set; }

        public AuthenticatedVM(string rootUrl)
        {
            RootUrl = rootUrl;
        }
    }
}