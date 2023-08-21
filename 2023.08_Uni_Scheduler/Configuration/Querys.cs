using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using System.Windows.Forms;

namespace _2023._08_Uni_Scheduler.Configuration
{
    public class Querys<Gen> where Gen : new()
    {
        public List<Gen> MapResultsToObject(IDataReader reader)
        {
            List<Gen> objects = new List<Gen>();

            while (reader.Read())
            {
                objects.Add(MapRow(reader));
            }

            return objects;
        }
        private Gen MapRow(IDataReader reader)
        {
            Gen item = new Gen();

            var typeProperties = typeof(Gen).GetProperties();

            foreach (var property in typeProperties)
            {
                int ordinal = reader.GetOrdinal(property.Name);

                if (!reader.IsDBNull(ordinal))
                {
                    property.SetValue(item, reader[ordinal].ToString(), null);
                }
            }

            return item;
        }

        /** List of GETs **/
        public async static Task<List<Gen>> getAllToList(string query)
        {
            using (SqlConnection conn = new Connection().getConnectionApp())
            {
                try
                {
                    await conn.OpenAsync();
                    SqlCommand command = conn.CreateCommand();
                    command.CommandText = query;

                    Querys<Gen> resultsMapper = new Querys<Gen>();

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    List<Gen> results = resultsMapper.MapResultsToObject(reader);
                    return results;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public async static Task<Gen> getToClass(string query)
        {
            using (SqlConnection conn = new Connection().getConnectionApp())
            {
                try
                {
                    await conn.OpenAsync();
                    SqlCommand command = conn.CreateCommand();
                    command.CommandText = query;

                    Querys<Gen> resultsMapper = new Querys<Gen>();

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    List<Gen> results = resultsMapper.MapResultsToObject(reader);
                    return results[0];

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Querys.getToClass method error => " + ex.Message);
                    return new Gen();
                }
            }
        }
        public async static Task<DataTable> getAllToDataTable(string query)
        {
            using (SqlConnection conn = new Connection().getConnectionApp())
            {
                try
                {
                    await conn.OpenAsync();

                    SqlCommand command = conn.CreateCommand();
                    command.CommandText = query;
                    command.CommandTimeout = 200;
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public async static Task<int> getNextCodeAsync()
        {
            int nextCode = 0;
            using (SqlConnection conn = new Connection().getConnectionApp())
            {
                try
                {
                    await conn.OpenAsync();
                    SqlCommand command = conn.CreateCommand();
                    command.CommandText = $"SELECT nextCode = iif(ident_current('[UHCDB].dbo.[{typeof(Gen).Name}]') = 1, 1, ident_current('[UHCDB].dbo.[{typeof(Gen).Name}]') + 1)";


                    
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            nextCode = Convert.ToInt32(reader["nextCode"].ToString());
                        }
                    }

                    return nextCode;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return 0;
                }
            }

        }
        public async static Task<int> getLastCodeAsync()
        {
            int lastCode = 0;
            using (SqlConnection conn = new Connection().getConnectionApp())
            {
                try
                {
                    await conn.OpenAsync();
                    SqlCommand command = conn.CreateCommand();
                    command.CommandText = $"SELECT lastCode = ident_current ('[UHCDB].dbo.[{typeof(Gen).Name}]')";

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            lastCode = Convert.ToInt32(reader["lastCode"].ToString());
                        }
                    }

                    return lastCode;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return 0;
                }
            }


        }

        /** Insert **/
        public async static Task insertAsync(List<Gen> gens)
        {
            using (SqlConnection conn = Connection.getInstancia().getConnectionApp())
            {
                await conn.OpenAsync();
                SqlTransaction transaction = conn.BeginTransaction();

                using (var bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, transaction))
                {
                    bulkCopy.BatchSize = 100;
                    bulkCopy.DestinationTableName = $"[UHCDB].dbo.[{typeof(Gen).Name}]";
                    try
                    {
                        await bulkCopy.WriteToServerAsync(gens.AsDataTable());
                        Console.WriteLine(gens.AsDataTable());

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to insert {ex.Message}");
                        transaction.Rollback();
                        conn.Close();
                    }
                    finally
                    {
                        transaction.Commit();
                        conn.Close();
                    }
                }
            }
        }

       
        }
}
