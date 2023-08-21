using _2023._08_Uni_Scheduler.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace _2023._08_Uni_Scheduler.Domain.Entities
{
    public class SchedulerApp_Report : Querys<SchedulerApp_Report>
    {
        public string id { get; set; }
        public string description { get; set; }
        public string observation { get; set; }
        public string withsheets { get; set; } = "1";
        public string format { get; set; } = "EJX";


      

        public async static Task<List<SchedulerApp_Report>> getAllToListAsync()
        {
            string query = $@"SELECT Report.* 
FROM [UHCDB].dbo.[SchedulerApp_Report] Report";
            return await getAllToList(query);
        }


        public async static Task<string> updateAsync(SchedulerApp_Report report)
        {
            using (SqlConnection conn = Connection.getInstancia().getConnectionApp())
            {
                SqlTransaction transaction = null;
                try
                {
                    await conn.OpenAsync();
                    SqlCommand command = conn.CreateCommand();
                    transaction = conn.BeginTransaction();
                    command.Connection = conn;
                    command.Transaction = transaction;

                    command.CommandText = $@"UPDATE [UHCDB].dbo.[SchedulerApp_Report]
                                              SET 
                                                  [description] = '{report.description}'
                                                 ,[observation] = '{report.observation}'                                                 
                                                 ,[withsheets] = '{report.withsheets}'                                                 
                                                 ,[format] = '{report.format}'                                                 
                                            WHERE [id] =  {report.id}";


                    Console.WriteLine(command.CommandText);
                    await command.ExecuteNonQueryAsync();

                    return null;
                }
                catch (SqlException)
                {
                    transaction.Rollback();
                    conn.Close();
                    return "error";
                }
                finally
                {
                    transaction.Commit();
                    conn.Close();
                }
            }
        }

        public async static Task<List<SchedulerApp_Report>> getAllToListByCodeAsync(string idSchedule)
        {
            string query = $@"SELECT Report.* 
FROM [UHCDB].dbo.[SchedulerApp_Report] Report
JOIN [UHCDB].dbo.[SchedulerApp_Schedule_Report] SR ON SR.idReport = Report.id
WHERE idSchedule = {idSchedule}";
            return await getAllToList(query);
        }
    }

}
