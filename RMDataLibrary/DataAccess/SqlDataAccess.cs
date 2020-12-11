using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataLibrary.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;
        public string ConnectionStringName { get; set; } = "RestaurantManagerMVC";
        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<T>> LoadData<T, U>(string storedProcedure, U parameters)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var results = await connection.QueryAsync<T>(storedProcedure, 
                                                             parameters, 
                                                             commandType: CommandType.StoredProcedure);
                 
                return results.ToList();
            }
        }


        public async Task SaveData<T>(string storedProcedure, T parameters)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }


        public async Task DeleteData<T>(string storedProcedure, T parameters)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
