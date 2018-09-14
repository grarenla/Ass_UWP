using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using App2.Entity;
using SQLite.Net;

namespace App2.Model
{
    class UserModel
    {

//        public static string DB_PATH = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, "User.sqlite"));

        public static void AddUser(string DB_PATH, User user)
        {
            using (SQLiteConnection db = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DB_PATH))
            {
                db.Insert(user);
            }
        }

        public static ObservableCollection<User> GetAllUsers(string DB_PATH)
        {
            using (SQLiteConnection db = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DB_PATH))
            {
                return new ObservableCollection<User>(db.Table<User>());
            }
        }

        public static ObservableCollection<User> SearchUsers(string DB_PATH, string key, string field)
        {
            using (SQLiteConnection db = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DB_PATH))
            {
                switch (field)
                {
                    case "Name":
                        return new ObservableCollection<User>(db.Table<User>().Where(user => user.FullName.Contains(key)));
                    case "Email":
                        return new ObservableCollection<User>(db.Table<User>().Where(user => user.Email.Contains(key)));
                    case "Phone":
                        return new ObservableCollection<User>(db.Table<User>().Where(user => user.Phone.Contains(key)));
                    default: return new ObservableCollection<User>();
                }
                
            }
        }
    }
}
