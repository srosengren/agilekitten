using AgileKitten.Core.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using AgileKitten.Core.Repositories.DynamicParams;
using System.Data;

namespace AgileKitten.Core.Repositories
{
    public class DBRepository
    {
        public async Task<Model.DTO.Repository> MergeRepository(Model.DTO.Repository repository)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Storage"].ConnectionString))
            {
                var result = await con.QueryMultipleAsync("MergeRepository", new RepositoryParam(repository), commandType: CommandType.StoredProcedure);
                var repo = result.Read<Model.DTO.Repository>().FirstOrDefault();
                if (repo != null)
                {
                    repo.Labels = result.Read<Model.DTO.Label>();
                    repo.Issues = result.Read<Model.DTO.Issue>();
                }
                return repo;
            }
        }
    }
}
