using Euro.TaskManagement.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Euro.TaskManagement.Api.Abstraction
{
    public interface IWeeklyReportRepository
    {
        Task<List<WeeklyReport>> GetWeeklyReportAsync();
    }
}
