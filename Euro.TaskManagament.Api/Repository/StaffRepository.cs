using Dapper;
using Euro.TaskManagement.Api.Abstraction;
using Euro.TaskManagement.Api.Logging;
using Euro.TaskManagement.Api.Model;
using Euro.TaskManagement.Api.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Euro.TaskManagement.Api.Repository
{
    public class StaffRepository : IStaffRepository
    {
        protected readonly ISqlRepository sqlRepository;
        protected readonly IConnectionFactory connectionFactory;

        public StaffRepository()
        {
            sqlRepository = new SqlBaseRepository(connectionFactory);

        }

        public async Task<Response> UpsertStaffWithTimetrackerAsync(Staff createStaffWithTTCommand)
        {
            Response response = new Response();

            try
            {
                DataTable dtTimeTracker = new DataTable();
                dtTimeTracker.Columns.Add("WeekID", typeof(int));
                dtTimeTracker.Columns.Add("Hours", typeof(int));

                foreach (var cmd in createStaffWithTTCommand.TimeTracker)
                {
                    dtTimeTracker.Rows.Add(cmd.Week, cmd.Hours);
                }


                var tskInsert = await sqlRepository.ExecuteAsync<int>(SqlQueries.CreateStaffWithTimeTracker,
                       new
                       {
                           staffName = createStaffWithTTCommand.StaffName,
                           timeTracker = dtTimeTracker.AsTableValuedParameter(SqlQueries.TimeTrackerTableType),
                       }, commandType: CommandType.StoredProcedure, commandTimeout: 300);

                response.IsSuccess = tskInsert > 0;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }

            return response;
        }

        public async Task<Response> DeleteStaffAsync(DeleteStaffCommand deleteCommand)
        {
            Response response = new Response();

            try
            {
                int result = await sqlRepository.ExecuteAsync<int>(SqlQueries.DeleteStaff,
                     new
                     {
                         deleteCommand.StaffId
                     },
                     commandTimeout: 300);
                response.IsSuccess = result > 0;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }

            return response;
        }

        public async Task<IEnumerable<Staff>> GetStaffDetailAsync(int staffId)
        {
            try
            {
                return await sqlRepository.QueryAsync<Staff>(SqlQueries.GetStaff);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }

    }
}

