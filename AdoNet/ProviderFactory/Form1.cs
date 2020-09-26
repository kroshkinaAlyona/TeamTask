using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProviderFactory
{
    public partial class Form1 : Form
    {
        DbConnection conn = null;
        DbProviderFactory fact = null;

        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable t = DbProviderFactories.GetFactoryClasses();
            dataGridView1.DataSource = t;
            comboBox1.Items.Clear();
            foreach (DataRow dr in t.Rows)
            {
                comboBox1.Items.Add(dr["InvariantName"]);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = GetConnectionStringByProvider(comboBox1.SelectedItem.ToString());

                fact = DbProviderFactories.GetFactory(comboBox1.SelectedItem.ToString());
                conn = fact.CreateConnection();
                conn.ConnectionString = textBox1.Text;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        private string GetConnectionStringByProvider(string providerName)
        {
            var settings = ConfigurationManager.ConnectionStrings;
            if (settings != null)
            {
                foreach (ConnectionStringSettings cs in settings)
                {
                    if (cs.ProviderName == providerName)
                        return cs.ConnectionString;
                }
            }
            return string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var adapter = fact.CreateDataAdapter();
                adapter.SelectCommand = conn.CreateCommand();
                adapter.SelectCommand.CommandText = textBox2.Text.ToString();

                var set = new DataSet();
                adapter.Fill(set, "Books");

                DataViewManager dvm = new DataViewManager(set);
                //dvm.DataViewSettings["Books"].RowFilter   = $"Id < 10";
                dvm.DataViewSettings["Books"].Sort = "Title ASC";
                var view = dvm.CreateDataView(set.Tables["Books"]);

                dataGridView1.DataSource = view;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Any())
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {          
            DbTransaction tran = null;
            try
            {
                conn.Open();
                
                using (var comm = conn.CreateCommand())
                {
                    tran = conn.BeginTransaction();

                    comm.Transaction = tran;
                    comm.CommandText = @"create table tmp3(id int not null identity(1,1) primary key, f1 varchar(100), f2 int)";
                    comm.ExecuteNonQuery();
                    comm.CommandText = @"insert into tmp3(f1, f2) values('Text value 1', 555)";
                    comm.ExecuteNonQuery();
                    comm.CommandText = @"insert into tmp3(f1, f2) values('Text value for second row', 777)";
                    comm.ExecuteNonQuery();

                    tran.Commit();
                }                                
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                tran.Rollback();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            const string asyncEnable = "Asynchronous Processing=true";
            if (!textBox1.Text.Contains(asyncEnable))
            {
                textBox1.Text = string.Format("{0};{1}", textBox1.Text, asyncEnable);
            }
            conn.ConnectionString = textBox1.Text;
            conn.Open();

            using (var comm = (conn as SqlConnection).CreateCommand())
            {
                comm.CommandText = $"WAITFOR DELAY '00:00:05';{textBox2.Text};";
                comm.CommandType = CommandType.Text;
                //comm.CommandTimeout = 10;
              
                comm.BeginExecuteReader(Callback, comm);
                MessageBox.Show("Added thread is working...");
            }
        }

        DataTable table = null;
        private void Callback(IAsyncResult result)
        {
            try
            {
                SqlCommand command = (SqlCommand)result.AsyncState;
                var dataReader = command.EndExecuteReader(result);

                table = new DataTable();
                do
                {
                    for (int i = 0; i < dataReader.FieldCount; i++)
                        table.Columns.Add(dataReader.GetName(i));

                    while (dataReader.Read())
                    {
                        var row = table.NewRow();
                        for (int i = 0; i < dataReader.FieldCount; i++)
                            row[i] = dataReader[i];

                        table.Rows.Add(row);
                    };
                }
                while (dataReader.NextResult());

                if (conn != null)
                    conn.Close();
               
                ShowData();
            }
            catch (Exception exp)
            {
                MessageBox.Show("Callback" + exp.ToString());
            }
        }

        private void ShowData()
        {
            if (dataGridView1.InvokeRequired)
            {
                dataGridView1.Invoke(new Action(ShowData));
                return;
            }
            dataGridView1.DataSource = table;
        }
    }
}
