using Euro.TaskManagement.Api.Model;
using System.Threading.Tasks;

namespace Euro.TaskManagement.Api.Abstraction
{
    public interface IStaffService
    {
        Task<Response> UpsertStaffWithTimetrackerAsync(Staff createStaffWithTTCommand);
        Task<Staff> GetStaffDetailAsync(int staffId);
        Task<Response> DeleteStaffAsync(DeleteStaffCommand deleteStaffCommand);
    }
}
