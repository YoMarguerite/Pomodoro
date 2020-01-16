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
    /// Logique d'interaction pour Calendrier.xaml
    /// </summary>
    public partial class Calendrier : UserControl
    {
        public Calendrier()
        {
            InitializeComponent();
            List<ActivityTime> times = new List<ActivityTime>();
            times.Add(new ActivityTime() { Activities = "test1", NbPomodoro = 1 } );

            TimePerActivity.ItemsSource = times;
        }

        public class ActivityTime
        {
            public string Activities { get; set; }

            public int NbPomodoro { get; set; }

            public string Time { get {
                    TimeSpan timeSpan = TimeSpan.FromMinutes(25 * NbPomodoro);
                    return timeSpan.ToString(@"hh\:mm");
                } }
        }
    }
}
