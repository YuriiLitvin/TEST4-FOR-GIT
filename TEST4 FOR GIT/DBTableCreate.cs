using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System.Data;

namespace TEST4_FOR_GIT
{
    class DBTableCreate
    {
        // 1. solve with TableName
        // 2. solve with createQuery
        // 3. solve with "i" -- column name

        readonly string createQuery = @"CREATE TABLE IF NOT EXISTS
                                [UkrNet] (
                                [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                [1] NVARCHAR (2048) NULL)";

        readonly string createQuery1 = @"CREATE TABLE IF NOT EXISTS
                                [UkrOnline] (
                                [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                [1] NVARCHAR (2048) NULL)";

        public List<string> TableUpdate() 
        {
            List<string> listDB = new List<string>();
            
            // connect to database. try/catch
            using (var conn = new SQLiteConnection("data source = sample.db3"))
            {
                //create a database command 
                using (var cmd = new SQLiteCommand(conn))
                {
                    conn.Open();

                    //swith between tables
                    if (tableName == "UkrNet")
                    {
                        cmd.CommandText = createQuery;
                    }
                    else
                    {
                        cmd.CommandText = createQuery1;
                    }
                    cmd.ExecuteNonQuery();

                    // Get the schema for the columns in the database.
                    DataTable ColsTable = conn.GetSchema("Columns");

                    // Query the columns schema using SQL statements to work out if the required columns exist.
                    bool ColumnExists = ColsTable.Select($"COLUMN_NAME='{i}' AND TABLE_NAME='{tableName}'").Length != 0;

                    if (!ColumnExists)
                    {
                        cmd.CommandText = $"ALTER TABLE {tableName} ADD COLUMN '{i}' NVARCHAR(2048) NULL";
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        // reading the table column and compare with listSite
                        // than resultList insert to the table
                        cmd.CommandText = $"Select * FROM {tableName}";
                        using (var rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                if (rdr[$"{i}"] != DBNull.Value)
                                {
                                    string readerLine = (rdr[$"{i}"]).ToString();
                                    listDB.Add(readerLine);
                                }

                            }

                        }
                    }
                    var listResult = listSite.Except(listDB).ToList();

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
                    // Close the connection to the database
                    conn.Close();

                }
            }


            return listResult;
        }

    }
}
