using _2023._08_Uni_Scheduler.Configuration;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2023._08_Uni_Scheduler.Domain.Entities
{
    public class SchedulerApp_Report_Query : Querys<SchedulerApp_Report_Query>
    {
        public string idReport { get; set; }
        public string idQuery { get; set; }


        /** Delete **/
        public async static Task deleteAsync(string idReport)
        {
            using (SqlConnection conn = new Connection().getConnectionApp())
            {
                try
                {
                    await conn.OpenAsync();
                    SqlCommand command = conn.CreateCommand();
                    command.CommandText = $"DELETE FROM [UHCDB].dbo.[SchedulerApp_Report_Query] WHERE idReport = {idReport}";
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
