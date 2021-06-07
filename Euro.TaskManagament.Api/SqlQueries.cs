using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Euro.TaskManagement.Api
{
    public static class SqlQueries
    {
        #region Staff

        internal const string CreateStaffWithTimeTracker = @"Declare staffID int 

                                                            IF EXISTS
                                                            (
                                                            Select Id from Staff
                                                            )
                                                            Begin 
                                                            UPDATE T                                   
                                                             SET  T.Hours = S.Hours,                                    
                                                               T.ModifiedDate = getutcdate()      
                                                               FROM @timeTracker s
                                                               INNER JOIN [TimeTracker] as T                        
                                                               ON  staffID = T.StaffID    
                                                               AND S.WeekId = T.WeekId  
                                                            End
                                                            Else
                                                            Insert into Staff 
                                                            Values(@staffName)
                                                            Where StaffName != @staffName

                                                            set staffID = Select @@Identity
       
                                                               INSERT INTO [TimeSheet] (StaffId,WeekId,Hours,CreatedDate)      
                                                               SELECT staffID,S.WeekId,S.Hours,GetUtcDate()       
                                                               FROM @timeTracker S    
                                                               LEFT JOIN [TimeTracker] as T                        
                                                               ON  staffID = T.StaffID    
                                                               AND S.WeekId = T.WeekId        
                                                               WHERE T.Id IS NULL      
      
                                                               SELECT 1";
        internal const string TimeTrackerTableType = @"[dbo].[TimeTracker]";

        internal const string GetStaff = @"Select Id,StaffName From Staff";

        internal const string DeleteStaff = @"Delete * from Staff Where Id= @staffID";

        #endregion

        #region TaskManagement
        internal const string CreateTaskForStaff = @"Insert Into TaskManagement
                                               (StaffName,StaffId,TaskName,TaskID,WeekId,Hours)
                                                Values (s.StaffName,s.Id, t.TaskName,t.Id, w.WeekId, t.Hours)
                                                From Task t Join @taskAllocations ta on t.Id = ta.TaskId
                                                Join Staff S ON T.StaffId = ta.StaffId";

        internal const string GetTaskDetail = @"Select TaskName From Task";
        #endregion
        #region WeeklyReport
        internal const string GetWeekReport = @";with cte_WeeklyReport as(                              
                                                select DISTINCT  T.Id as TaskId, Count(TM.TaskID) as Cnt                            
                                                FROM                              
                                                Task T JOIN TaskManagement TM ON                               
                                                T.Id= TM.TaskID)
                                                Select T.TaskName, c.Cnt*Hours 
                                                From Task T 
                                                join cte_WeeklyReport 
                                                on T.Id= c.TaskId  ";
        #endregion
    }
}
