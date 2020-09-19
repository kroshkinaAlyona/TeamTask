using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestDataTable
{
    public partial class Form1 : Form
    {
        static readonly SqlConnection conn = new SqlConnection
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["LibraryConnection"].ConnectionString
        };

        string connectionString = ConfigurationManager.ConnectionStrings["LibraryConnection"].ConnectionString;
 

        DataTable table;

        public Form1()
        {
            InitializeComponent();
        }

        void DataTest()
        {
            SqlCommand comm = new SqlCommand
            {
                CommandText = textBox1.Text,
                Connection = conn
            };
            conn.Open();
            table = new DataTable();
            using (var reader = comm.ExecuteReader())
            {

                do
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                        table.Columns.Add(reader.GetName(i));

                    while (reader.Read())
                    {
                        DataRow row = table.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                            row[i] = reader[i];

                        table.Rows.Add(row);
                    }
                } while (reader.NextResult());

                dataGridView1.DataSource = table;
            }
            conn.Close();
        }

        DataSet set;
        SqlDataAdapter da;
        private void fill_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn =  new SqlConnection(connectionString);
                
                set = new DataSet();
                da = new SqlDataAdapter(textBox1.Text, conn);

                var cmd = new SqlCommandBuilder(da);

                //SqlCommand UpdateCmd = new SqlCommand("Update Books set Price = @pPrice where id = @pId", conn);
                //UpdateCmd.Parameters.Add(new SqlParameter("@pPrice", SqlDbType.Int));
                //UpdateCmd.Parameters["@pPrice"].SourceVersion = DataRowVersion.Current;
                //UpdateCmd.Parameters["@pPrice"].SourceColumn = "Price";
                //UpdateCmd.Parameters.Add(new SqlParameter("@pId", SqlDbType.Int));
                //UpdateCmd.Parameters["@pId"].SourceVersion = DataRowVersion.Original;
                //UpdateCmd.Parameters["@pId"].SourceColumn = "id";

                SqlCommand updateCommand = new SqlCommand("UpdateBooks", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlParameterCollection cparams = updateCommand.Parameters;
                cparams.Add("@pid", SqlDbType.Int, 0, "id");
                cparams["@pid"].SourceVersion = DataRowVersion.Original;
                cparams.Add("@pAuthorId", SqlDbType.Int, 8, "AuthorId");
                cparams.Add("@pTitle", SqlDbType.NChar, 100, "Title");
                cparams.Add("@pPrice", SqlDbType.Int, 8, "Price");
                cparams.Add("@pPages", SqlDbType.Int, 8, "Pages");
                da.UpdateCommand = updateCommand;

                da.Fill(set, "mybook");
                dataGridView1.DataSource = set.Tables["mybook"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void update_Click(object sender, EventArgs e)
        {
            da.Update(set, "mybook");
        }
    }
    
}
