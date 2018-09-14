using SQLite.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using App2.Entity;

namespace App2.Model
{
    class DataAccess
    {

        

        public static void CreateDatabase(string DB_PATH)
        {
            using (var db = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DB_PATH))
            {
                db.CreateTable<User>();
            }
        }
    }
}