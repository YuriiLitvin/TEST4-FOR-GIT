using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace TEST4_FOR_GIT
{
    // TODO: rename: a table for news
    class DataBaseTable 
    {
        public string TableName { get; set; }

        public DataBaseTable(string Url, SQLiteConnection connection)
        {
            TableName = Url;
        }

        public void Create()
        {   
            string createQuery = $@"CREATE TABLE IF NOT EXISTS
                                [{TableName}] (
                                [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                [Data] TEXT(MAX) NOT NULL)";
        
        }

        public Dictionary<string,List<Article>> GetPreviousNews() 
        {
            var DictionaryDB = new Dictionary<string, List<Article>>();

            var cmd = new SQLiteCommand(connection);
            cmd.CommandText = $"Select * FROM {TableName}";
            using (var rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    
                        // TODO: use deserialization here to get the dictionary from string 
                        //which were stored in DB
                        // e.g. JSON one via Newtonsoft.JSON library
                        // tutorial: https://www.newtonsoft.com/json/help/html/DeserializeDictionary.htm
                    
                }
            }
            return DictionaryDB;
        }

        

        public void SaveNews()
        {
            // TODO: use Serialization here to store the dictionary in DB
            // e.g. JSON one via Newtonsoft.JSON library
            // tutorial: https://www.newtonsoft.com/json/help/html/SerializeDictionary.htm
            
            cmd.CommandText = $"INSERT INTO {TableName}('1') VALUES('{listResult[j]}')";
            cmd.ExecuteNonQuery();
        }
    
    
    
    
    }
}