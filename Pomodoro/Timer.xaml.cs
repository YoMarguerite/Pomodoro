using PomodoroProjet.Class;
using PomodoroProjet.DAL;
using PomodoroProjet.Model;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PomodoroProjet
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Timer : UserControl
    {
        public Timer()
        {
            InitializeComponent();

            int minutes = 1;

            this.tag.Content = "Work";

            this.time = new DateTime();
            this.time = this.time.AddMinutes(minutes);
            this.timer.Content = this.time.ToString("mm:ss");

            this.pause = true;
            this.takeABreak = false;
            this.nb = 0;

            this.dispatcher = new DispatcherTimer();
            this.dispatcher.Interval = TimeSpan.FromSeconds(1);
            this.dispatcher.Tick += dispatcherTimer_Tick;
            this.dispatcher.Start();

            this.slider.Maximum = minutes * 60;
            this.slider.Value = 0;
            this.slider.Minimum = 0;
        }

        private bool _isPressed = false;
        private Canvas _templateCanvas = null;

        private DispatcherTimer dispatcher;
        private DateTime time;
        private bool pause;
        private bool takeABreak;
        private int nb;

        private ItemCollection tags;

        public void StartNewPomodoros(ItemCollection tags)
        {
            this.tags = tags;
            string libelle = ((Tag)tags[0]).Libelle;
            this.tag.Content = libelle;
            
            Pomodoro pomodoro = new Pomodoro()
            {
                Libelle = libelle,
                Date = DateTime.Now
            };
            PomodorosDataAccess pomodoroDataAccess = new PomodorosDataAccess();
            pomodoroDataAccess.SavePomodoro(pomodoro);

            this.btn1.Content = "❚❚";
            this.pause = false;
        }

        private void dispatcherTimer_Tick(Object source, EventArgs e)
        {
            if (!this.pause)
            {
                if (!this._isPressed)
                {
                    try
                    {
                        this.time = this.time.Subtract(((DispatcherTimer)source).Interval);
                        this.timer.Content = this.time.ToString("mm:ss");
                    }
                    catch
                    {
                        this.takeABreak = !this.takeABreak;
                        this.takeABreakActivate(this.takeABreak);
                    }
                    this.slider.Value = this.slider.Maximum-(this.time.Minute * 60 + this.time.Second);
                }
            }
        }

        private void ButonActivate(bool activate)
        {
            string[] content = new string[] { "❚❚", "▶" };
            this.btn1.Content = content[activate ? 1 : 0];
        }

        private void takeABreakActivate(bool activate)
        {
            int minute = 0;
            if (activate)
            {
                minute = (this.nb++ % 3 == 0) ? 15 : 5;
                this.tag.Content = "Pause";
            }
            else
            {
                minute = 25;
                if (this.tags.MoveCurrentToNext())
                {
                    this.tag.Content = ((Tag)this.tags.CurrentItem).Libelle;
                }
            }

            DateTime date = new DateTime();
            this.time = date.AddMinutes(minute);
            this.timer.Content = this.time.ToString("mm:ss");

            this.slider.Maximum = minute * 60;
            this.slider.Value = 0;
        }

        private void OnClick1(object sender, RoutedEventArgs e)
        {
            this.pause = !this.pause;
            ButonActivate(this.pause);
        }       

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isPressed = true;
        }

        private void Ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isPressed = false;
        }

        private void Ellipse_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isPressed)
            {
                if (_templateCanvas == null)
                {
                    _templateCanvas = MyHelper.FindParent<Canvas>(e.Source as Ellipse);
                    if (_templateCanvas == null) return;
                }
                const double RADIUS = 100;
                Point newPos = e.GetPosition(_templateCanvas);
                double angle = MyHelper.GetAngleR(newPos, RADIUS);
                slider.Value = (slider.Maximum - slider.Minimum) * angle / (2 * Math.PI);

                double value = this.slider.Maximum - this.slider.Value;
                int minutes = (int)value / 60;
                int seconds = (int)value % 60;
                DateTime date = new DateTime();
                date = date.AddMinutes(minutes);
                date = date.AddSeconds(seconds);

                this.time = date;
                this.timer.Content = this.time.ToString("mm:ss");
            }
        }
    }
}