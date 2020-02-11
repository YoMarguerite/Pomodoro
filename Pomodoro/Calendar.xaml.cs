using PomodoroProjet.DAL;
using PomodoroProjet.Model;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace PomodoroProjet
{
    /// <summary>
    /// Logique d'interaction pour Calendar.xaml
    /// Permet de récupérer la liste des pomodoros
    /// entre deux dates définis
    /// </summary>
    public partial class Calendar : UserControl
    {
        private PomodorosDataAccess pomodorosDataAccess = new PomodorosDataAccess();
        private List<ActivityTime> times;

        public Calendar()
        {
            InitializeComponent();
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
            if(date1.SelectedDate.HasValue && date2.SelectedDate.HasValue)
            {
                //On rajoute un jour à la deuxième date
                //L'heure étant fixé à 00:00 si on choisit le 21/01/2020 comme date
                //le pomodoro du 21/01/2020 15:00 ne sera pas pris en compte
                GetPomodorosDate(date1.SelectedDate.Value, date2.SelectedDate.Value.AddDays(1));
            }
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
