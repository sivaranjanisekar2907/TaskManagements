using Euro.TaskManagement.Api.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Euro.TaskManagement.Api.Abstraction
{
    public interface IStaffRepository
    {
        Task<Response> UpsertStaffWithTimetrackerAsync(Staff createStaffWithTTCommand);
        Task<IEnumerable<Staff>> GetStaffDetailAsync(int staffId);

        Task<Response> DeleteStaffAsync(DeleteStaffCommand commands);
    }
}
