using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileKitten.Core.Model.DTO
{
    public class Issue
    {
        public int Number { get; set; }
        public int RepositoryId { get; set; }
        public int? Sort { get; set; }
    }
}
