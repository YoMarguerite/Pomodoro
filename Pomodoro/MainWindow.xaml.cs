using System.Windows;
using System.Windows.Controls;

namespace PomodoroProjet
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Tabcontrol_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string tabItem = ((sender as TabControl).SelectedItem as TabItem).Header as string;

            switch (tabItem)
            {
                case "Calendrier":
                    this.Calendrier.GetPomodorosLastDay();
                    break;

                case "Test":
                    this.Test.Reset();
                    break;

                default:
                    return;
            }
        }
    }
}
