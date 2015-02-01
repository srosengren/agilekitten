using AgileKitten.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileKitten.Core.Repositories
{
    public class GHRepository
    {
        public async Task<RepositoryContent> GetBoard(int repoId)
        {
            return new RepositoryContent
            {
                OwnerName = "srosengren",
                RepositoryName = "agilekitten"
            };
        }
    }
}
