

using Euro.TaskManagement.Api.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Euro.TaskManagement.Api.Abstraction
{
    public interface ITaskManagementRepository
    {
        Task<Response> UpsertTaskForStaffAsync(int staffId, List<TaskAllocation> taskAllocations);
        Task<List<string>> GetTaskDetailAsync();
    }
}
