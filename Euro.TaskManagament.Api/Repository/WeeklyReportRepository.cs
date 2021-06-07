using Dapper;
using Euro.TaskManagement.Api.Abstraction;
using Euro.TaskManagement.Api.Logging;
using Euro.TaskManagement.Api.Model;
using Euro.TaskManagement.Api.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Euro.TaskManagement.Api.Repository
{
    public class WeeklyReportRepository : IWeeklyReportRepository
    {
        protected readonly ISqlRepository sqlRepository;
        protected readonly IConnectionFactory connectionFactory;

        public WeeklyReportRepository() => sqlRepository = new SqlBaseRepository(connectionFactory);

        public async Task<List<WeeklyReport>> GetWeeklyReportAsync()
        {
            try
            {
                return (await sqlRepository.QueryAsync<WeeklyReport>(SqlQueries.GetWeekReport).ConfigureAwait(false)).ToList();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }
    }
}
