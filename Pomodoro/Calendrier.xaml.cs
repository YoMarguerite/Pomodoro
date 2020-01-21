using PomodoroProjet.DAL;
using PomodoroProjet.Model;
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
        private PomodorosDataAccess pomodorosDataAccess;
        private List<ActivityTime> times;

        public Calendrier()
        {
            InitializeComponent();

            pomodorosDataAccess = new PomodorosDataAccess();
            GetPomodorosLastDay();
        }

        private ActivityTime ActivityTimeContains(Pomodoro pomodoro)
        {
            foreach(ActivityTime activity in this.times)
            {
                if (activity.Activities == pomodoro.Libelle)
                {
                    return activity;
                }
            }
            return null;
        }

        public void GetPomodorosLastDay()
        {
            DateTime dateTime1 = DateTime.Now.AddDays(-1);
            DateTime dateTime2 = DateTime.Now.AddDays(1);
            GetPomodorosDate(dateTime1, dateTime2);
            this.date1.DisplayDate = dateTime1;
            this.date2.DisplayDate = dateTime2;
        }

        private void GetPomodorosDate(DateTime date1,DateTime date2)
        {
            IEnumerable<Pomodoro> pomodoros = pomodorosDataAccess.GetPomodorosDate(date1, date2);

            times = new List<ActivityTime>();

            foreach (Pomodoro pomodoro in pomodoros)
            {
                ActivityTime activity = ActivityTimeContains(pomodoro);
                if (activity == null)
                {
                    times.Add(new ActivityTime() { Activities = pomodoro.Libelle, NbPomodoro = 1 });
                }
                else
                {
                    activity.NbPomodoro++;
                }
            }

            TimePerActivity.ItemsSource = times;
        }

        private void Btn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GetPomodorosDate(this.date1.DisplayDate, this.date2.DisplayDate);
        }

        public class ActivityTime
        {
            public string Activities { get; set; }

            public int NbPomodoro { get; set; }

            public string Time {
                get {
                    TimeSpan timeSpan = TimeSpan.FromMinutes(25 * NbPomodoro);
                    return timeSpan.ToString(@"hh\:mm");
                }
            }
        }
    }
}
