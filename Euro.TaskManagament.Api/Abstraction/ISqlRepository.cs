using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Euro.TaskManagement.Api.Abstraction
{
    public interface ISqlRepository : IDisposable
    {
        /// <summary>
        /// Method to execute the SELECT query to fetch the rows of generic type 'T'.
        /// </summary>
        /// <param name="sqlQuery">SQL query to execute</param>
        /// <param name="parameters">Parameters to be associated with SQL query</param>
        /// <returns>Returns a list of type 'T'</returns>
        Task<IEnumerable<T>> QueryAsync<T>(string sqlQuery, dynamic parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        Task<int> ExecuteAsync<T>(string sqlQuery, dynamic parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Method to fetch a single object of simple type (ideally used for INSERT query to return inserted id).
        /// </summary>
        /// <param name="sqlQuery">SQL query to execute</param>
        /// <param name="parameters">Parameters to be associated with SQL query</param>
        /// <returns>"T" entity result from the query execution</returns>
        Task<T> ExecuteScalarAsync<T>(string sqlQuery, dynamic parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Method to execute the insert query to send the rows of generic type 'T' in bulk to stored procedure.
        /// </summary>
        /// <param name="sqlQuery">SQL query to execute</param>
        /// <param name="parameters">Parameters to be associated with SQL query</param>
        /// <returns>Returns a list of type 'T'</returns>
        Task<long> ExecuteBulkAsync<T>(string sqlQuery, dynamic parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        Task<IDbConnection> GetConnectionAsync();
    }
}
