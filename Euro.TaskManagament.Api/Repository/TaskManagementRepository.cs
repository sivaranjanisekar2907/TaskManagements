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
    public class TaskManagementRepository : ITaskManagementRepository
    {
        protected readonly ISqlRepository sqlRepository;
        protected readonly IConnectionFactory connectionFactory;

        public TaskManagementRepository()
        {
            sqlRepository = new SqlBaseRepository(connectionFactory);

        }

        public async Task<List<string>> GetTaskDetailAsync()
        {
            try
            {
                return (await sqlRepository.QueryAsync<string>(SqlQueries.GetTaskDetail).ConfigureAwait(false)).ToList();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }

        public async Task<Response> UpsertTaskForStaffAsync(int staffId, List<TaskAllocation> taskAllocations)
        {
            Response response = new Response();

            try
            {
                DataTable dtTaskAlloCation = new DataTable();
                dtTaskAlloCation.Columns.Add("StaffId", typeof(int));
                dtTaskAlloCation.Columns.Add("TaskId", typeof(int));
                dtTaskAlloCation.Columns.Add("WeekId", typeof(int));             

                foreach (var cmd in taskAllocations)
                {
                    dtTaskAlloCation.Rows.Add(cmd.task, cmd.WeekId);
                }
               var tskInsert = await sqlRepository.ExecuteAsync<int>(SqlQueries.CreateTaskForStaff,
                       new
                       {
                          staffId,
                           taskAllocations = dtTaskAlloCation.AsTableValuedParameter(SqlQueries.TimeTrackerTableType),
                       });

                response.IsSuccess = (tskInsert) >= 0;
                return response;

            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }

    }
}
