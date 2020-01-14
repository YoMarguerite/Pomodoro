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
using System.Windows.Threading;

namespace Pomodoro
{
    /// <summary>
    /// Logique d'interaction pour Timer.xaml
    /// </summary>
    public partial class TimerControl : UserControl
    {
        public String timeDisplay { get; set; }
        public DispatcherTimer dispatcherTimer { get; set; }

        public TimerControl()
        {
            InitializeComponent();
            this.DataContext = this;
            SetTimer();
           
        }

        private void SetTimer()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
        }

        private void dispatcherTimer_Tick(Object source, EventArgs e)
        {
        }
    }
}
