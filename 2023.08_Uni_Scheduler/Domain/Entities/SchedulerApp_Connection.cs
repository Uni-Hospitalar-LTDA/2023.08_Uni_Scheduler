using _2023._08_Uni_Scheduler.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace _2023._08_Uni_Scheduler.Domain.Entities
{
    public class SchedulerApp_Connection : Querys<SchedulerApp_Connection>
    {
        public string id { get; set; }
        public string description { get; set; }
        public string observation { get; set; }
        public string server { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string logo { get; set; }


        public async static Task<bool> TestSqlConnection(string server, string login, string password)
        {
            string connectionString = $"Server={server};User Id={login};Password={password};";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    await conn.OpenAsync();
                    MessageBox.Show("Conexão bem sucedida.");
                    return true;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Falha na conexão." + ex.Message);
                    return false;
                }
            }
        }

        public async static Task<DataTable> getAllToDataTableAsync()
        {
            string query = $@"select * from [UHCDB].dbo.[SchedulerApp_Connection]";
            return await getAllToDataTable(query);
        }

        public async static Task<List<SchedulerApp_Connection>> getAllToListAsync()
        {
            string query = $@"select * from [UHCDB].dbo.[SchedulerApp_Connection]";
            return await getAllToList(query);
        }

        public async static Task<string> updateAsync(SchedulerApp_Connection connection)
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

                    command.CommandText = $@"UPDATE [UHCDB].dbo.[SchedulerApp_Connection]
                                              SET 
                                                  [description] = '{connection.description}'
                                                 ,[observation] = '{connection.observation}'                                                 
                                                 ,[server] = '{connection.server}'
                                                 ,[login] = '{connection.login}'
                                                 ,[password] = '{connection.password}'
                                                 ,[logo] = '{connection.logo}' 
                                            WHERE [id] =  {connection.id}";


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
        public async static Task<List<SchedulerApp_Connection>> getAllToListByidAsync(string idQuery)
        {
            string query = $@"select * from [UHCDB].dbo.[SchedulerApp_Connection] Connection
JOIN [UHCDB].dbo.[SchedulerApp_Query_Connection] QC ON QC.idConnection = Connection.id
where idQuery = {idQuery}";
            return await getAllToList(query);
        }
        public async static Task<SchedulerApp_Connection> getToClassByDescriptionAsync(string description)
        {
            string query = $@"select * from [UHCDB].dbo.[SchedulerApp_Connection] Connection
                              where description like '{description}'";
            return await getToClass(query);
        }

    }

}
