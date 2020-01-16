using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Pomodoro.DAL;
using Pomodoro.Model;

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

            try
            {
                TagsDataAccess tagData = new TagsDataAccess();
                lbx_pomodoro_bdd.ItemsSource = tagData.getTags();
            }
            catch
            {
                //MessageBox.Show("Récupération des tags impossible.");
            }
        }
      
        //Ajout d'une nouvelle tache dans la list du jour
        public async void AddItem(object sender, RoutedEventArgs e)
        {
            if(tbx_libellePomodoro.Text != "")
            {
                

                try
                {
                    TagsDataAccess tagData = new TagsDataAccess();
                    tagData.SaveTag(new Tag
                    {
                        Libelle = tbx_libellePomodoro.Text
                    });
                    lbx_pomodoro.Items.Add(tbx_libellePomodoro.Text);
                }
                catch
                {
                    MessageBox.Show("Sauvegarde du Tag " + tbx_libellePomodoro.Text + " impossible.");
                }

                tbx_libellePomodoro.Text = "";
            }
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

    }
}
