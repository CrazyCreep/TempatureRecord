using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace OracleForWin
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            this.textBox2.KeyDown += new KeyEventHandler(textBox2_KeyDown);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OracleConn con = new OracleConn();
            string sql = "select 序号,区域,工号，姓名，管理部门，温度，时间 from heg_temp_record where 时间> sysdate-1 order by 时间 desc";
            dataGridView1.DataSource = con.ExecuteQuery(sql);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleConn conn = new OracleConn();
            string number = "'" + textBox1.Text + "'";
            string sql = "select 工号,姓名,管理部门 from heg_temp where 工号 =" + number;
            dataGridView2.DataSource = conn.ExecuteQuery(sql);
            this.textBox2.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OracleConn conn = new OracleConn();
            string number = textBox1.Text;
            string temperature = textBox2.Text;
            string location = textBox3.Text;
            string sql = "insert into heg_temp_record(工号,温度,时间,姓名,管理部门,序号,工号1,区域) values ((select 工号 from heg_temp where 工号=" + number + ")," + temperature + ",sysdate,(select 姓名 from heg_temp where 工号=" + number + "),(select 管理部门 from heg_temp where 工号=" + number + ")，（select max(序号) from HEG_TEMP_RECORD）+1， " + number + ",'" + location + "')";
            //string sql = "insert into heg_temp_record(工号,温度,时间,姓名,管理部门,序号) values (" + number + "," + temperature + ",sysdate,(select 姓名 from heg_temp where 工号=" + number + "),(select 管理部门 from heg_temp where 工号=" + number + ")，（select max(序号) from HEG_TEMP_RECORD）+1)";
            dataGridView1.DataSource = conn.ExecuteUpdate(sql);
            //string sql2 = "select 工号,温度 from heg_temp_record";
            //string sql2 = "select t.姓名,r.工号,r.温度，t.管理部门,r.时间 from heg_temp_record r left join heg_temp t on r.工号=t.工号";
            string sql2 = "select 序号,区域,工号，姓名，管理部门，温度，时间 from heg_temp_record  order by 时间 desc";
            dataGridView1.DataSource = conn.ExecuteQuery(sql2);
            textBox1.Text = "";
            textBox2.Text = "";
            this.textBox1.Focus();
            //MessageBox.Show("提交成功");
            // string sql = "update heg_temp  SET T2_10 =  " + temperature + " WHERE 工号 =" + number;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OracleConn conn = new OracleConn();
                string number = "'" + textBox1.Text + "'";
                string sql = "select 工号,姓名,管理部门 from heg_temp where 工号 =" + number;
                dataGridView2.DataSource = conn.ExecuteQuery(sql);
                label4.Text = "";
                label4.BackColor = Color.Transparent;
                this.textBox2.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OracleConn conn = new OracleConn();
                string number = textBox1.Text;
                //int.Parse(textBox2.Text);
                //int temperature = textBox2.Text;
                double temperature =Convert.ToDouble(textBox2.Text);
                string location = textBox3.Text;
                //string time1 = "Select to_char(sysdate, 'YYYY-MM-DD HH24:MI:SS') From dual";
                if (temperature >= 35 && temperature < 36)
                {
                    string sql = "insert into heg_temp_record(工号,温度,时间,姓名,管理部门,序号,工号1,区域) values ((select 工号 from heg_temp where 工号=" + number + ")," + temperature + ",sysdate,(select 姓名 from heg_temp where 工号=" + number + "),(select 管理部门 from heg_temp where 工号=" + number + ")，（select max(序号) from HEG_TEMP_RECORD）+1， " + number + ",'" + location + "')";
                    //string sql = "insert into heg_temp_record(工号,温度,时间,姓名,管理部门,序号) values (" + number + "," + temperature + ",sysdate,(select 姓名 from heg_temp where 工号=" + number + "),(select 管理部门 from heg_temp where 工号=" + number + ")，（select max(序号) from HEG_TEMP_RECORD）+1)";
                    //dataGridView1.DataSource = conn.ExecuteUpdate(sql);
                    dataGridView1.DataSource = conn.ExecuteQuery(sql);
                    //string sql2 = "select 工号,温度 from heg_temp_record";
                    //string sql2 = "select t.姓名,r.工号,r.温度，t.管理部门,r.时间 from heg_temp_record r left join heg_temp t on r.工号=t.工号";
                    //string sql2 = "select 序号,区域,工号，姓名，管理部门，温度，时间 from heg_temp_record  order by 时间 desc";
                    string sql2 = "select 序号,区域,工号，姓名，管理部门，温度，时间 from heg_temp_record where 时间> sysdate-1 order by 时间 desc";
                    dataGridView1.DataSource = conn.ExecuteQuery(sql2);
                    textBox1.Text = "";
                    textBox2.Text = "";
                    label4.Text = "FAIL";
                    label4.BackColor = Color.Red;
                    this.textBox1.Focus();
                    //MessageBox.Show("提交成功");
                    // string sql = "update heg_temp  SET T2_10 =  " + temperature + " WHERE 工号 =" + number;
                }
                else if (temperature >= 36 && temperature < 37)
                {
                    string sql = "insert into heg_temp_record(工号,温度,时间,姓名,管理部门,序号,工号1,区域) values ((select 工号 from heg_temp where 工号=" + number + ")," + temperature + ",sysdate,(select 姓名 from heg_temp where 工号=" + number + "),(select 管理部门 from heg_temp where 工号=" + number + ")，（select max(序号) from HEG_TEMP_RECORD）+1， " + number + ",'" + location + "')";                  
                    //string sql = "insert into heg_temp_record(工号,温度,时间,姓名,管理部门,序号) values (" + number + "," + temperature + ",sysdate,(select 姓名 from heg_temp where 工号=" + number + "),(select 管理部门 from heg_temp where 工号=" + number + ")，（select max(序号) from HEG_TEMP_RECORD）+1)";
                    //dataGridView1.DataSource = conn.ExecuteUpdate(sql);
                    dataGridView1.DataSource = conn.ExecuteQuery(sql);
                    //string sql2 = "select 工号,温度 from heg_temp_record";
                    //string sql2 = "select t.姓名,r.工号,r.温度，t.管理部门,r.时间 from heg_temp_record r left join heg_temp t on r.工号=t.工号";
                    //string sql2 = "select 序号,区域,工号，姓名，管理部门，温度，时间 from heg_temp_record  order by 时间 desc";
                    string sql2 = "select 序号,区域,工号，姓名，管理部门，温度，时间 from heg_temp_record where 时间> sysdate-1 order by 时间 desc";
                    dataGridView1.DataSource = conn.ExecuteQuery(sql2);
                    textBox1.Text = "";
                    textBox2.Text = "";
                    label4.Text = "PASS";
                    label4.BackColor = Color.Green;
                    this.textBox1.Focus();
                }
                else if (temperature >= 37 && temperature < 41)
                {
                    string sql = "insert into heg_temp_record(工号,温度,时间,姓名,管理部门,序号,工号1,区域) values ((select 工号 from heg_temp where 工号=" + number + ")," + temperature + ",sysdate,(select 姓名 from heg_temp where 工号=" + number + "),(select 管理部门 from heg_temp where 工号=" + number + ")，（select max(序号) from HEG_TEMP_RECORD）+1， " + number + ",'" + location + "')";
                    //string sql = "insert into heg_temp_record(工号,温度,时间,姓名,管理部门,序号) values (" + number + "," + temperature + ",sysdate,(select 姓名 from heg_temp where 工号=" + number + "),(select 管理部门 from heg_temp where 工号=" + number + ")，（select max(序号) from HEG_TEMP_RECORD）+1)";
                    //dataGridView1.DataSource = conn.ExecuteUpdate(sql);
                    dataGridView1.DataSource = conn.ExecuteQuery(sql);
                    //string sql2 = "select 工号,温度 from heg_temp_record";
                    //string sql2 = "select t.姓名,r.工号,r.温度，t.管理部门,r.时间 from heg_temp_record r left join heg_temp t on r.工号=t.工号";
                    //string sql2 = "select 序号,区域,工号，姓名，管理部门，温度，时间 from heg_temp_record  order by 时间 desc";
                    string sql2 = "select 序号,区域,工号，姓名，管理部门，温度，时间 from heg_temp_record where 时间> sysdate-1 order by 时间 desc";
                    dataGridView1.DataSource = conn.ExecuteQuery(sql2);
                    textBox1.Text = "";
                    textBox2.Text = "";
                    label4.Text = "FAIL";
                    label4.BackColor = Color.Red;
                    this.textBox1.Focus();
                }
                else
                {
                    MessageBox.Show("数据异常，请确认后输入");
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OracleConn conn = new OracleConn();
            string number = "'" + textBox1.Text + "'";
            string sql = "select * from heg_temp where 工号 =" + number;
            dataGridView2.DataSource = conn.ExecuteQuery(sql);
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}