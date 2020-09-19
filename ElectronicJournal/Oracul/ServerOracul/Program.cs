using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerOracul
{
    class Program
    {
        static readonly TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), Convert.ToInt32("1024"));


        private static void ThreadFun()
        {
            while (true)
            {
                var client = listener.AcceptTcpClient();

                //Task thread = Task.Run(() =>
                {
                    try
                    {
                        Console.WriteLine("Client connected");
                        var nstream = client.GetStream();

                        //while(nstream.e)
                        StreamReader sr = new StreamReader(nstream, Encoding.ASCII);
                        string s = sr.ReadLine();
                        Console.WriteLine(s);
                        if (s != "EXIT")
                        {
                            byte[] barray = Encoding.ASCII.GetBytes("Some Prediction");
                            nstream.Write(barray, 0, barray.Length);
                        }
                        else
                        {
                           // byte[] barray = Encoding.ASCII.GetBytes("Some Prediction");
                           // nstream.Write(barray, 0, barray.Length);
                        }
                        Console.WriteLine("Client connected completed");


                        client.Close();
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp);
                    }
                    //});
                }
            }
        }


        static void Main(string[] args)
        {
            listener.Start();
            Thread thread = new Thread(new ThreadStart(ThreadFun))
            {
                IsBackground = true
            };
            thread.Start();

            Console.WriteLine("Server is running...");
            Console.ReadKey();
        }
    }
}
