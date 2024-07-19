using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzeWater
{
    public class OracleDatabase
    {
        //private string _connectionString = "User Id=tst_user;Password=password;Data Source=192.168.1.12:1521/XEPDB1;Pooling=false;";
        private string _connectionString = "DATA SOURCE=192.168.1.12:1521/XEPDB1;TNS_ADMIN=C:\\Users\\MAD1\\Oracle\\network\\admin;PERSIST SECURITY INFO=True;USER ID=TST_USER;Password=password";

        public OracleDatabase()
        {

        }

        private OracleConnection GetConnection()
        {
            return new OracleConnection(_connectionString);
        }

        public void testconnections()
        {
            OracleConnection conn = GetConnection();
            conn.Open();
            MessageBox.Show("all good");
        }


        public DataTable GetAllWaterData()
        {
            DataTable dataTable = new DataTable();

            using (OracleConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM WATERDB";

                    using (OracleCommand command = new OracleCommand(query, conn))
                    {
                        using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message);
                }
                conn.Close();
            }
            return dataTable;
        }
    }
}
