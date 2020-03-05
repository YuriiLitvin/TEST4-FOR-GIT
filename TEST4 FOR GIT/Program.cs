using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Data.SQLite;
using System.IO;
using System.Data;

namespace TEST4_FOR_GIT
{
    class Program
    {
        const string createQuery = @"CREATE TABLE IF NOT EXISTS
                                [UkrNet] (
                                [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                [1] NVARCHAR (2048) NULL
                                )";

        const string createQuery1 = @"CREATE TABLE IF NOT EXISTS
                                [UkrOnline] (
                                [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                [1] NVARCHAR (2048) NULL)";




        static void Main(string[] args)
        {
            Parser("http://www.ukr.net", "//article//section", "/*[position()<last()]//a", "UkrNet");
            //Parser("http://www.ukr-online.com", "//td[1]/div[1]/div[@class ='lastblock']", "//a", "UkrOnline");

            //Console.ReadKey();
        }

        public static void Parser(string url, string nodeCount, string nodeSelect, string tableName)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            //
            List<string> list1 = new List<string>();

            // determines amount of parse regions/SQLite columns

            //int count = doc.DocumentNode.SelectNodes(nodeCount).Count;!!!!!!
            for (int i = 1; i <= 1; i++) //count!!!!!!
            {
                //indicator for each parsed region
                Console.WriteLine($"[{i}]");

                //determines amount of urls in one parse region/SQLite rows
                var htmlNodes = doc.DocumentNode.SelectNodes($"{nodeCount}[{i}]{nodeSelect}");

                //int j = 1;
                foreach (var node in htmlNodes)
                {
                    string nodeValue = node.Attributes["href"].Value;
                    list1.Add(nodeValue);
                }
                FillInTable(list1, i, tableName);
                list1.Clear();
            }
        }

        static void FillInTable(List<string> list1, int i, string tableName)
        {
            List<string> list = new List<string>();
            string path = @"C:\Users\Юрій\Desktop\for check\TEST4 FOR GIT\TEST4 FOR GIT\bin\Debug\sample.db3";
            bool fileExist = File.Exists(path);
            if (!fileExist)
            {
                SQLiteConnection.CreateFile("sample.db3");
            }
            // connect to database
            using (var conn = new SQLiteConnection("data source = sample.db3"))
            {
                //create a database command 
                using (var cmd = new SQLiteCommand(conn))
                {
                    conn.Open();

                    //swith between tables
                    if (tableName == "UkrNet")
                    {
                        cmd.CommandText = createQuery;
                    }
                    else
                    {
                        cmd.CommandText = createQuery1;
                    }
                    cmd.ExecuteNonQuery();

                    // Get the schema for the columns in the database.
                    DataTable ColsTable = conn.GetSchema("Columns");

                    // Query the columns schema using SQL statements to work out if the required columns exist.
                    bool ColumnExists = ColsTable.Select($"COLUMN_NAME='{i}' AND TABLE_NAME='{tableName}'").Length != 0;

                    if (!ColumnExists)
                    {
                        cmd.CommandText = $"ALTER TABLE {tableName} ADD COLUMN '{i}' NVARCHAR(2048) NULL";
                        cmd.ExecuteNonQuery();
                    }
                    // insert here reading the table column and compare with "list1"
                    // than resultList insert to the table


                    // Insert entries in database table
                    cmd.CommandText = $"SELECT COUNT(1) FROM {tableName}";
                    var rowsCount = Convert.ToInt32(cmd.ExecuteScalar());
                    int rows = rowsCount - list1.Count;


                    if (i == 1)
                    {
                        for (int j = 0; j < list1.Count; j++)
                        {
                            cmd.CommandText = $"INSERT INTO {tableName}('{i}') VALUES('{list1[j]}')";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        for (int j = 0; j < list1.Count; j++)
                        {
                            cmd.CommandText = $"UPDATE {tableName} SET ('{i}') = ('{list1[j]}') WHERE ID = '{rows + j + 1}'";
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Select and display database entries

                    cmd.CommandText = $"Select * FROM {tableName}";


                    using (var rdr = cmd.ExecuteReader())
                    {

                        while (rdr.Read())
                        {
                            string readerLine = rdr.GetString(i);
                            list.Add(readerLine);
                        }
                        //Console.WriteLine(list.Count);
                        //Console.ReadLine();
                    }
                    // Close the connection to the database
                    conn.Close();
                }
            }

            var listResult = list1.Except(list).ToList();
            if (listResult != null)
            {
                Console.WriteLine("Here we have new lines!!!");
                Console.WriteLine(listResult.Count);
                Console.ReadLine();
            }
            else

            {
                Console.WriteLine("There are no new lines");
                Console.ReadLine();
            }
        }







    }
}
