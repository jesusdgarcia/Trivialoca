using Microsoft.Maui.Graphics.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaTriviaLoca.Converters
{
    public class clsConvertirAColor : IValueConverter
    {
        /// <summary>
        /// Método para convertir una string de color hex a Microsoft.Maui.Graphics.Color
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>Color Microsoft.Maui.Graphics.Color</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ColorTypeConverter converter = new ColorTypeConverter();
            Color color = (Color)(converter.ConvertFromInvariantString((string)value));
            return color;
        }

        /// <summary>
        /// Necesario, pero no se usa en nuestro caso 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string colorString = "White"; //TODO

            return colorString;
        }
    }
}
