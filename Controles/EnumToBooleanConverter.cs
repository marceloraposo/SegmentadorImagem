using System;
using System.Globalization;
using System.Windows.Data;

namespace SegmentadorImagem.Controles
{
    public class EnumToBoolConverter : IValueConverter
    {
        #region Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //return parameter.Equals(value);
            object opcaoEscolhida = Enum.Parse(value.GetType(), parameter.ToString(), true);
            return opcaoEscolhida.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //return parameter;
            bool isChecked = (bool)value;
            object ret = null; // Enum.GetValues(targetType).GetValue(0); // valor padrão

            if (isChecked == true)
            {
                ret = Enum.Parse(targetType, parameter.ToString(), true);
            }

            return ret;
        }

        #endregion Methods
    }
}
