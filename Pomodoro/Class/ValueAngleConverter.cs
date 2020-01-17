using System;
using System.Windows.Data;

namespace PomodoroProjet.Class
{
    public partial class ValueAngleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter,
                        System.Globalization.CultureInfo culture)
        {
            double value = (double)values[0];
            double minimum = (double)values[1];
            double maximum = (double)values[2];

            return MyHelper.GetAngle(value, maximum, minimum);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
                System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
