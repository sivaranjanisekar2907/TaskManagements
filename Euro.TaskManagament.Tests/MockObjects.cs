using Euro.TaskManagement.Api.Model;
using System;
using System.Collections.Generic;

namespace Euro.TaskManagement.Tests
{
    public class MockObjects
    {
        public static Staff GetStaffData()
        {

            var staffData = new Staff
            {
                StaffName = "Siva",
                TimeTracker = new List<TimeTracker>()
                {
                 new TimeTracker()
                 {
                     Week = new Days
                     {
                         Monday = "1",
                         Tuesday = "2",
                         Wednesday = "3",
                         Thrusday ="4",
                         Friday = "5"
                     },
                     Hours = 8

                 }
                }
            };
            return staffData;
        }

        internal static DeleteStaffCommand DeleteStaff(int staffId)
        {
            var delete = new DeleteStaffCommand();
            delete.StaffId = staffId;
            return delete;
        }
    }
}
