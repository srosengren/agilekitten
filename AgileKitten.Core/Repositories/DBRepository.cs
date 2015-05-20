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
        private SqlConnection getOpenConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["Storage"].ConnectionString);
        }

        public async Task<Model.DTO.Repository> MergeRepository(Model.DTO.Repository repository)
        {
            using (var con = getOpenConnection())
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

        public async Task SortIssue(Model.DTO.Issue issue)
        {
            using(var con = getOpenConnection())
            {
                await con.ExecuteAsync("SortIssue", issue, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task SortLabel(Model.DTO.Label label)
        {
            using(var con = getOpenConnection())
            {
                await con.ExecuteAsync("SortLabel", label, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
