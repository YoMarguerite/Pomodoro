using Pomodoro.Class;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Pomodoro
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
                        this.time = this.time.AddMinutes(5);
                        this.timer.Content = this.time.ToString("mm:ss");
                        this.pause = true;
                        String[] content = new string[] { "||", "▶" };
                        this.btn1.Content = content[this.pause ? 1 : 0];
                        this.slider.Maximum = 5 * 60;
                        this.slider.Value = 0;
                    }
                    this.slider.Value = this.slider.Maximum-(this.time.Minute * 60 + this.time.Second);
                }
            }
        }

        private void OnClick1(object sender, RoutedEventArgs e)
        {
            this.pause = !this.pause;
            String[] content = new string[] { "||", "▶" };
            this.btn1.Content = content[this.pause ? 1 : 0];
        }

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Enable moving mouse to change the value.
            _isPressed = true;
        }

        private void Ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Disable moving mouse to change the value.
            _isPressed = false;
        }

        private void Ellipse_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isPressed)
            {
                //Find the parent canvas.
                if (_templateCanvas == null)
                {
                    _templateCanvas = MyHelper.FindParent<Canvas>(e.Source as Ellipse);
                    if (_templateCanvas == null) return;
                }
                //Canculate the current rotation angle and set the value.
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