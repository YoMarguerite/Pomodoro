using System.IO;
using SQLite;

namespace Pomodoro.DAL
{
    public class DatabaseConnection //: IDatabaseConnection
    {
        public static SQLiteConnection DbConnection()
        {
            var dbname = "pomodoro.db";
            var path = Path.Combine(System.Environment.
                GetFolderPath(System.Environment.
                SpecialFolder.Personal), dbname);
            return new SQLiteConnection(path);
        }
    }
}