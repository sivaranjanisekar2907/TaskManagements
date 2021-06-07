using Dapper;
using Euro.TaskManagement.Api.Abstraction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Euro.TaskManagement.Api.SqlDataAccess
{
    public class SqlBaseRepository: ISqlRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public SqlBaseRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public virtual async Task<IDbConnection> GetConnectionAsync()
        {
            IDbConnection con = await connectionFactory.CreateConnectionAsync();
            return con;
        }
        /// <summary>
        /// Method to execute the SELECT query to fetch the rows of generic type 'T'.
        /// </summary>
        /// <param name="sqlQuery">SQL query to execute</param>
        /// <param name="parameters">Parameters to be associated with SQL query</param>
        /// <returns>Returns a list of type 'T'</returns> 
        public async Task<IEnumerable<T>> QueryAsync<T>(string sqlQuery, dynamic parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IEnumerable<T> result = null;
            using (IDbConnection connection = await connectionFactory.CreateConnectionAsync())
            {
                connection.Open();
                result = await connection.QueryAsync<T>(sqlQuery, (object)parameters, commandType: commandType, commandTimeout: commandTimeout);
            }
            return result;
        }

        /// <summary>
        /// Method to execute a UPDATE/DELETE query on the database
        /// </summary>
        /// <param name="sqlQuery">SQL query to execute</param>
        /// <param name="parameters">Parameters to be associated with SQL query</param>
        /// <returns>Integer value indicating the query result</returns>
        public async Task<int> ExecuteAsync<T>(string sqlQuery, dynamic parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            int result;

            using (IDbConnection connection = await connectionFactory.CreateConnectionAsync())
            {
                connection.Open();
                result = await connection.ExecuteAsync(sqlQuery, (object)parameters, commandTimeout: commandTimeout, commandType: commandType);
            }

            return result;
        }

        /// <summary>
        /// Method to fetch a single value. Ideally used for INSERT query to return inserted id.
        /// </summary>
        /// <param name="sqlQuery">SQL query to execute</param>
        /// <param name="parameters">Parameters to be associated with SQL query</param>
        /// <returns>An object which is the query result</returns>
        public async Task<T> ExecuteScalarAsync<T>(string sqlQuery, dynamic parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            T result;

            using (IDbConnection connection = await connectionFactory.CreateConnectionAsync())
            {
                connection.Open();
                result = await connection.ExecuteScalarAsync<T>(sqlQuery, (object)parameters);
            }

            return result;
        }

        /// <summary>
        /// Method to execute a bulk insert using stored procedure query on the database
        /// </summary>
        /// <param name="sqlQuery">SQL query to execute</param>
        /// <param name="parameters">Parameters to be associated with SQL query</param>
        /// <returns>Integer value indicating the query result</returns>
        public async Task<long> ExecuteBulkAsync<T>(string sqlQuery, dynamic parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            long result;

            using (IDbConnection connection = await connectionFactory.CreateConnectionAsync())
            {
                connection.Open();
                result = await connection.ExecuteAsync(sqlQuery, (object)parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
            }

            return result;
        }

       

        Task<IDbConnection> ISqlRepository.GetConnectionAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
