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

            


        }

        void SendMessage(string message)
        {
            try
            {

                endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Convert.ToInt32("1024"));
                label3.Text = "Можешь задать вопрос";
                client = new TcpClient();
                client.Connect(endPoint);
                nstream = client.GetStream();


                byte[] barray = Encoding.ASCII.GetBytes(message);
                nstream.Write(barray, 0, barray.Length);

                StreamReader sr = new StreamReader(nstream, Encoding.ASCII);
                string s = sr.ReadLine();
                MessageBox.Show(s);
                //textBox2.Text =  s;
                
                client.Close();

            }
            catch (SocketException sockEx)
            {
                MessageBox.Show("Оракул не доступен, эфир затуманен");
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Exception :" + Ex.Message);
            }
        }


        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            SendMessage(textBox1.Text);           
        }

       

        private void button1_Click(object sender, EventArgs e)
        {           
            button1.Visible = false;
            label3.Text = "Нажми на кристалл, чтобы установить связь с Оракулом";

            SendMessage("EXIT");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            client.Close();
        }
    }
}
