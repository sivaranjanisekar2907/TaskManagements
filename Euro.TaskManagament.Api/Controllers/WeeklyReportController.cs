using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Euro.TaskManagement.Api.Abstraction;
using Euro.TaskManagement.Api.Model;
using Euro.TaskManagement.Api.Service;

namespace Euro.TaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeeklyReportController : ControllerBase
    {
        private readonly IWeeklyReportService _weeklyReportService;
        public WeeklyReportController()
        {
            _weeklyReportService = new WeeklyReportService();
        }        

        [HttpGet]
        [Route("Weekly-Report")]
        public async Task<IActionResult> GetWeeklyReportAsync()
        {
            return Ok(await _weeklyReportService.GetWeeklyReportAsync());
        }
    }
}
