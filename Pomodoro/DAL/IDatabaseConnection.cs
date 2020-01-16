namespace Pomodoro.DAL
{
    interface IDatabaseConnection
    {
        SQLite.SQLiteConnection DbConnection();
    }
}
