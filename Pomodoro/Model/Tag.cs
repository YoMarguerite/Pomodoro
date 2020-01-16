using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Model
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
