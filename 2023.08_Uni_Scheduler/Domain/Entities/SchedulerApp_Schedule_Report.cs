using _2023._08_Uni_Scheduler.Configuration;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2023._08_Uni_Scheduler.Domain.Entities
{
    public class SchedulerApp_Schedule_Report : Querys<SchedulerApp_Schedule_Report>
    {
        public string idSchedule { get; set; }
        public string idReport { get; set; }

        /** Delete **/
        public async static Task deleteAsync(string idSchedule)
        {
            using (SqlConnection conn = new Connection().getConnectionApp())
            {
                try
                {
                    await conn.OpenAsync();
                    SqlCommand command = conn.CreateCommand();
                    command.CommandText = $"DELETE FROM [UHCDB].dbo.[SchedulerApp_Schedule_Report] WHERE idSchedule = {idSchedule}";
                    await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }

}
