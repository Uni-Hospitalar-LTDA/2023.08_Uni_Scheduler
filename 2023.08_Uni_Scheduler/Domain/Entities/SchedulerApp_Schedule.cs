using _2023._08_Uni_Scheduler.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace _2023._08_Uni_Scheduler.Domain.Entities
{
    public class SchedulerApp_Schedule : Querys<SchedulerApp_Schedule>
    {
        public string id { get; set; }        
        public string description { get; set; }
        public string observation { get; set; }
        public string hour { get; set; }
        public string daysofweek { get; set; }
        public string daysofmonth { get; set; }

        public async static Task<string> updateAsync(SchedulerApp_Schedule schedule)
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

                    command.CommandText = $@"UPDATE [UHCDB].dbo.[SchedulerApp_Schedule]
                                              SET 
                                                  [description] = '{schedule.description}'
                                                 ,[observation] = '{schedule.observation}'                                                 
                                                 ,[hour] = '{schedule.hour}'                                                 
                                                 ,[daysofweek] = '{schedule.daysofweek}'                                                 
                                                 ,[daysofmonth] = '{schedule.daysofmonth}'                                                 
                                            WHERE [id] =  {schedule.id}";


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
        public async static Task<List<SchedulerApp_Schedule>> getAllToListAsync()
        {
            string query = "SELECT * FROM [UHCDB].dbo.[SchedulerApp_Schedule]";
            return await getAllToList(query);
        }
        public async static Task<List<SchedulerApp_Schedule>> getNotifyList()
        {
            string query = $@"DECLARE @DATE DATE
SET @DATE = GETDATE();

SELECT 
	* 
FROM 
    [UHCDB].dbo.[SchedulerApp_Schedule] Schedule
WHERE 	 
    (Schedule.daysofweek LIKE ('%'+(CONVERT(VARCHAR,DATEPART(WEEKDAY, @date))+'%'))
    OR
    (
        (',' + CONVERT(VARCHAR,Schedule.daysofmonth) + ',') LIKE '%,' + CONVERT(VARCHAR,DAY(@date)) + ',%'
        OR
        (EOMONTH(@date) = CONVERT(DATE,@date) AND Schedule.daysofmonth LIKE '%last%')
    )
	)
	AND CAST(CONVERT(VARCHAR(5), Schedule.hour, 108) AS TIME) >= CAST(CONVERT(VARCHAR(5), GETDATE(), 108) AS TIME);

";
            return await getAllToList(query);
        }

    }

}
