using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileKitten.Core.Model.DTO
{
    public class Repository
    {
        public int RepositoryId { get; set; }
        public int OriginId { get; set; }

        public IEnumerable<Issue> Issues { get; set; }
        public IEnumerable<Label> Labels { get; set; }
    }
}
