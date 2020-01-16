using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro
{
    /// <summary>
    /// Logique d'interaction pour Accueil.xaml
    /// Permet d'avoir une liste des taches du jour
    /// </summary>
    public partial class Accueil : UserControl
    {
        
        public Accueil()
        {
            InitializeComponent();

        }
      
        //Ajout d'une nouvelle tache dans la list du jour
        public void AddItem(object sender, RoutedEventArgs e)
        {
            if(tbx_libellePomodoro.Text != "")
            {
                lbx_pomodoro.Items.Add(tbx_libellePomodoro.Text);
                tbx_libellePomodoro.Text = "";
            }
            lbx_pomodoro_bdd.Items.Add("test");
        }

        //Suppression d'un element dans la liste des taches du jour
        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if(lbx_pomodoro.Items.Count > 0)
            {
                lbx_pomodoro.Items.RemoveAt(lbx_pomodoro.Items.IndexOf(lbx_pomodoro.SelectedItem));
            }
            
        }

        //Ajout de tache deja existante dans la list du jour
        public void AddItemBdd(object sender, RoutedEventArgs e)
        {
            lbx_pomodoro.Items.Add(lbx_pomodoro_bdd.SelectedItem);
        }

        public async void load_Data()

        {

            App_dbContext db = new App_dbContext();

            if (db.Database.Connection.State == System.Data.ConnectionState.Closed || db.Database.Connection.State == System.Data.ConnectionState.Broken) await db.Database.Connection.OpenAsync();



            try

            {

                var data = db.tags.FirstOrDefault(); //id == 1

                if (data == null)
                {
                    //--< Add() >--

                    data = new tags();
                    data.libelle = "test2";
                    db.tags.Add(data);

                    //--</ Add() >--

                }
                await db.SaveChangesAsync();
            }

            catch (Exception ex){ }
        }
    }
}
