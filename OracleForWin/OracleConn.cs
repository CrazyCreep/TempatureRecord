using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace OracleForWin
{
    class OracleConn
    {
        //private string OracleCon = "Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.0.236)(PORT = 1522))(CONNECT_DATA =(SERVICE_NAME = orcl)));Persist Security Info = True;User ID = system; Password = root;";
        //ORA-12541
        //private string OracleCon = "Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.0.236)(PORT = 1521))(CONNECT_DATA =(SERVICE_NAME = ORA-12541)));Persist Security Info = True;User ID = system; Password = root;";
        private static string OracleCon= "User Id = system;Password = root;Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.0.236)(PORT = 1521)))(CONNECT_DATA = (SERVICE_NAME = orcl)))";
        public DataTable ExecuteQuery(string sqlStr)
        {
            OracleConnection con = new OracleConnection(OracleCon);
            OracleCommand cmd = new OracleCommand(sqlStr, con);
            OracleDataAdapter msda = new OracleDataAdapter(cmd);
            con.Open();
            DataTable dt = new DataTable();
            msda.Fill(dt);
            con.Close();
            return dt;
        }

        public int ExecuteUpdate(string sqlStr)
        {
            OracleCommand cmd;
            OracleConnection con;
            con = new OracleConnection(OracleCon);
            con.Open();
            cmd = new OracleCommand(sqlStr, con);
            cmd.CommandType = CommandType.Text;
            int iud = 0;
            iud = cmd.ExecuteNonQuery();
            con.Close();
            return iud;
        }
    }
}
