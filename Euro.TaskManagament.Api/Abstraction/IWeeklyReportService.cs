using Euro.TaskManagement.Api.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Euro.TaskManagement.Api.Abstraction
{
    public interface IWeeklyReportService
    {
        Task<List<WeeklyReport>> GetWeeklyReportAsync();
    }
}
