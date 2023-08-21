using _2023._08_Uni_Scheduler.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2023._08_Uni_Scheduler.Domain.Entities
{
    public class SchedulerApp_Contact : Querys<SchedulerApp_Contact>
    {
        public string id { get; set; }
        public string description { get; set; }
        public string observation { get; set; }
        public string mail { get; set; }
        public string canrequest { get; set; }

        public async static Task<string> updateAsync(SchedulerApp_Contact contact)
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

                    command.CommandText = $@"UPDATE [UHCDB].dbo.[SchedulerApp_Contact]
                                              SET 
                                                  [description] = '{contact.description}'
                                                 ,[observation] = '{contact.observation}'                                                                                                  
                                                 ,[mail] = '{contact.mail}'                                                 
                                                 ,[canrequest] = '{contact.canrequest}'                                                 
                                            WHERE [id] =  {contact.id}";


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
        public async static Task<List<SchedulerApp_Contact>> getAllToListAsync()
        {
            string query = $"SELECT * FROM [UHCDB].dbo.[SchedulerApp_Contact]";
            return await getAllToList(query);
        }
        public async static Task<List<SchedulerApp_Contact>> getAllToListByCodeAsync(string idSchedule)
        {
            string query = $@"SELECT Contact.* FROM [UHCDB].dbo.[SchedulerApp_Contact] Contact
                              JOIN [UHCDB].dbo.[SchedulerApp_Schedule_Contact] SC ON  SC.idContact = Contact.id
                              WHERE idSchedule = {idSchedule}";
            return await getAllToList(query);
        }

        public async static Task<bool> canRequest(string mail)
        {
            string query = $"SELECT TOP 1 * FROM [UHCDB].dbo.[SchedulerApp_Contact] WHERE MAIL LIKE '{mail}'";
            var cc = await getToClass(query);
            bool canRequest = false;
            if (cc != null)
            {
                canRequest = Convert.ToBoolean(Convert.ToInt32(cc.canrequest));
            }
            
            return canRequest;
        }
    }

}
