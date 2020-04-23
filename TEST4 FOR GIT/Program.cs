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
using Newtonsoft.Json;

namespace TEST4_FOR_GIT
{
    class Program
    {
        static void Main(string[] args)
        {

            CallParser();
        }
        
        // TODO: return news dictionary instead of void to use it in future
        // TODO: then rename "Call parser" to something easily understandable
        static void CallParser()
        {
            #region SetForParsers
            //Parser UkrNetParser = new Parser("http://www.ukr.net",
            //"//article//section",
            //".//h2",
            //".//*[position()>1 and position()<last()]//a");


            //Parser UkrOnline = new Parser("http://www.ukr-online.com",
            //    "//td[1]/div[1]/div[@class ='lastblock']",
            //    ".//*[@class = 'lastheader']",
            //    ".//div[@class = 'custom-4']//a");
            #endregion

            ParserKeys keys = new ParserKeys();
            foreach (var key in keys.GetList()) 
            {
                var news = new Parser(key);
            }
            
            
            
            //DataBase db = new DataBase();
            //db.CreateDatabaseIfNotExists();
            //var connection = db.CreateConnection();
            //var table = new DataBaseTable("X",connection);
            //table.Create();



            using (var fs = new FileStream("test.txt", FileMode.Append))
            {
                using (var sw = new StreamWriter(fs))
                {
                    Dictionary<string, List<Article>> news = UkrOnline.GetNews();

                    foreach (KeyValuePair<string, List<Article>> chapterArticlesPair in news)
                    {
                        sw.WriteLine(chapterArticlesPair.Key);

                        foreach (Article article in chapterArticlesPair.Value)
                        {
                            sw.WriteLine(article.GetArticleTextWithUrl());
                        }
                    }
                }
            }

            //string json = JsonConvert.SerializeObject(news, Formatting.Indented);
            //Console.WriteLine(json);
            Console.WriteLine("finished");
            Console.ReadLine();


        }

        //static async void TelegramBot(string news_data)
        //{
        //    Console.WriteLine("Hello, World! I am user  and my name is Yurii))).");
        //    await botClient.SendTextMessageAsync("@botbroadcasting", text: news_data);
        //}









    }
}
