using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PomodoroProjet.Model;
using SQLite;

namespace PomodoroProjet.DAL
{
    public class PomodorosDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();

        public ObservableCollection<Pomodoro> Pomodoros { get; set; }

        public PomodorosDataAccess()
        {
            database = DatabaseConnection.DbConnection();
            database.CreateTable<Pomodoro>();

            this.Pomodoros = new ObservableCollection<Pomodoro>(database.Table<Pomodoro>());
        }

        public IEnumerable<Pomodoro> getPomodoros()
        {
            lock (collisionLock)
            {
                var query = from Pomodoro in database.Table<Pomodoro>()
                            select Pomodoro;
                return query.AsEnumerable();
            }
        }

        public Pomodoro GetPomodoro(int id)
        {
            lock (collisionLock)
            {
                return database.Table<Pomodoro>().FirstOrDefault(Pomodoro => Pomodoro.ID == id);
            }
        }

        public int SavePomodoro(Pomodoro Pomodoro)
        {
            lock (collisionLock)
            {
                if (Pomodoro.ID != 0)
                {
                    database.Update(Pomodoro);
                    return Pomodoro.ID;
                }
                else
                {
                    database.Insert(Pomodoro);
                    return Pomodoro.ID;
                }
            }
        }

        public void SaveAllPomodoros()
        {
            lock (collisionLock)
            {
                foreach (var pomodoro in this.Pomodoros)
                {
                    if (pomodoro.ID != 0)
                    {
                        database.Update(pomodoro);
                    }
                    else
                    {
                        database.Insert(pomodoro);
                    }
                }
            }
        }

        public int DeletePomodoro(Pomodoro Pomodoro)
        {
            var id = Pomodoro.ID;
            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<Pomodoro>(id);
                }
            }
            this.Pomodoros.Remove(Pomodoro);
            return id;
        }

        public void DeleteAllPomodoros()
        {
            lock (collisionLock)
            {
                database.DropTable<Pomodoro>();
                database.CreateTable<Pomodoro>();
            }
            this.Pomodoros = null;
            this.Pomodoros = new ObservableCollection<Pomodoro>(database.Table<Pomodoro>());
        }
    }
}