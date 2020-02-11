using PomodoroProjet.DAL;
using PomodoroProjet.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace PomodoroProjet
{
    /// <summary>
    /// Logique d'interaction pour Test.xaml
    /// </summary>
    public partial class Details : UserControl
    {
        public Details()
        {
            InitializeComponent();
        }

        public void Reset()
        {
            pomodoros = pomodorosData.GetPomodoros();
            this.grid.ItemsSource = pomodoros.ToArray();
        }

        private PomodorosDataAccess pomodorosData = new PomodorosDataAccess();
        private IEnumerable<Pomodoro> pomodoros;
    }
}
