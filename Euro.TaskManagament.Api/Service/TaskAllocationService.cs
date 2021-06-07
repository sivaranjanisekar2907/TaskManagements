using Euro.TaskManagement.Api.Abstraction;
using Euro.TaskManagement.Api.Logging;
using Euro.TaskManagement.Api.Model;
using Euro.TaskManagement.Api.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Euro.TaskManagement.Api.Service
{
    public class TaskAllocationService : ITaskManagementService
    {
        private readonly ITaskManagementRepository _taskRepository;
        public TaskAllocationService()
        {
            _taskRepository = new TaskManagementRepository();
        }
        //Insert or Update the Task for staff
        public async Task<Response> UpsertTaskForStaffAsync(int staffId, List<TaskAllocation> taskAllocations)
        {
            Response response;
            // For RollBack if any transaction goes wrong
            using (var transaction = TransactionFactory.CreateTransactionScope())
            {
                response = await _taskRepository.UpsertTaskForStaffAsync(staffId, taskAllocations);
                return response;
            }

        }
        // Fetch all the task detail 
        public async Task<List<string>> GetTaskDetailAsync()
        {
            try
            {
                return await _taskRepository.GetTaskDetailAsync();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }
    }
}
