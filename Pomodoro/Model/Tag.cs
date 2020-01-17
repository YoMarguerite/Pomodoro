using SQLite;

namespace PomodoroProjet.Model
{
    public class Tag
    {
        private int id;

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }


        private string libelle;

        [NotNull]
        public string Libelle { get; set; }
    }
}
