using Euro.TaskManagement.Api.Abstraction;
using Euro.TaskManagement.Api.Logging;
using Euro.TaskManagement.Api.Model;
using Euro.TaskManagement.Api.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Euro.TaskManagement.Api.Service
{
    public class WeeklyReportService : IWeeklyReportService
    {
        private readonly IWeeklyReportRepository _weeklyReportRepository;
        public WeeklyReportService() => _weeklyReportRepository = new WeeklyReportRepository();

        public async Task<List<WeeklyReport>> GetWeeklyReportAsync()
        {
            try
            {
                return await _weeklyReportRepository.GetWeeklyReportAsync();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }
    }
}
