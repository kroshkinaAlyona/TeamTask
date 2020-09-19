using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace PictureLibrary
{
    public partial class Form1 : Form
    {

        DataSet _set;
        SqlDataAdapter _adapter;
        SqlCommandBuilder _sqlCommandBuilder;
        string _fileName;

        SqlConnection conn = new SqlConnection
        {
            ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library; Integrated Security=SSPI;"
        };

        public Form1()
        {
            InitializeComponent();

            this.Text = "Picture Library";
        }

        private void loadPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                //Filter = "Graphics File|*.bmp;*.gif;*.jpg;*.png",
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _fileName = openFile.FileName;
                    var bytes = CreateCopyImage(_fileName);
                    conn.Open();

                    if ((toolStripTextBox1.Text?.Length ?? 0) != 0 &&
                        int.TryParse(toolStripTextBox1.Text, out int index))
                    {
                        var comm = new SqlCommand("Insert into Pictures (bookid, name, picture) values (@bookid, @name, @picture)", conn);
                        comm.Parameters.AddWithValue("@bookid", index);
                        comm.Parameters.AddWithValue("@name", Path.GetFileName(_fileName));
                        comm.Parameters.AddWithValue("@picture", bytes);
                        comm.ExecuteNonQuery();
                        MessageBox.Show("Image was saved to DB");
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
                finally
                {
                    if (conn != null)
                        conn.Close();
                }
            }

        }

        private byte[] CreateCopyImage(string fileName)
        {
            var img = Image.FromFile(fileName);
            int maxWidht = 300;
            int maxHight = 300;
            int newWidth = (int)(img.Width * (double)maxWidht / img.Width);
            int newHeight = (int)(img.Height * (double)maxHight / img.Height);

            var imageRation = new Bitmap(newWidth, newHeight);
            var g = Graphics.FromImage(imageRation);
            g.DrawImage(img, 0, 0, newWidth, newHeight);

            using (var stream = new MemoryStream())
            using (var reader = new BinaryReader(stream))
            {
                imageRation.Save(stream, ImageFormat.Jpeg);
                stream.Flush();
                stream.Seek(0, SeekOrigin.Begin);

                return reader.ReadBytes((int)stream.Length);
            }
        }

        private void showOneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((toolStripTextBox1.Text?.Length ?? 0) != 0 &&
                   int.TryParse(toolStripTextBox1.Text, out int index))
            {
                _adapter = new SqlDataAdapter("select picture from Pictures where id=@id", conn);
                _adapter.SelectCommand.Parameters.AddWithValue("@id", index);
                _adapter.TableMappings.Add("Table", "Pictures");
                var cmdBuider = new SqlCommandBuilder(_adapter);

                _set = new DataSet();
                _adapter.Fill(_set);

                using (var stream = new MemoryStream((byte[]) _set.Tables["Pictures"].Rows[0]["picture"]))
                {
                    this.pictureBox1.Image = Image.FromStream(stream);
                }
            }
            else
            {
                MessageBox.Show("Load ID!!!");
            }
        }

        private void shawAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _adapter = new SqlDataAdapter("select * from Pictures; ", conn);
                 SqlCommandBuilder cmb = new SqlCommandBuilder(_adapter);
                _set = new DataSet();
                _adapter.Fill(_set, "picture");
                this.dataGridView1.DataSource = _set.Tables["picture"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
