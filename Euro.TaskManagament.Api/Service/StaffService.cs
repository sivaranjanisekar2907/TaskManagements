using Euro.TaskManagement.Api.Abstraction;
using Euro.TaskManagement.Api.Logging;
using Euro.TaskManagement.Api.Model;
using Euro.TaskManagement.Api.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Euro.TaskManagement.Api.Service
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;
        public StaffService()
        {
            _staffRepository = new StaffRepository();
        }

        // Update and Create the Product
        // If the ProductID is 0 then it ll create
        public async Task<Response> UpsertStaffWithTimetrackerAsync(Staff createStaffWithTTCommand)
        {
            Response response;
            // For RollBack if any transaction goes wrong
            using (var transaction = TransactionFactory.CreateTransactionScope())
            {
              response = await _staffRepository.UpsertStaffWithTimetrackerAsync(createStaffWithTTCommand);
                return response;
            }

        }
        // Fetch all the products
        public async Task<Staff> GetStaffDetailAsync(int staffId)
        {
            try
            {
                var lststaff = await _staffRepository.GetStaffDetailAsync(staffId);
                var staff =lststaff.FirstOrDefault();
                var staffDetail = new Staff()
                {
                    StaffName = staff.StaffName,
                    TimeTracker = staff.TimeTracker.Select(a => new Model.TimeTracker()
                    {
                        Week = new Days()
                        {
                            Monday = a.Week.Monday,
                            Tuesday = a.Week.Tuesday,
                            Wednesday = a.Week.Wednesday,
                            Thrusday=  a.Week.Thrusday,
                            Friday= a.Week.Friday
                        },
                        Hours = staff.TimeTracker.FirstOrDefault().Hours


                    }).ToList()
                };

                return staffDetail;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }
        // Delete the products
        public async Task<Response> DeleteStaffAsync(DeleteStaffCommand commands)
        {
            try
            {
                var response = await _staffRepository.DeleteStaffAsync(commands);
                return response;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }

    }
}

