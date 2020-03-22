using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST4_FOR_GIT
{
    class DataBaseTable : DataBase
    {
        public string TableName { get; set; }

        public DataBaseTable(string Url) 
        {
            TableName = Url.Substring(11);
        }


        

        public void CreateDBTable()
        {   
            string createQuery = $@"CREATE TABLE IF NOT EXISTS
                                [{TableName}] (
                                [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                [1] NVARCHAR (MAX) NULL)";



            using (var conn = new SQLiteConnection("data source = sample.db3"))
            {
                //create a database command 
                using (var cmd = new SQLiteCommand(conn))
                {
                    conn.Open();

                    cmd.CommandText = createQuery;
                    cmd.ExecuteNonQuery();

                    // Close the connection to the database
                    conn.Close();
                }
            }

        }

        public Dictionary<string,List<Article>> ReadDBTable() 
        {
            var DictionaryDB = new Dictionary<string, List<Article>>();
            
            //NOTE: here I have to use "using" operator
            cmd.CommandText = $"Select * FROM {TableName}";
            using (var rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    if (rdr["1"] != DBNull.Value)
                    {
                         DictionaryDB = (rdr["1"]);
                    }
                }
            }
            return DictionaryDB;
        }


        public Dictionary<string, List<Article>> GetNewsDifference()
        {
            var DictionaryDifference = new Dictionary<string, List<Article>>();
            //NOTE: here I want to compare just List<Article> of new and previous dictionaries 
            //while the keyValues are equal
            // I think to use two times "if
            foreach (KeyValuePair<string, List<Article>> chapterArticlesPair in news)
            {
                Console.WriteLine(chapterArticlesPair.Key);

                foreach (Article article in chapterArticlesPair.Value)
                {
                    //var listResult = listSite.Except(listDB).ToList();    
                }
            }


            return DictionaryDifference;
            
        }

        //NOTE: here I will replace "void" with Dictionary<string,List<Artilce>>
        public void SaveNewsdifference()
        {
            // Insert entries in database table
            cmd.CommandText = $"INSERT INTO {TableName}('1') VALUES('{listResult[j]}')";
            cmd.ExecuteNonQuery();
            listResult.Clear();
        }
    
    
    
    
    }
}