using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;  
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Data.SQLite;
using System.IO;

namespace TEST4_FOR_GIT
{
    class Program
    {
        const string createQuery = @"CREATE TABLE IF NOT EXISTS
                                [UkrNet] (
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
                                [1] NVARCHAR (2048) NULL,
                                [2] NVARCHAR (2048) NULL,
                                [3] NVARCHAR (2048) NULL,
                                [4] NVARCHAR (2048) NULL,
                                [5] NVARCHAR (2048) NULL,
                                [6] NVARCHAR (2048) NULL,
                                [7] NVARCHAR (2048) NULL)";



        static void Main(string[] args)
        {
            //Parser("http://www.ukr.net", "//article//section", "/*[position()<last()]//a", "UkrNet");
            Parser("http://www.ukr-online.com", "//td[1]/div[1]/div[@class ='lastblock']", "//a", "UkrOnline");


        }

        public static void Parser(string url, string nodeCount, string nodeSelect, string tableName)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            //
            // determines amount of parse regions/SQLite columns
            int count = doc.DocumentNode.SelectNodes(nodeCount).Count;
            for (int i = 1; i <= count; i++)
            {
                //indicator for each parse region
                Console.WriteLine($"[{i}]");

                //determines amount of urls in one parse region/SQLite rows
                var htmlNodes = doc.DocumentNode.SelectNodes($"{nodeCount}[{i}]{nodeSelect}");
                //int htmlNodeCount = htmlNodes.Count;

                foreach (var node in htmlNodes)
                {
                    string nodeValue = node.Attributes["href"].Value;
                    string result = node.Attributes["href"].XPath;
                    char res = result.Substring(result.Length - 16).First();
                    fillInTable(nodeValue, i, tableName, res);

                }
            }
            Console.ReadKey();
            return;
        }

        static void fillInTable(string nodeValue, int i, string tableName, char res)
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


                    // Insert entries in database table
                    if (i == 1)
                    {
                        cmd.CommandText = $"INSERT INTO {tableName}('{i}') VALUES('{nodeValue}')";
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd.CommandText = $"UPDATE {tableName} SET ('{i}') = ('{nodeValue}') WHERE ID = '{res}'";
                        cmd.ExecuteNonQuery();

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
