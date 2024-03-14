using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nakliye
{
    internal class AraclarDatabase
    {

        DataTable dTable;
        SqlDataAdapter sqlDA;
        public SqlDataReader reader;

        SqlCommand sqlCmd;
        SqlConnection sqlCon;
        string conStr;


        public void DataConnector()
        {
            sqlCon = new SqlConnection(conStr);
            sqlCmd = sqlCon.CreateCommand();
            sqlCmd.CommandType = CommandType.Text;

        }

        public AraclarDatabase()
        {
            conStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Cemalettin\\Desktop\\Nakliye Proje\\Nakliye\\Nakliye\\Database1.mdf\";Integrated Security=True";
            sqlCon = new SqlConnection(conStr);
            sqlCon.Open();
        }

        public DataTable selectCmd(string sql)
        {
            dTable = new DataTable();
            sqlDA = new SqlDataAdapter(sql, conStr);
            sqlDA.Fill(dTable);
            sqlDA.Dispose();
            return dTable;
        }

        public void readData(string sql)
        {
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand(sql, sqlCon);
        }

        public int cudCMD(string sql)
        {
            sqlCmd = new SqlCommand(sql, sqlCon);
            return sqlCmd.ExecuteNonQuery();
        }


    }
}
