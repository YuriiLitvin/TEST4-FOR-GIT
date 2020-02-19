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
                                [1] NVARCHAR (2048) NULL,
                                [2] NVARCHAR (2048) NULL,
                                [3] NVARCHAR (2048) NULL,
                                [4] NVARCHAR (2048) NULL,
                                [5] NVARCHAR (2048) NULL,
                                [6] NVARCHAR (2048) NULL,
                                [7] NVARCHAR (2048) NULL,
                                [8] NVARCHAR (2048) NULL,
                                [9] NVARCHAR (2048) NULL,
                                [10] NVARCHAR (2048) NULL,
                                [11] NVARCHAR (2048) NULL,
                                [12] NVARCHAR (2048) NULL,
                                [13] NVARCHAR (2048) NULL,
                                [14] NVARCHAR (2048) NULL,
                                [15] NVARCHAR (2048) NULL,
                                [16] NVARCHAR (2048) NULL)";

        const string createQuery1 = @"CREATE TABLE IF NOT EXISTS
                                [UkrOnline] (
                                [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                [1] NVARCHAR (2048) NULL)";



        static void Main(string[] args)
        {
            Parser("http://www.ukr.net", "//article//section", "/*[position()<last()]//a", "UkrNet");
            Parser("http://www.ukr-online.com", "//td[1]/div[1]/div[@class ='lastblock']", "//a", "UkrOnline");

            //Console.ReadKey();
        }

        public static void Parser(string url, string nodeCount, string nodeSelect, string tableName)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            //
            List<string> list1 = new List<string>();

            // determines amount of parse regions/SQLite columns
            int count = doc.DocumentNode.SelectNodes(nodeCount).Count;
            for (int i = 1; i <= count; i++)
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
                fillInTable(list1, i, tableName);
                list1.Clear();

            }
            
            return;
        }

        static void fillInTable(List<string> list1, int i, string tableName)
        {
            string path = @"C:\Users\Юрій\Desktop\myTest\test4\test4\bin\Debug\sample.db3";
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

                    // Insert entries in database table
                    if (i == 1)
                    {
                        for (int j = 0; j < list1.Count; j++)
                        {
                            cmd.CommandText = $"INSERT INTO {tableName}('{i}') VALUES('{list1[j]}')";
                            cmd.ExecuteNonQuery();
                            //cmd.CommandText = "SELECT LAST_INSERT_ROWID()";

                        }
                    }
                    else
                    {
                        for (int j = 0; j < list1.Count; j++)
                        {
                            cmd.CommandText = $"UPDATE {tableName} SET ('{i}') = ('{list1[j]}') WHERE ID = '{j + 1}'";
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Select and display database entries
                    //cmd.CommandText = $"Select * FROM {tableName}";
                    //using (var rdr = cmd.ExecuteReader())
                    //{
                    //    while (rdr.Read())
                    //    {
                    //        Console.WriteLine(rdr[$"{i}"]);
                    //    }
                    //}
                    // Close the connection to the database
                    conn.Close();

                }

            }

            return;

        }







    }
}
