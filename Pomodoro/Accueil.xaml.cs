using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pomodoro
{
    /// <summary>
    /// Logique d'interaction pour Accueil.xaml
    /// </summary>
    public partial class Accueil : UserControl
    {
        
        public Accueil()
        {
            InitializeComponent();

        }
        public void AddItem(object sender, RoutedEventArgs e)
        {
            lbx_pomodoro.Items.Add(tbx_libellePomodoro.Text);
            tbx_libellePomodoro.Text = "";
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if(lbx_pomodoro.Items.Count > 0)
            {
                lbx_pomodoro.Items.RemoveAt(lbx_pomodoro.Items.IndexOf(lbx_pomodoro.SelectedItem));
            }
            
        }
    }
}
