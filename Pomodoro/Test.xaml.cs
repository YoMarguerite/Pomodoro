using PomodoroProjet.DAL;
using PomodoroProjet.Model;
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

namespace PomodoroProjet
{
    /// <summary>
    /// Logique d'interaction pour Test.xaml
    /// </summary>
    public partial class Test : UserControl
    {
        public Test()
        {
            InitializeComponent();
            Reset();
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
