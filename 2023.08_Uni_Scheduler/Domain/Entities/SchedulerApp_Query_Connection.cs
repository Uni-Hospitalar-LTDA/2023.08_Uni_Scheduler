using _2023._08_Uni_Scheduler.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2023._08_Uni_Scheduler.Domain.Entities
{
    public class SchedulerApp_Query_Connection : Querys<SchedulerApp_Query_Connection>
    {
        public string idQuery { get; set; }
        public string idConnection { get; set; }

        public static async Task<List<SchedulerApp_Query_Connection>> getAllToListByIdAsync(string queryId)
        {
            string query = $@"SELECT * FROM [UHCDB].dbo.[SchedulerApp_Query_Connection] WHERE idQuery = {queryId}";
            return await getAllToList(query);
        }

        /** Delete **/
        public async static Task deleteAsync(string idQuery)
        {
            using (SqlConnection conn = new Connection().getConnectionApp())
            {
                try
                {
                    await conn.OpenAsync();
                    SqlCommand command = conn.CreateCommand();
                    command.CommandText = $"DELETE FROM [UHCDB].dbo.[SchedulerApp_Query_Connection] WHERE idQuery = {idQuery}";
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
