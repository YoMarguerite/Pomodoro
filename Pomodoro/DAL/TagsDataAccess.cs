using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PomodoroProjet.Model;
using SQLite;

namespace PomodoroProjet.DAL
{
    public class TagsDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();

        public ObservableCollection<Tag> Tags { get; set; }

        public TagsDataAccess()
        {
            database = DatabaseConnection.DbConnection();
            database.CreateTable<Tag>();

            this.Tags = new ObservableCollection<Tag>(database.Table<Tag>());

            if (!database.Table<Tag>().Any())
            {
                addnewTag();
            }
        }

        public void addnewTag()
        {
            var i = SaveTag(new Tag
            {
                Libelle = "ok"
            });
            this.Tags.Add(GetTag(i));
        }

        public IEnumerable<Tag> getTags()
        {
            lock (collisionLock)
            {
                var query = from Tag in database.Table<Tag>()
                            select Tag;
                return query.AsEnumerable();
            }
        }

        public Tag GetTag(int id)
        {
            lock (collisionLock)
            {
                return database.Table<Tag>().FirstOrDefault(Tag => Tag.ID == id);
            }
        }

        public int SaveTag(Tag Tag)
        {
            lock (collisionLock)
            {
                if (Tag.ID != 0)
                {
                    database.Update(Tag);
                    return Tag.ID;
                }
                else
                {
                    database.Insert(Tag);
                    return Tag.ID;
                }
            }
        }

        public void SaveAllTags()
        {
            lock (collisionLock)
            {
                foreach (var Tag in this.Tags)
                {
                    if (Tag.ID != 0)
                    {
                        database.Update(Tag);
                    }
                    else
                    {
                        database.Insert(Tag);
                    }
                }
            }
        }

        public int DeleteTag(Tag Tag)
        {
            var id = Tag.ID;
            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<Tag>(id);
                }
            }
            this.Tags.Remove(Tag);
            return id;
        }

        public void DeleteAllTags()
        {
            lock (collisionLock)
            {
                database.DropTable<Tag>();
                database.CreateTable<Tag>();
            }
            this.Tags = null;
            this.Tags = new ObservableCollection<Tag>(database.Table<Tag>());
        }
    }
}