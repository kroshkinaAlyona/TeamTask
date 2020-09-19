using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oracul
{
    public partial class Form1 : Form
    {

        TcpClient client;
        NetworkStream nstream;
        IPEndPoint endPoint;
           
        public Form1()
        {
            InitializeComponent();
            endPoint = new IPEndPoint(IPAddress.Parse(textBox1.Text), Convert.ToInt32("1024"));
            label3.Text = "Можешь задать вопрос";       
        }

        void SendMessage()
        {
            try
            {
                var client = new TcpClient();
                client.Connect(endPoint);
                var nstream = client.GetStream();

                //byte[] barray = Encoding.Unicode.GetBytes("message");
                //nstream.Flush();
                //nstream.Write(barray, 0, barray.Length);
                //MessageBox.Show("message");

                StreamReader sr = new StreamReader(nstream, Encoding.Unicode);
                string s = sr.ReadLine();
                textBox2.Text =  s;
                nstream.Close();
                client.Close();
            }
            catch (SocketException exp)
            {
                MessageBox.Show("Оракул не доступен, эфир затуманен :" + exp.Message);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Exception :" + Ex.Message);
            }
        }


        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            SendMessage();           
        }
     
        private void button1_Click(object sender, EventArgs e)
        {           
            button1.Visible = false;
            label3.Text = "Нажми на кристалл, чтобы установить связь с Оракулом";

            SendMessage();
        }
    }
}
