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
        [JsonProperty(PropertyName = "repositories")]
        public IEnumerable<Repository> Repositories { get; set; }
    }
}