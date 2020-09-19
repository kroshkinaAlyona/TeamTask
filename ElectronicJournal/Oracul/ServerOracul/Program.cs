using System;
using System.CodeDom;
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
        static readonly TcpListener listener =
            new TcpListener(IPAddress.Parse("192.168.88.109"), Convert.ToInt32("1024"));


        //private static void ThreadFun()
        //{
        //    while (true)
        //    {
        //        if (listener.Pending())
        //        {
        //            var client = listener.AcceptTcpClient();
        //            Task thread = Task.Run(() =>
        //            {
        //                try
        //                {
        //                    Console.WriteLine("Client connected");                    
        //                    while (true)
        //                    {
        //                        var str = client.GetStream();
        //                        Console.WriteLine("Client connected block1");
        //                        StreamReader sr = new StreamReader(str, Encoding.Unicode);

        //                        Console.WriteLine("Client connected block");
        //                        string s = sr.ReadLine();
        //                        if (s != null)
        //                        {
        //                            Console.WriteLine(s);
        //                            //var nstream = client.GetStream();
        //                            //nstream.Flush();
        //                            //byte[] barray = Encoding.ASCII.GetBytes("Some Prediction");
        //                            //nstream.Write(barray, 0, barray.Length);

        //                            //Console.WriteLine("Client connected completed");
        //                        }
        //                        str.Close();


        //                    }

        //                    //client.Close();
        //                }
        //                catch (Exception exp)
        //                {
        //                    Console.WriteLine(exp);
        //                }
        //            });
        //        };               
        //    }
        //}


        static void Main(string[] args)
        {          
            Thread thread = new Thread(new ThreadStart(ThreadFun))
            {
                IsBackground = true
            };
            thread.Start();
            listener.Start();

            Console.WriteLine("Server is running...");          
            Console.ReadKey();
        }

        private static void ThreadFun()
        {
            while (true)
            {
                if (listener.Pending())
                {
                    var client = listener.AcceptTcpClient();
                    var task = Task.Run(() =>
                    {
                        var nstream = client.GetStream();
                        byte[] barray = Encoding.ASCII.GetBytes("Some Prediction");
                        nstream.Write(barray, 0, barray.Length);
                        nstream.Close();
                    });
                    client.Close();
                }
            }
        }
    }
}
