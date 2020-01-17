using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace PomodoroProjet
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
