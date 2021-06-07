using Euro.TaskManagement.Api.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Euro.TaskManagement.Api.Model
{
    public class TaskAllocation
    {
        public string task { get; set; }
        public Week WeekId { get; set; }
    }
}
