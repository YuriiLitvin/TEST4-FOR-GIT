using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST4_FOR_GIT
{
    // TODO: rename: a table for news
    // TODO: remove nesting, table is A PART OF database, not IS database
    class DataBaseTable : DataBase
    {
        public string TableName { get; set; }

        // TODO: pass connection to the DB as a parameter
        public DataBaseTable(string Url) 
        {
            // TODO: remove substring, pass the readable and sensable name
            TableName = Url.Substring(11);
        }


        
        // rename: If the class is already "...Table", don`t duplicate it with ...Table(), just Create() will be enought
        public void CreateDBTable()
        {   
            // TODO: make nvarchar(max) not null
            // TODO: rename column to at least "Data"
            string createQuery = $@"CREATE TABLE IF NOT EXISTS
                                [{TableName}] (
                                [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                [1] NVARCHAR (MAX) NULL)";
            


            // TODO: use connection that will be already set up and passed as a constructor parameter
            using (var conn = new SQLiteConnection("data source = sample.db3"))
            {
                //create a database command 
                using (var cmd = new SQLiteCommand(conn))
                {
                    conn.Open();

                    cmd.CommandText = createQuery;
                    cmd.ExecuteNonQuery();

                    // TODO: remove Close(), you either use using or conn.Close, not both
                    // Close the connection to the database
                    conn.Close();
                }
            }

        }

        // TODO: rename: its not just table, its news, and you dont read them, you get them
        public Dictionary<string,List<Article>> ReadDBTable() 
        {
            var DictionaryDB = new Dictionary<string, List<Article>>();

            //NOTE: here I have to use "using" operator
            var cmd = new SQLiteCommand(conn);
            cmd.CommandText = $"Select * FROM {TableName}";
            using (var rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    // TODO: remove null check, dont store null
                    if (rdr["1"] != DBNull.Value)
                    {
                        // TODO: use deserialization here to get the dictionary from string which were stored in DB
                        // e.g. JSON one via Newtonsoft.JSON library
                        // tutorial: https://www.newtonsoft.com/json/help/html/DeserializeDictionary.htm
                        DictionaryDB = (rdr["1"]);
                    }
                }
            }
            return DictionaryDB;
        }

        // TODO: replace from here.
        // news difference is a difference between 2 dicts, it shouldn't be here 
        // as it doesn't related to the database or tables
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
        // TODO: Save shouldnt retun anything, Dictionary<string,List<Artilce>> it a parameter -- the difference to save
        // TODO: rename method (remove "difference"), database table shouldn't be specific to the application logic
        
        // for database and for table its just some info for some columns
        public void SaveNewsdifference()
        {
            // TODO: use Serialization here to store the dictionary in DB
            // e.g. JSON one via Newtonsoft.JSON library
            // tutorial: https://www.newtonsoft.com/json/help/html/SerializeDictionary.htm
            
            // Insert entries in database table
            cmd.CommandText = $"INSERT INTO {TableName}('1') VALUES('{listResult[j]}')";
            cmd.ExecuteNonQuery();
            listResult.Clear();
        }
    
    
    
    
    }
}