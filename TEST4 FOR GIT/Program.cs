using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Data.SQLite;
using System.IO;
using System.Data;
using System.Timers;
using Telegram.Bot;


namespace TEST4_FOR_GIT
{
    class Program
    {
        static ITelegramBotClient botClient;


        const string createQuery = @"CREATE TABLE IF NOT EXISTS
                                [UkrNet] (
                                [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                [1] NVARCHAR (2048) NULL)";

        const string createQuery1 = @"CREATE TABLE IF NOT EXISTS
                                [UkrOnline] (
                                [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                [1] NVARCHAR (2048) NULL)";




        static void Main(string[] args)

        {
            //botClient = new TelegramBotClient("1028340877:AAGZMZOwKrdZD5-yrONAlgv4Tmlytk6ShiA");

            //Timer aTimer = new Timer();
            //aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            //aTimer.Interval = 30000;
            //aTimer.Enabled = true;
            //Console.WriteLine("Press \'q\' to quit the sample.");
            //while (Console.Read() != 'q') ;
            // TODO: ensure you fixed all other todos before starting this
            // TODO: here you can create List<Dictionary<string, List<Arcticle>>> to use it like a database (for not bothering yourself with real db yet)
            // TODO: then you can "call the parser" 2 times in a row, to have 2 items in a list.
            // TODO: then you can start to compare them and store the diff in the same type dictionary, but with only different values
            // TODO: then you can run it with timer and compare last item in list with previous, using the diff you set up above
            // TODO: then you can prettify your code - rename some variables and extract couple of methods :)
            CallParser();
        }

        //private static void OnTimedEvent(object source, ElapsedEventArgs e)
        //{
        //    CallParser();
        //    // post to telegram
        //    var news_data = "post this news data to channel";
        //    // TODO: use .Wait() or .Result applying to async function to wait for it to be executed.
        //    TelegramBot(news_data);
        //    // for async: if ther is no any other methods need to set some time to sleep
        //    // in our case we exiting timed event, so async method runs correctly
        //    //Thread.Sleep(int.MaxValue);

        //}

        // TODO: return news dictionary instead of void to use it in future
        // TODO: then rename "Call parser" to something easily understandable
        static void CallParser()
        {
            Parser UkrNetParser = new Parser("http://www.ukr.net", "//article//section", "*[position()>1 and position()<last()]//a");
            //Parser UkrOnline = new Parser("http://www.ukr-online.com", "//td[1]/div[1]/div[@class ='lastblock']", "a");
            
            foreach (var chapter in UkrNetParser.GetNews())
            {
                Console.WriteLine(chapter.Key);

                foreach (var article in chapter.Value)
                {
                    // DONE: use already created item.GetArticleTextWithUrl() or create something similar
                    Console.WriteLine(article);
                }
            }
            Console.ReadLine();

            
        }



        public static void Parser(string url, string nodeCount, string nodeSelect, string tableName)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            //
            List<string> listSite = new List<string>();

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
                    listSite.Add(nodeValue);
                }
                FillInTable(listSite, i, tableName);
                listSite.Clear();
            }
        }

        static void FillInTable(List<string> listSite, int i, string tableName)
        {
            List<string> listDB = new List<string>();
            string path = @"C:\Users\Юрій\Desktop\for check
                            \TEST4 FOR GIT\TEST4 FOR GIT\bin\Debug\sample.db3";
            bool fileExist = File.Exists(path);
            if (!fileExist)
            {
                SQLiteConnection.CreateFile("sample.db3");
            }
            // connect to database. try/catch
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
                    else
                    {
                        // reading the table column and compare with listSite
                        // than resultList insert to the table
                        cmd.CommandText = $"Select * FROM {tableName}";
                        using (var rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                if (rdr[$"{i}"] != DBNull.Value)
                                {
                                    string readerLine = (rdr[$"{i}"]).ToString();
                                    listDB.Add(readerLine);
                                }
                               
                            }
                            
                        }
                    }
                    var listResult = listSite.Except(listDB).ToList();

                    // Insert entries in database table
                    cmd.CommandText = $"SELECT COUNT(1) FROM {tableName}";
                    var rowsCount = Convert.ToInt32(cmd.ExecuteScalar());
                    int rows = rowsCount - listResult.Count;


                    if (i == 1)
                    {
                        for (int j = 0; j < listResult.Count; j++)
                        {
                            cmd.CommandText = $"INSERT INTO {tableName}('{i}') VALUES('{listResult[j]}')";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        for (int j = 0; j < listResult.Count; j++)
                        {
                            cmd.CommandText = $"UPDATE {tableName} SET ('{i}') = ('{listResult[j]}') WHERE ID = '{rows + j + 1}'";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    listResult.Clear();
                    // Close the connection to the database
                    conn.Close();
                }
            }
        }
        static async void TelegramBot(string news_data)
        {
            Console.WriteLine("Hello, World! I am user  and my name is Yurii))).");
            await botClient.SendTextMessageAsync("@botbroadcasting", text: news_data);
        }









    }
}
