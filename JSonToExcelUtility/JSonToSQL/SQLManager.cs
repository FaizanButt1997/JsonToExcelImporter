using Intelligize.Commons.DBConnectionString;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSonToExcelUtility.JSonToSQL
{
    public class SQLManager
    {
        public static int StoreInDB(string query)
        {
            try
            {
                var id = ExecuteNonQuery(query);
                return id;
            }
            catch (Exception e)
            {
                int a = 0;
                return a;
            }
        }
        public static int ExecuteNonQuery(string query)
        {
            int rows = -1;
            int tries = 1;
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = query;
            string connectionString = string.Empty;

            connectionString = DBConnectionStringFactory.GetConnectionString(Database.Companies, PermissionGroup.PowerWorker);
            while (tries <= 10)
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandTimeout = 3600;
                        rows = cmd.ExecuteNonQuery();
                        rows = (int)cmd.LastInsertedId;
                        break;
                    }
                    catch (MySqlException ex)
                    {
                        if (!ex.Message.ToLower().Contains("deadlock"))
                        {
                            throw;
                        }
                        Thread.Sleep(100);
                        tries++;
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return rows;
        }
        public static int SelectInDB(string query)
        {
            MySqlConnectionStringBuilder cs = new MySqlConnectionStringBuilder(DBConnectionStringFactory.GetConnectionString(Database.Companies, PermissionGroup.Power));
            try
            {
                var dt = MySqlHelper.ExecuteDataset(cs.ConnectionString, string.Format(query)).Tables[0];
                return dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["id"].ToString()) : 0;
            }
            catch (Exception e)
            {
                int a = 0;
                return a;
            }
        }
    }
}
