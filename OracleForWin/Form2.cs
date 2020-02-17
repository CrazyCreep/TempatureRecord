using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OracleForWin
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void myLocation()
        {
            ArrayList list = new ArrayList();
            string OracleCon = "User Id = system;Password = root;Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.0.236)(PORT = 1521)))(CONNECT_DATA = (SERVICE_NAME = orcl)))";
            string str = "select distinct 区域 from heg_temp_record";
            OracleConnection con = new OracleConnection(OracleCon);
            OracleCommand cmd = new OracleCommand(str, con);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                //dr[0]表示取结果的第一列，dr[1]就是第二列
                list.Add(dr[0].ToString().Trim());
            }
            comboBox1.DataSource = list;
            con.Close();
            this.comboBox1.SelectedIndex = -1;
        }

        private void myManage()
        {
            ArrayList list = new ArrayList();
            string OracleCon = "User Id = system;Password = root;Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.0.236)(PORT = 1521)))(CONNECT_DATA = (SERVICE_NAME = orcl)))";
            string str = "select distinct 管理部门 from heg_temp_record";
            OracleConnection con = new OracleConnection(OracleCon);
            OracleCommand cmd = new OracleCommand(str, con);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                //dr[0]表示取结果的第一列，dr[1]就是第二列
                list.Add(dr[0].ToString().Trim());
            }
            comboBox2.DataSource = list;
            con.Close();
            this.comboBox2.SelectedIndex = -1;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            myLocation();
            myManage();
            dateTimePicker1.Text = DateTime.Now.ToString();
            dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string dateStart=dateTimePicker1.Value.ToString("yyyy-MM-dd");
            dateTimePicker2.Text = DateTime.Now.ToString();
            dateTimePicker2.Value.ToString("yyyy-MM-dd");
            string dateEnd = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            string H00 = "00:00:00";
            string H24 = "23:29:59";
            OracleConn conn = new OracleConn();
            //string sql = "select 序号,区域,工号，姓名，管理部门，温度，时间 from heg_temp_record where 时间> sysdate+1";
            string sql = "select a.序号,a.区域,a.工号，a.姓名，a.管理部门，a.温度，a.时间 from heg_temp_record a left join (Select 序号,to_char(时间, 'YYYY-MM-DD HH24:MI:SS') as 时间2 From heg_temp_record)b on a.序号=b.序号 where  b.时间2 between '" + dateStart + " " + H00 + "' and '" + dateEnd + " " + H24 + "' order by 时间2";
            dataGridView1.DataSource = conn.ExecuteQuery(sql);
            this.textBox2.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "";
            OracleConn conn = new OracleConn();
            string location = "'" + comboBox1.Text + "'";
            string part = "'" + comboBox2.Text + "'";
            string number = "'" + textBox2.Text + "'";
            string name = "'" + textBox3.Text + "'";
            dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string dateStart = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string dateEnd = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            string H00 = "00:00:00";
            string H24 = "23:59:59";
            if (comboBox1.Text == "" && comboBox2.Text == "" && textBox2.Text == "" && textBox3.Text == "")
            {
                //sql = "select 序号,区域,工号，姓名，管理部门，温度，时间 from heg_temp_record  order by 时间 desc";
                  sql = "select a.序号,a.区域,a.工号，a.姓名，a.管理部门，a.温度，a.时间 from heg_temp_record a left join (Select 序号,to_char(时间, 'YYYY-MM-DD HH24:MI:SS') as 时间2 From heg_temp_record)b on a.序号=b.序号 where  b.时间2 between '" + dateStart + " " + H00 + "' and '" + dateEnd + " " + H24 + "' order by 时间2";           
            }
            if (comboBox1.Text == "" && comboBox2.Text == "" && textBox2.Text == "" && textBox3.Text != "")
            {
                sql = "select a.序号,a.区域,a.工号，a.姓名，a.管理部门，a.温度，a.时间 from heg_temp_record a left join (Select 序号,to_char(时间, 'YYYY-MM-DD HH24:MI:SS') as 时间2 From heg_temp_record)b on a.序号=b.序号 where  b.时间2 between '" + dateStart + " " + H00 + "' and '" + dateEnd + " " + H24 + "' and 姓名 like '%" + textBox3.Text + "%'";
            }
            if (comboBox1.Text == "" && comboBox2.Text == "" && textBox2.Text != "" && textBox3.Text == "")
            {
                sql = "select a.序号,a.区域,a.工号，a.姓名，a.管理部门，a.温度，a.时间 from heg_temp_record a left join (Select 序号,to_char(时间, 'YYYY-MM-DD HH24:MI:SS') as 时间2 From heg_temp_record)b on a.序号=b.序号 where  b.时间2 between '" + dateStart + " " + H00 + "' and '" + dateEnd + " " + H24 + "' and 工号 like '%" + textBox2.Text + "%'";            
            }
            if (comboBox1.Text == "" && comboBox2.Text == "" && textBox2.Text != "" && textBox3.Text != "")
            {
                sql = "select a.序号,a.区域,a.工号，a.姓名，a.管理部门，a.温度，a.时间 from heg_temp_record a left join (Select 序号,to_char(时间, 'YYYY-MM-DD HH24:MI:SS') as 时间2 From heg_temp_record)b on a.序号=b.序号 where  b.时间2 between '" + dateStart + " " + H00 + "' and '" + dateEnd + " " + H24 + "' and 工号 like '%" + textBox2.Text + "%' and 姓名 like '%" + textBox3.Text + "%'";          
            }
            
            
            if (comboBox1.Text == "" && comboBox2.Text != "" && textBox2.Text == "" && textBox3.Text == "")
            {
                sql = "select a.序号,a.区域,a.工号，a.姓名，a.管理部门，a.温度，a.时间 from heg_temp_record a left join (Select 序号,to_char(时间, 'YYYY-MM-DD HH24:MI:SS') as 时间2 From heg_temp_record)b on a.序号=b.序号 where  b.时间2 between '" + dateStart + " " + H00 + "' and '" + dateEnd + " " + H24 + " 'and 管理部门 =" + part;
            }
            if (comboBox1.Text == "" && comboBox2.Text != "" && textBox2.Text == "" && textBox3.Text != "")
            {
                sql = "select a.序号,a.区域,a.工号，a.姓名，a.管理部门，a.温度，a.时间 from heg_temp_record a left join (Select 序号,to_char(时间, 'YYYY-MM-DD HH24:MI:SS') as 时间2 From heg_temp_record)b on a.序号=b.序号 where  b.时间2 between '" + dateStart + " " + H00 + "' and '" + dateEnd + " " + H24 + " 'and 管理部门 =" + part + " and 姓名 like '%" + textBox3.Text + "%'";
            }
            if (comboBox1.Text == "" && comboBox2.Text != "" && textBox2.Text != "" && textBox3.Text == "")
            {
                sql = "select a.序号,a.区域,a.工号，a.姓名，a.管理部门，a.温度，a.时间 from heg_temp_record a left join (Select 序号,to_char(时间, 'YYYY-MM-DD HH24:MI:SS') as 时间2 From heg_temp_record)b on a.序号=b.序号 where  b.时间2 between '" + dateStart + " " + H00 + "' and '" + dateEnd + " " + H24 + " 'and 管理部门 =" + part + " and 工号 like '%" + textBox2.Text + "%'";
            }
            if (comboBox1.Text == "" && comboBox2.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                sql = "select a.序号,a.区域,a.工号，a.姓名，a.管理部门，a.温度，a.时间 from heg_temp_record a left join (Select 序号,to_char(时间, 'YYYY-MM-DD HH24:MI:SS') as 时间2 From heg_temp_record)b on a.序号=b.序号 where  b.时间2 between '" + dateStart + " " + H00 + "' and '" + dateEnd + " " + H24 + " 'and 管理部门 =" + part + " and 工号 like '%" + textBox2.Text + "%' and 姓名 like '%" + textBox3.Text + "%'";
            }

            if (comboBox1.Text != "" && comboBox2.Text == "" && textBox2.Text == "" && textBox3.Text == "")
            {
                sql = "select a.序号,a.区域,a.工号，a.姓名，a.管理部门，a.温度，a.时间 from heg_temp_record a left join (Select 序号,to_char(时间, 'YYYY-MM-DD HH24:MI:SS') as 时间2 From heg_temp_record)b on a.序号=b.序号 where  b.时间2 between '" + dateStart + " " + H00 + "' and '" + dateEnd + " " + H24 + " 'and 区域 =" + location;
            }
            if (comboBox1.Text != "" && comboBox2.Text == "" && textBox2.Text == "" && textBox3.Text != "")
            {
                sql = "select a.序号,a.区域,a.工号，a.姓名，a.管理部门，a.温度，a.时间 from heg_temp_record a left join (Select 序号,to_char(时间, 'YYYY-MM-DD HH24:MI:SS') as 时间2 From heg_temp_record)b on a.序号=b.序号 where  b.时间2 between '" + dateStart + " " + H00 + "' and '" + dateEnd + " " + H24 + " 'and 区域 =" + location + " and 姓名 like '%" + textBox3.Text + "%'";
            }
            if (comboBox1.Text != "" && comboBox2.Text == "" && textBox2.Text != "" && textBox3.Text == "")
            {
                sql = "select a.序号,a.区域,a.工号，a.姓名，a.管理部门，a.温度，a.时间 from heg_temp_record a left join (Select 序号,to_char(时间, 'YYYY-MM-DD HH24:MI:SS') as 时间2 From heg_temp_record)b on a.序号=b.序号 where  b.时间2 between '" + dateStart + " " + H00 + "' and '" + dateEnd + " " + H24 + " 'and 区域 =" + location + " and 工号 like '%" + textBox2.Text + "%'";
            }
            if (comboBox1.Text != "" && comboBox2.Text == "" && textBox2.Text != "" && textBox3.Text != "")
            {
                sql = "select a.序号,a.区域,a.工号，a.姓名，a.管理部门，a.温度，a.时间 from heg_temp_record a left join (Select 序号,to_char(时间, 'YYYY-MM-DD HH24:MI:SS') as 时间2 From heg_temp_record)b on a.序号=b.序号 where  b.时间2 between '" + dateStart + " " + H00 + "' and '" + dateEnd + " " + H24 + " 'and 区域 =" + location + " and 工号 like '%" + textBox2.Text + "%' and 姓名 like '%" + textBox3.Text + "%'";
            }
            if(comboBox1.Text != "" && comboBox2.Text != "" && textBox2.Text == "" && textBox3.Text == "")
            {
                sql = "select a.序号,a.区域,a.工号，a.姓名，a.管理部门，a.温度，a.时间 from heg_temp_record a left join (Select 序号,to_char(时间, 'YYYY-MM-DD HH24:MI:SS') as 时间2 From heg_temp_record)b on a.序号=b.序号 where  b.时间2 between '" + dateStart + " " + H00 + "' and '" + dateEnd + " " + H24 + " 'and 区域 =" + location + " and 管理部门 =" + part;
            }
            if (comboBox1.Text != "" && comboBox2.Text != "" && textBox2.Text == "" && textBox3.Text != "")
            {
                sql = "select a.序号,a.区域,a.工号，a.姓名，a.管理部门，a.温度，a.时间 from heg_temp_record a left join (Select 序号,to_char(时间, 'YYYY-MM-DD HH24:MI:SS') as 时间2 From heg_temp_record)b on a.序号=b.序号 where  b.时间2 between '" + dateStart + " " + H00 + "' and '" + dateEnd + " " + H24 + " 'and 区域 =" + location + " and 管理部门 =" + part + " and 姓名 like '%" + textBox3.Text + "%'";
            }
            if(comboBox1.Text != "" && comboBox2.Text != "" && textBox2.Text != "" && textBox3.Text == "")
            {
                sql = "select a.序号,a.区域,a.工号，a.姓名，a.管理部门，a.温度，a.时间 from heg_temp_record a left join (Select 序号,to_char(时间, 'YYYY-MM-DD HH24:MI:SS') as 时间2 From heg_temp_record)b on a.序号=b.序号 where  b.时间2 between '" + dateStart + " " + H00 + "' and '" + dateEnd + " " + H24 + " 'and 区域 =" + location + " and 管理部门 =" + part + " and 工号 like '%" + textBox2.Text + "%'";
            }
            if (comboBox1.Text != "" && comboBox2.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                sql = "select a.序号,a.区域,a.工号，a.姓名，a.管理部门，a.温度，a.时间 from heg_temp_record a left join (Select 序号,to_char(时间, 'YYYY-MM-DD HH24:MI:SS') as 时间2 From heg_temp_record)b on a.序号=b.序号 where  b.时间2 between '" + dateStart + " " + H00 + "' and '" + dateEnd + " " + H24 + " 'and 区域 =" + location + " and 管理部门 =" + part + " and 工号 like '%" + textBox2.Text + "%' and 姓名 like '%" + textBox3.Text + "%'";
            }
            dataGridView1.DataSource = conn.ExecuteQuery(sql);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            dateTimePicker1.Text = DateTime.Now.ToString();
            dateTimePicker2.Text = DateTime.Now.ToString();
            textBox2.Text = "";
            textBox3.Text = "";
            this.textBox2.Focus();
        }
    }
}
