using _2023._08_Uni_Scheduler.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace _2023._08_Uni_Scheduler.Domain.Entities
{
    public class SchedulerApp_Query : Querys<SchedulerApp_Query>
    {
        public string id { get; set; }
        public string description { get; set; }
        public string observation { get; set; }
        public string SQLcode { get; set; }


        /** gets **/
        public static async Task<List<SchedulerApp_Query>> getAllToListAsync()
        {
            string query = $@"select * from [UHCDB].dbo.[SchedulerApp_Query]";
            return await getAllToList(query);
        }
        public async static Task<List<SchedulerApp_Query>> getAllToListByIdAsync(string idReport)
        {
            string query = $@"SELECT Query.* 
FROM [UHCDB].dbo.[SchedulerApp_Query] Query
JOIN [UHCDB].dbo.[SchedulerApp_Report_Query] RP ON
Query.id = RP.idQuery
WHERE RP.idReport = {idReport}";
            return await getAllToList(query);
        }
        /** Update **/
        public async static Task<string> updateAsync(SchedulerApp_Query query)
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

                    command.CommandText = $@"UPDATE [UHCDB].dbo.[SchedulerApp_Query]
                                              SET 
                                                  [description] = '{query.description}'
                                                 ,[observation] = '{query.observation}'                                                 
                                                 ,[SQLcode] = '{query.SQLcode.Replace("'","`")}'                                                 
                                            WHERE [id] =  {query.id}";


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
        
        /** Execution **/
        public async static Task<Tuple<string, DataTable>> ExecuteAsync(string query, SchedulerApp_Connection connection, int pageIndex = 0, int pageSize = 2500)
        {
            string outputMessage = null;
            DataTable outputData = new DataTable();
            TimeSpan executionTime = TimeSpan.Zero;

            bool isSelectQuery = Regex.IsMatch(query, @"^\s*SELECT", RegexOptions.IgnoreCase);
            bool hasFromClause = Regex.IsMatch(query, @"\bFROM\b", RegexOptions.IgnoreCase);

            // Se for uma consulta SELECT que tem uma cláusula FROM, tentamos aplicar a paginação
            if (isSelectQuery && hasFromClause)
            {
                query = $@"
WITH PaginatedResults AS
(
    SELECT 
   ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS RowNum
    ,*
    FROM ({query}) AS SubQuery
)
SELECT * FROM PaginatedResults WHERE RowNum BETWEEN (@PageIndex * @PageSize + 1) AND ((@PageIndex + 1) * @PageSize)";
            }

            using (SqlConnection conn = new Connection().getConnectionApp(connection))
            {
                try
                {
                    await conn.OpenAsync();
                    SqlCommand command = conn.CreateCommand();
                    command.CommandText = query;
                    if (isSelectQuery && hasFromClause)
                    {
                        command.Parameters.AddWithValue("@PageIndex", pageIndex);
                        command.Parameters.AddWithValue("@PageSize", pageSize);
                    }
                    command.CommandTimeout = 200;

                    Stopwatch stopwatch = Stopwatch.StartNew();

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(outputData);

                    executionTime = stopwatch.Elapsed;
                    outputMessage = $"Comando executado com sucesso. Execution Time: {executionTime.TotalMilliseconds} ms. {outputData.Rows.Count}/{pageSize} linhas.";

                }
                catch (SqlException ex)
                {
                    outputMessage = "Erro SQL: " + ex.Message;
                    outputData = null;
                }
                catch (Exception ex)
                {
                    outputMessage = "Erro: " + ex.Message;
                    outputData = null;
                }
                finally
                {
                    conn.Close();
                }
            }

            return new Tuple<string, DataTable>(outputMessage, outputData);
        }
        public async static Task<Tuple<string, DataTable>> ExecuteAsync(string query, SchedulerApp_Connection connection)
        {
            string outputMessage = null;
            DataTable outputData = new DataTable();
            TimeSpan executionTime = TimeSpan.Zero;
            using (SqlConnection conn = new Connection().getConnectionApp(connection))
            {
                try
                {
                    await conn.OpenAsync();
                    SqlCommand command = conn.CreateCommand();
                    command.CommandText = query;                    
                    command.CommandTimeout = 200;

                    Stopwatch stopwatch = Stopwatch.StartNew();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(outputData);

                    executionTime = stopwatch.Elapsed;
                    outputMessage = $"Comando executado com sucesso. Execution Time: {executionTime.TotalMilliseconds} ms. Retornado {outputData.Rows.Count} linhas.";

                }
                catch (SqlException ex)
                {
                    outputMessage = "Erro SQL: " + ex.Message;
                    outputData = null;
                }
                catch (Exception ex)
                {
                    outputMessage = "Erro: " + ex.Message;
                    outputData = null;
                }
                finally
                {
                    conn.Close();
                }
            }

            return new Tuple<string, DataTable>(outputMessage, outputData);
        }
        public async static Task<int> GetRowCountAsync(string query, SchedulerApp_Connection connection)
        {
            int rowCount = 0;

            // Cria uma consulta de contagem baseada na consulta original
            string countQuery = $"SELECT COUNT(*) FROM ({query}) AS CountSubQuery";

            using (SqlConnection conn = new Connection().getConnectionApp(connection))
            {
                try
                {
                    await conn.OpenAsync();
                    SqlCommand command = conn.CreateCommand();
                    command.CommandText = countQuery;
                    command.CommandTimeout = 200;

                    rowCount = (int)await command.ExecuteScalarAsync();
                }
                catch
                {
                    // Em caso de exceção, podemos retornar 0 ou manipular conforme necessário
                    rowCount = 0;
                }
                finally
                {
                    conn.Close();
                }
            }

            return rowCount;
        }


    }

}
