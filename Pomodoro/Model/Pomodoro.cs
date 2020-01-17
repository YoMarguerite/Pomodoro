using SQLite;
using System;

namespace PomodoroProjet.Model
{
    public class Pomodoro
    {
        private int id;

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }


        private DateTime date;

        public DateTime Date { get; set; }


        private string libelle;

        [NotNull]
        public string Libelle { get; set; }
    }
}
