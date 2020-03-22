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
        public string SiteName { get; set; }
        
        const string createQuery = $@"CREATE TABLE IF NOT EXISTS
                                [{SiteName}] (
                                [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                [1] NVARCHAR (MAX) NULL)";

        

        public void CreateDataBaseTable()
        {
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
        //// Get the schema for the columns in the database.
        //DataTable ColsTable = conn.GetSchema("Columns");

        //// Query the columns schema using SQL statements to work out if the required columns exist.
        //bool ColumnExists = ColsTable.Select($"COLUMN_NAME='{i}' AND TABLE_NAME='{tableName}'").Length != 0;

        //if (!ColumnExists)
        //{
        //    cmd.CommandText = $"ALTER TABLE {tableName} ADD COLUMN '{i}' NVARCHAR(2048) NULL";
        //    cmd.ExecuteNonQuery();
        //}
        //else
        //{
        // reading the table column and compare with listSite
        // than resultList insert to the table


        public Dictionary<string,List<Article>> ReadDataBaseTable() 
        {
            var ParserDB = new Dictionary<string, List<Article>>();
            cmd.CommandText = $"Select * FROM {SiteName}";
            using (var rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    if (rdr["1"] != DBNull.Value)
                    {
                                   
                        //string readerLine = (rdr[$"{i}"]).ToString();
                        //listDB.Add(readerLine);
                    }

                }

            }
            return ParserDB;
        }


        public void GetNewsDifference()
        {
            var listResult = listSite.Except(listDB).ToList();
        }


        public void SaveNewsdifference()
        {
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
        }
    
    
    
    
    }
}