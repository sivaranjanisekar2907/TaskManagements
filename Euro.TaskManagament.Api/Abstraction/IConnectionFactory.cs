using System;
using System.Data;
using System.Threading.Tasks;

namespace Euro.TaskManagement.Api.Abstraction
{
    public interface IConnectionFactory : IDisposable
    {
        // Fetch the Connection String
        Task<IDbConnection> CreateConnectionAsync();
    }
}
