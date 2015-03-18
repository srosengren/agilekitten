using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using Microsoft.SqlServer.Server;

namespace AgileKitten.Core.Repositories.DynamicParams
{
    public class RepositoryParam : Dapper.SqlMapper.IDynamicParameters
    {
        private Model.DTO.Repository _repository;

        public RepositoryParam(Model.DTO.Repository repository)
        {
            _repository = repository;
        }

        public void AddParameters(System.Data.IDbCommand command, SqlMapper.Identity identity)
        {
            if (_repository == null)
                return;

            var sqlCommand = (SqlCommand)command;
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlMetaData[] Label_Definition = { new SqlMetaData("Name", SqlDbType.NVarChar, SqlMetaData.Max), new SqlMetaData("RepositoryId", SqlDbType.Int), new SqlMetaData("Sort", SqlDbType.Int) };
            SqlMetaData[] Issue_Definition = { new SqlMetaData("Number", SqlDbType.Int), new SqlMetaData("RepositoryId", SqlDbType.Int), new SqlMetaData("Sort", SqlDbType.Int) };

            sqlCommand.Parameters.AddWithValue("@OriginId", _repository.OriginId);

            if (_repository.Labels != null && _repository.Labels.Count() > 0)
            {
                sqlCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@Labels",
                    Direction = ParameterDirection.Input,
                    TypeName = "LabelType",
                    SqlDbType = SqlDbType.Structured,
                    Value = _repository.Labels.Select(l =>
                    {
                        var r = new SqlDataRecord(Label_Definition);
                        r.SetString(0, l.Name);
                        r.SetInt32(1, 0);
                        r.SetInt32(2, 0);
                        return r;
                    }).ToList()
                });
            }

            if (_repository.Issues != null && _repository.Issues.Count() > 0)
            {
                sqlCommand.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@Issues",
                    Direction = ParameterDirection.Input,
                    TypeName = "IssueType",
                    SqlDbType = SqlDbType.Structured,
                    Value = _repository.Issues.Select(i =>
                    {
                        var r = new SqlDataRecord(Issue_Definition);
                        r.SetInt32(0, i.Number);
                        r.SetInt32(1, 0);
                        r.SetInt32(2, 0);
                        return r;
                    }).ToList()
                });
            }
        }
    }
}
