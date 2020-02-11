using System;
using System.Windows;
using System.Windows.Controls;
using PomodoroProjet.DAL;
using PomodoroProjet.Model;

namespace PomodoroProjet
{
    /// <summary>
    /// Logique d'interaction pour Home.xaml
    /// Permet de créer une nouvelle liste de pomodoro
    /// </summary>
    public partial class Home : UserControl
    {
        
        public Home()
        {
            InitializeComponent();

            GetTags();
        }

        public void GetTags()
        {
            TagsDataAccess tagData = new TagsDataAccess();
            lbx_pomodoro_bdd.ItemsSource = tagData.getTags();
        }

        //Ajout d'une nouvelle tache dans la list du jour
        public void AddItem(object sender, RoutedEventArgs e)
        {
            if(tbx_libellePomodoro.Text != "")
            {        
                try
                {
                    TagsDataAccess tagData = new TagsDataAccess();
                    Tag tag = tagData.GetTagByLibelle(tbx_libellePomodoro.Text);
                    if (tag == null)
                    {
                        tag = new Tag
                        {
                            Libelle = tbx_libellePomodoro.Text
                        };
                        tagData.SaveTag(tag);
                        lbx_pomodoro.Items.Add(tag);
                        GetTags();
                    }
                    else
                    {
                        lbx_pomodoro.Items.Add(tag);
                    }       
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
                lbx_pomodoro.Items.Remove(((Button)sender).Tag);
            }
            
        }

        //Ajout de tache deja existante dans la liste du jour
        public void AddItemBdd(object sender, RoutedEventArgs e)
        {
            lbx_pomodoro.Items.Add(((Button)sender).Tag);
            GetTags();
        }

        //Lancement du pomodoro et redirection vers le timer
        private void Start_btn(object sender, RoutedEventArgs e)
        {
            if(lbx_pomodoro.Items.Count > 0)
            {             
                TabControl tabcontrol = (TabControl)App.Current.MainWindow.FindName("tabcontrol");
                tabcontrol.SelectedIndex = 1;

                Timer time = (Timer)tabcontrol.FindName("Timer");
                time.StartNewPomodoros(lbx_pomodoro.Items);
                lbx_pomodoro.Items.Clear();
            }
        }
    }
}

