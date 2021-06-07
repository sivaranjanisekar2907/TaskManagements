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
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;
        public StaffController()
        {
            _staffService = new StaffService();          
        }

        [HttpPost("create-update-StaffWithTimeTracker")]
        public async Task<IActionResult> UpsertStaffWithTimetracker(Staff createStaffWithTTCommand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(await _staffService.UpsertStaffWithTimetrackerAsync(createStaffWithTTCommand));
        }

        [HttpGet]
        [Route("StaffWithTimeTracker")]
        public async Task<IActionResult> GetStaffDetail(int StaffID)
        {
            return Ok(await _staffService.GetStaffDetailAsync(StaffID));
        }

        [HttpDelete("delete-staff")]
        public async Task<IActionResult> DeleteStaff(DeleteStaffCommand deleteProductCommand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(await _staffService.DeleteStaffAsync(deleteProductCommand));
        }
    }
}
