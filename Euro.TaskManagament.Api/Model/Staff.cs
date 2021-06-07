using Euro.TaskManagement.Api.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Euro.TaskManagement.Api.Model
{
    public class Staff
    {
        [Required]
        public string StaffName { get; set; }
        public int? StaffId { get; set; }       
        public List<TimeTracker> TimeTracker { get; set; }
    }

    public class TimeTracker
    {
        public Days Week { get; set; }
        public int Hours { get; set; }
    }
    
}
