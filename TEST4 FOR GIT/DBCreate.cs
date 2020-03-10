using System;
using System.IO;
using System.Data.SQLite;

namespace TEST4_FOR_GIT
{
    class DBCreate
    {
        private string Path { get; set; } = @"C:\Users\Юрій\Desktop\for check
                            \TEST4 FOR GIT\TEST4 FOR GIT\bin\Debug\sample.db3";

        public void DataBaseCreate()
        {

            bool fileExist = File.Exists(Path);
            if (!fileExist)
            {
                SQLiteConnection.CreateFile("sample.db3");
            }
        }
    }
}
