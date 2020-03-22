using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;

namespace TEST4_FOR_GIT
{
    class DataBase
    {
        // TODO: rename to reference logic -- CreateDatabaseIfNotExists
        public void CreateDataBaseFile()
        {
            // TODO: rename from "sample"
            // TODO: use just "sample.db3", program is in Debug folder already
            string path = @"C:\Users\Юрій\Desktop\for check
                            \TEST4 FOR GIT\TEST4 FOR GIT\bin\Debug\sample.db3";
            bool fileExist = File.Exists(path);
            if (!fileExist)
            {
                SQLiteConnection.CreateFile("sample.db3");
            }
        }

// TODO: fix bracket :)
}
}
