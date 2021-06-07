using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Euro.TaskManagement.Api.Abstraction;
using Euro.TaskManagement.Api.Model;
using Euro.TaskManagement.Api.Service;

namespace Euro.TaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskAllocationController : ControllerBase
    {
        private readonly ITaskManagementService _taskManagementService;
        public TaskAllocationController()
        {
            _taskManagementService = new TaskAllocationService();
        }

        [HttpPost("create-update-Task")]
        public async Task<IActionResult> UpsertTaskForStaffAsync(int staffId, List<TaskAllocation> taskAllocations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(await _taskManagementService.UpsertTaskForStaffAsync(staffId,taskAllocations));
        }

        [HttpGet]
        [Route("TaskName")]
        public async Task<IActionResult> GetStaffDetail()
        {
            return Ok(await _taskManagementService.GetTaskDetailAsync());
        }

    }
}
