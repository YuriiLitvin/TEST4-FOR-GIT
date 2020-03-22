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


        
        static Program()
        {
            Console.OutputEncoding = Encoding.UTF8;
        }

        static void Main(string[] args)

        {
            //botClient = new TelegramBotClient("1028340877:AAGZMZOwKrdZD5-yrONAlgv4Tmlytk6ShiA");

            //Timer aTimer = new Timer();
            //aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            //aTimer.Interval = 30000;
            //aTimer.Enabled = true;
            //Console.WriteLine("Press \'q\' to quit the sample.");
            //while (Console.Read() != 'q') ;
            // DONE: ensure you fixed all other todos before starting this
            // TODO: here you can create List<Dictionary<string, List<Arcticle>>> to use it like a database
            //(for not bothering yourself with real db yet)
            // TODO: then you can "call the parser" 2 times in a row, to have 2 items in a list.
            // TODO: then you can start to compare them and store the diff in the same type dictionary,
            //but with only different values
            // TODO: then you can run it with timer and compare last item in list with previous, 
            //using the diff you set up above
            // TODO: then you can prettify your code - rename some variables and 
            //extract couple of methods :)
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
            Parser UkrNetParser = new Parser("http://www.ukr.net", "//article//section", "*[position()<last()]//a");
            //Parser UkrOnline = new Parser("http://www.ukr-online.com", "//td[1]/div[1]/div[@class ='lastblock']", "a");

            Dictionary<string,List<Article>> news = UkrNetParser.GetNews();

            foreach (KeyValuePair<string, List<Article>> chapterArticlesPair in news)
            {
                Console.WriteLine(chapterArticlesPair.Key);

                foreach (Article article in chapterArticlesPair.Value)
                {
                    Console.WriteLine(article.GetArticleTextWithUrl());
                }
            }
            Console.ReadLine();


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
            // connect to database
            
        }
        static async void TelegramBot(string news_data)
        {
            Console.WriteLine("Hello, World! I am user  and my name is Yurii))).");
            await botClient.SendTextMessageAsync("@botbroadcasting", text: news_data);
        }









    }
}
