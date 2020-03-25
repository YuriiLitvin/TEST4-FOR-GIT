using System.Data.SQLite;
using System.IO;

namespace TEST4_FOR_GIT
{
    class DataBase
    {
        public void CreateDatabaseIfNotExists()
        {
            string path = @"sample.db3";
            bool fileExist = File.Exists(path);
            if (!fileExist)
            {
                SQLiteConnection.CreateFile("sample.db3");
            }
        }

        public SQLiteConnection CreateConnection() 
        {
            // TODO: use connection that will be already set up and passed as a constructor parameter
            
            var result = new SQLiteConnection("data source = sample.db3");
            result.Open();
            return result;
        }
    }
    
    
    
}
