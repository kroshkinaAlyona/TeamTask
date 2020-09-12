using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace Lesson1
{
    class Program
    {
        static readonly SqlConnection conn = new SqlConnection
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["LibraryConnection"].ConnectionString
        };

        //static readonly SqlConnection conn1 = new SqlConnection
        //{
        //    ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Integrated Security=SSPI;"
        //};

        void InsertQ()
        {
            try
            {
                conn.Open();

                string a = "'Zelazny');drop table Books";
                string insertString = $"insert into Authors (FirstName, LastName) values ('Roger', {a}";
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = insertString
                };
                cmd.ExecuteNonQuery();


            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        void SelectQ()
        {
            try
            {
                conn.Open();
            
                var cmd = new SqlCommand("select * from Authors;select * from Books", conn);
                var rdr = cmd.ExecuteReader();

                do
                {
                    //  вывести заголовки колонок
                    for (int i = 0; i < rdr.FieldCount; i++)
                        Console.Write(rdr.GetName(i) + "\t");
                    Console.WriteLine();
                    while (rdr.Read())
                    {
                        //  выводим данные, полученные из запроса
                        for (int i = 0; i < rdr.FieldCount; i++)
                            Console.Write(rdr[i] + "\t");
                        Console.WriteLine();

                        //  выводим ииформация по книгам
                        //conn1.Open();
                        //var cmd2 = new SqlCommand
                        //{
                        //    CommandText = $"select Title from Books where AuthorId=@p1",
                        //    Connection = conn1,
                        //};
                        //cmd2.Parameters.Add("@p1", SqlDbType.Int).Value = rdr["Id"];

                        //var readB = cmd2.ExecuteReader();
                        //while (readB.Read())
                        //    Console.WriteLine($"\t - {readB[0]}");
                        //conn1.Close();
                        //readB.Close();
                    }
                    Console.WriteLine();
                } while (rdr.NextResult());
                
                
                rdr.Close();
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
            }
            finally
            {
                conn.Close();
               
            }
        }

        static void Main(string[] args)
        {

            Program program = new Program();
            //program.InsertQ();
            program.SelectQ();

            //conn.Open();

            //var nameFirst = "Имя";
            //var naneLast = "Фамилия";
            //var id = 2;

            //var insertString = $@"insert into Authors (FirstName, LastName) values (N'{nameFirst}', N'{naneLast}')";

            //using (SqlCommand cmd = new SqlCommand(insertString, conn)) 
            //{
            //    cmd.ExecuteNonQuery();
            //}

            /*
            var selectString = @"select * from Authors where Id=@p1";

            int lineCount = 0;
            using (SqlCommand cmd = new SqlCommand(selectString, conn))
            {
                cmd.Parameters.AddWithValue("p1", id);
                using (var dataReader = cmd.ExecuteReader())
                {
                    do
                    {
                        for (int i = 0; i < dataReader.FieldCount; i++)
                            Console.Write(dataReader.GetName(i) + " ");
                        Console.WriteLine();

                        while (dataReader.Read())
                        {
                            for (int i = 0; i < dataReader.FieldCount; i++)
                                Console.Write(dataReader[i] + " ");
                            Console.WriteLine();
                            lineCount++;
                        };
                        Console.WriteLine();
                    }
                    while (dataReader.NextResult());
                };
                Console.WriteLine($"Lines count {lineCount}");
            }
            */

            //using (SqlCommand cmd = new SqlCommand("getBooksNumber", conn))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@AuthorId", SqlDbType.Int).Value = id;

            //    var paramOut = new SqlParameter("@BookCount", SqlDbType.Int)
            //    {
            //        Direction = ParameterDirection.Output
            //    };
            //    cmd.Parameters.Add(paramOut);

            //    cmd.ExecuteNonQuery();
            //    Console.WriteLine($"Count books of autor id = {id} => {cmd.Parameters["@BookCount"].Value}");
            //}

            //if (conn != null)
            //{
            //    conn.Close();
            //}      
        }
    }
}
