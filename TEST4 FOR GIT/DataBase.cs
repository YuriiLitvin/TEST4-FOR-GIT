using System.Data.SQLite;
using System.IO;

namespace TEST4_FOR_GIT
{
    class DataBase
    {
        public void CopyFromTest3()
        {
            string path = @"sample.db3";

            bool fileExist = File.Exists(path);
            if (!fileExist)
            {
                SQLiteConnection.CreateFile("sample.db3");
            }


            using (var conn = new SQLiteConnection("data source = sample.db3"))
            {
                using (var cmd = new SQLiteCommand(conn))
                {

                    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS [NEWS] 
                                ([ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                [Data] TEXT(MAX) NOT NULL)";

                    cmd.ExecuteNonQuery();

                    cmd.CommandText = $"INSERT INTO NEWS (Data) VALUES('')";
                    cmd.ExecuteNonQuery();


                    //cmd.CommandText = $"Select * FROM {tableName}";
                    //using (var rdr = cmd.ExecuteReader())
                    //{
                    //    while (rdr.Read())
                    //    {
                    //        Console.WriteLine(rdr[$"{i}"]);
                    //    }
                    //}
                    // Close the connection to the database
                    
                }
            }
        }
        // TODO: use connection that will be already set up and passed as a constructor parameter
            

        //                // TODO: use deserialization here to get the dictionary from string 
        //                //which were stored in DB
        //                // e.g. JSON one via Newtonsoft.JSON library
        //                // tutorial: https://www.newtonsoft.com/json/help/html/DeserializeDictionary.htm



        //public void SaveNews()
        //    // TODO: use Serialization here to store the dictionary in DB
        //    // e.g. JSON one via Newtonsoft.JSON library
        //    // tutorial: https://www.newtonsoft.com/json/help/html/SerializeDictionary.htm

    }
}
